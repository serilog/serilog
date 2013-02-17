using System.Collections.Generic;
using System.Text;

namespace Opi.Parsing
{
    class MessageTemplateTokenParser
    {
        public static IEnumerable<MessageTemplateToken> Parse(string messageTemplate)
        {
            if (messageTemplate == "")
            {
                yield return new TextToken("");
                yield break;
            }

            var nextIndex = 0;
            while (true)
            {
                var beforeText = nextIndex;
                var tt = ParseTextToken(nextIndex, messageTemplate, out nextIndex);
                if (nextIndex > beforeText)
                    yield return tt;

                if (nextIndex == messageTemplate.Length)
                    yield break;

                var beforeProp = nextIndex;
                var pt =  ParsePropertyToken(nextIndex, messageTemplate, out nextIndex);
                if (beforeProp < nextIndex)
                    yield return pt;

                if (nextIndex == messageTemplate.Length)
                    yield break;
            }
        }

        private static MessageTemplateToken ParsePropertyToken(int startAt, string messageTemplate, out int next)
        {
            var first = startAt;
            startAt++;
            while (startAt < messageTemplate.Length && IsValidInPropetyTag(messageTemplate[startAt]))
                startAt++;

            if (startAt == messageTemplate.Length || messageTemplate[startAt] != '}')
            {
                next = startAt;
                return new TextToken(messageTemplate.Substring(first, next - first));
            }
            
            next = startAt + 1;

            var rawText = messageTemplate.Substring(first, next - first);
            var tagContent = messageTemplate.Substring(first + 1, next - (first + 2));
            if (tagContent.Length == 0 ||
                !char.IsLetter(tagContent[0]))
                return new TextToken(rawText);

            var parts = tagContent.Split(':');
            if (parts.Length > 2)
                return new TextToken(rawText);

            var propertyName = parts[0];
            var format = parts.Length == 2 ? parts[1] : null;

            foreach (var c in propertyName)
                if (!IsValidInPropetyName(c))
                    return new TextToken(rawText);

            var destructuringHint = DestructuringHint.Default;
            if (format != null)
            {
                foreach (var c in format)
                    if (!IsValidInFormat(c))
                        return new TextToken(rawText);

                if (format == "s" || format == "S")
                {
                    destructuringHint = DestructuringHint.Stringify;
                    format = null;
                }
            }

            return new LogEventPropertyToken(
                propertyName,
                rawText,
                format,
                destructuringHint);
        }

        private static bool IsValidInPropetyTag(char c)
        {
            return IsValidInPropetyName(c) ||
                IsValidInFormat(c) ||
                c == ':';
                
        }

        private static bool IsValidInPropetyName(char c)
        {
            return char.IsLetterOrDigit(c) ||
                c == '-' ||
                c == '.';
        }

        private static bool IsValidInFormat(char c)
        {
            return char.IsLetterOrDigit(c) ||
                c == '#' ||
                c == '-' ||
                c == '.';
        }

        private static TextToken ParseTextToken(int startAt, string messageTemplate, out int next)
        {
            var accum = new StringBuilder();
            do
            {
                var nc = messageTemplate[startAt];
                if (nc == '{')
                {
                    if (startAt + 1 < messageTemplate.Length &&
                        messageTemplate[startAt + 1] == '{')
                    {
                        accum.Append(nc);
                        startAt++;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    accum.Append(nc);
                    if (nc == '}')
                    {
                        if (startAt + 1 < messageTemplate.Length &&
                            messageTemplate[startAt + 1] == '}')
                        {
                            startAt++;
                        }
                    }
                }

                startAt++;
            } while (startAt < messageTemplate.Length);

            next = startAt;
            return new TextToken(accum.ToString());
        }
    }
}
