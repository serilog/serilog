// Copyright 2013-2015 Serilog Contributors
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

using System;
using System.Collections.Generic;
using System.Text;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.Parsing
{
    /// <summary>
    /// Parses message template strings into sequences of text or property
    /// tokens.
    /// </summary>
    public class MessageTemplateParser : IMessageTemplateParser
    {
        /// <summary>
        /// Parse the supplied message template.
        /// </summary>
        /// <param name="messageTemplate">The message template to parse.</param>
        /// <returns>A sequence of text or property tokens. Where the template
        /// is not syntactically valid, text tokens will be returned. The parser
        /// will make a best effort to extract valid property tokens even in the
        /// presence of parsing issues.</returns>
        public MessageTemplate Parse(string messageTemplate)
        {
            if (messageTemplate == null)
                throw new ArgumentNullException(nameof(messageTemplate));
            return new MessageTemplate(messageTemplate, Tokenize(messageTemplate));
        }

        static IEnumerable<MessageTemplateToken> Tokenize(string messageTemplate)
        {
            if (messageTemplate.Length == 0)
            {
                yield return new TextToken("", 0);
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
                var pt = ParsePropertyToken(nextIndex, messageTemplate, out nextIndex);
                if (beforeProp < nextIndex)
                    yield return pt;

                if (nextIndex == messageTemplate.Length)
                    yield break;
            }
        }

        static MessageTemplateToken ParsePropertyToken(int startAt, string messageTemplate, out int next)
        {
            var first = startAt;
            startAt++;
            while (startAt < messageTemplate.Length && IsValidInPropertyTag(messageTemplate[startAt]))
                startAt++;

            if (startAt == messageTemplate.Length || messageTemplate[startAt] != '}')
            {
                next = startAt;
                return new TextToken(messageTemplate.Substring(first, next - first), first);
            }

            next = startAt + 1;

            var rawText = messageTemplate.Substring(first, next - first);
            var tagContent = rawText.Substring(1, next - (first + 2));
            if (tagContent.Length == 0)
                return new TextToken(rawText, first);

            string propertyNameAndDestructuring, format, alignment;
            if (!TrySplitTagContent(tagContent, out propertyNameAndDestructuring, out format, out alignment))
                return new TextToken(rawText, first);

            var propertyName = propertyNameAndDestructuring;
            var destructuring = Destructuring.Default;
            if (propertyName.Length != 0 && TryGetDestructuringHint(propertyName[0], out destructuring))
                propertyName = propertyName.Substring(1);

            if (propertyName.Length == 0)
                return new TextToken(rawText, first);

            for (var i = 0; i < propertyName.Length; ++i)
            {
                var c = propertyName[i];
                if (!IsValidInPropertyName(c))
                    return new TextToken(rawText, first);
            }

            if (format != null)
            {
                for (var i = 0; i < format.Length; ++i)
                {
                    var c = format[i];
                    if (!IsValidInFormat(c))
                        return new TextToken(rawText, first);
                }
            }

            Alignment? alignmentValue = null;
            if (alignment != null)
            {
                for (var i = 0; i < alignment.Length; ++i)
                {
                    var c = alignment[i];
                    if (!IsValidInAlignment(c))
                        return new TextToken(rawText, first);
                }

                var lastDash = alignment.LastIndexOf('-');
                if (lastDash > 0)
                    return new TextToken(rawText, first);

                var width = 0;
                if (!int.TryParse(lastDash == -1 ? alignment : alignment.Substring(1), out width) || width == 0)
                    return new TextToken(rawText, first);

                var direction = lastDash == -1 ?
                    AlignmentDirection.Right :
                    AlignmentDirection.Left;

                alignmentValue = new Alignment(direction, width);
            }

            return new PropertyToken(
                propertyName,
                rawText,
                format,
                alignmentValue,
                destructuring,
                first);
        }

        static bool TrySplitTagContent(string tagContent, out string propertyNameAndDestructuring, out string format, out string alignment)
        {
            var formatDelim = tagContent.IndexOf(':');
            var alignmentDelim = tagContent.IndexOf(',');
            if (formatDelim == -1 && alignmentDelim == -1)
            {
                propertyNameAndDestructuring = tagContent;
                format = null;
                alignment = null;
            }
            else
            {
                if (alignmentDelim == -1 || (formatDelim != -1 && alignmentDelim > formatDelim))
                {
                    propertyNameAndDestructuring = tagContent.Substring(0, formatDelim);
                    format = formatDelim == tagContent.Length - 1 ?
                        null :
                        tagContent.Substring(formatDelim + 1);
                    alignment = null;
                }
                else
                {
                    propertyNameAndDestructuring = tagContent.Substring(0, alignmentDelim);
                    if (formatDelim == -1)
                    {
                        if (alignmentDelim == tagContent.Length - 1)
                        {
                            alignment = format = null;
                            return false;
                        }

                        format = null;
                        alignment = tagContent.Substring(alignmentDelim + 1);
                    }
                    else
                    {
                        if (alignmentDelim == formatDelim - 1)
                        {
                            alignment = format = null;
                            return false;
                        }

                        alignment = tagContent.Substring(alignmentDelim + 1, formatDelim - alignmentDelim - 1);
                        format = formatDelim == tagContent.Length - 1 ?
                            null :
                            tagContent.Substring(formatDelim + 1);
                    }
                }
            }

            return true;
        }

        static bool IsValidInPropertyTag(char c)
        {
            return IsValidInDestructuringHint(c) ||
                IsValidInPropertyName(c) ||
                IsValidInFormat(c) ||
                c == ':';
        }

        static bool IsValidInPropertyName(char c)
        {
            return char.IsLetterOrDigit(c) || c == '_';
        }

        static bool TryGetDestructuringHint(char c, out Destructuring destructuring)
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

        static bool IsValidInDestructuringHint(char c)
        {
            return c == '@' ||
                   c == '$';
        }

        static bool IsValidInAlignment(char c)
        {
            return char.IsDigit(c) ||
                   c == '-';
        }

        static bool IsValidInFormat(char c)
        {
            return c != '}' &&
                (char.IsLetterOrDigit(c) ||
                 char.IsPunctuation(c) ||
                 c == ' ');
        }

        static TextToken ParseTextToken(int startAt, string messageTemplate, out int next)
        {
            var first = startAt;

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
            return new TextToken(accum.ToString(), first);
        }
    }
}
