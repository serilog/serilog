// Copyright 2013 Nicholas Blumhardt
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Collections.Generic;
using System.Text;

namespace Serilog.Parsing
{
    public class MessageTemplateParser
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
            while (startAt < messageTemplate.Length && IsValidInPropertyTag(messageTemplate[startAt]))
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
                !IsValidInPropertyTag(tagContent[0]))
                return new TextToken(rawText);

            var parts = tagContent.Split(':');
            if (parts.Length > 2)
                return new TextToken(rawText);

            var propertyNameAndDestructuring = parts[0];
            var format = parts.Length == 2 ? parts[1] : null;

            var propertyName = propertyNameAndDestructuring;
            Destructuring destructuring;
            if (TryGetDestructuringHint(propertyName[0], out destructuring))
                propertyName = propertyName.Substring(1);

            if (propertyName == "" || !char.IsLetter(propertyName[0]))
                return new TextToken(rawText);

            foreach (var c in propertyName)
                if (!IsValidInPropertyName(c))
                    return new TextToken(rawText);

            if (format != null)
            {
                foreach (var c in format)
                    if (!IsValidInFormat(c))
                        return new TextToken(rawText);
            }

            return new LogEventPropertyToken(
                propertyName,
                rawText,
                format,
                destructuring);
        }

        private static bool IsValidInPropertyTag(char c)
        {
            return IsValidInDestructuringHint(c) ||
                IsValidInPropertyName(c) ||
                IsValidInFormat(c) ||
                c == ':';
                
        }

        private static bool IsValidInPropertyName(char c)
        {
            return char.IsLetterOrDigit(c);
        }

        private static bool TryGetDestructuringHint(char c, out Destructuring destructuring)
        {
            switch (c)
            {
                case '@':
                {
                    destructuring = Destructuring.Destructure;
                    return true;
                }
                case '$':
                {
                    destructuring = Destructuring.Stringify;
                    return true;
                }
                default:
                {
                    destructuring = Destructuring.Default;
                    return false;
                }
            }
        }

        private static bool IsValidInDestructuringHint(char c)
        {
            return c == '@' ||
                   c == '$';
        }

        private static bool IsValidInFormat(char c)
        {
            return c != '}' &&
                (char.IsLetterOrDigit(c) ||
                 char.IsPunctuation(c) ||
                 c == ' ');
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
