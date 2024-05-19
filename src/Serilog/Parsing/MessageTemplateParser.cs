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

namespace Serilog.Parsing;

/// <summary>
/// Parses message template strings into sequences of text or property
/// tokens.
/// </summary>
public class MessageTemplateParser : IMessageTemplateParser
{
    /// <summary>
    /// When set, property names in templates may include `.`.
    /// </summary>
    static readonly bool DefaultAcceptDottedPropertyNames = AppContext.TryGetSwitch("Serilog.Parsing.MessageTemplateParser.AcceptDottedPropertyNames", out var isEnabled) && isEnabled;

    static readonly TextToken EmptyTextToken = new("");

    readonly bool _acceptDottedPropertyNames;

    /// <summary>
    /// Construct a <see cref="MessageTemplateParser"/>.
    /// </summary>
    public MessageTemplateParser()
        : this(DefaultAcceptDottedPropertyNames)
    {
    }

    internal MessageTemplateParser(bool acceptDottedPropertyNames)
    {
        _acceptDottedPropertyNames = acceptDottedPropertyNames;
    }

    /// <summary>
    /// Parse the supplied message template.
    /// </summary>
    /// <param name="messageTemplate">The message template to parse.</param>
    /// <returns>A sequence of text or property tokens. Where the template
    /// is not syntactically valid, text tokens will be returned. The parser
    /// will make a best effort to extract valid property tokens even in the
    /// presence of parsing issues.</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="messageTemplate"/> is <code>null</code></exception>
    public MessageTemplate Parse(string messageTemplate)
    {
        Guard.AgainstNull(messageTemplate);

        return new(messageTemplate, Tokenize(messageTemplate));
    }

    IEnumerable<MessageTemplateToken> Tokenize(string messageTemplate)
    {
        if (messageTemplate.Length == 0)
        {
            yield return EmptyTextToken;
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

    MessageTemplateToken ParsePropertyToken(int startAt, string messageTemplate, out int next)
    {
        var first = startAt;
        startAt++;

        startAt = messageTemplate.IndexOf('}', startAt);
        if (startAt == -1)
        {
            next = messageTemplate.Length;
            return new TextToken(messageTemplate.Substring(first));
        }

        next = startAt + 1;

        var rawText = messageTemplate.Substring(first, next - first);
        var tagContent = rawText.Substring(1, next - (first + 2));
        if (tagContent.Length == 0)
            return new TextToken(rawText);

        if (!TrySplitTagContent(tagContent, out var propertyNameAndDestructuring, out var format, out var alignment))
            return new TextToken(rawText);

        var propertyName = propertyNameAndDestructuring;
        var destructuring = Destructuring.Default;
        if (propertyName.Length != 0 && TryGetDestructuringHint(propertyName[0], out destructuring))
            propertyName = propertyName.Substring(1);

        if (propertyName.Length == 0)
            return new TextToken(rawText);

        for (var i = 0; i < propertyName.Length; ++i)
        {
            var c = propertyName[i];
            if (!IsValidInPropertyName(c))
                return new TextToken(rawText);
        }

        if (format != null)
        {
            for (var i = 0; i < format.Length; ++i)
            {
                var c = format[i];
                if (!IsValidInFormat(c))
                    return new TextToken(rawText);
            }
        }

        Alignment? alignmentValue = null;
        if (alignment != null)
        {
            for (var i = 0; i < alignment.Length; ++i)
            {
                var c = alignment[i];
                if (!IsValidInAlignment(c))
                    return new TextToken(rawText);
            }

            var lastDash = alignment.LastIndexOf('-');
            if (lastDash > 0)
                return new TextToken(rawText);

            if (!int.TryParse(lastDash == -1 ? alignment : alignment.Substring(1), out var width) || width == 0)
                return new TextToken(rawText);

            var direction = lastDash == -1 ?
                AlignmentDirection.Right :
                AlignmentDirection.Left;

            alignmentValue = new(direction, width);
        }

        return new PropertyToken(
            propertyName,
            rawText,
            format,
            alignmentValue,
            destructuring);
    }

    static bool TrySplitTagContent(string tagContent, [NotNullWhen(true)] out string? propertyNameAndDestructuring, out string? format, out string? alignment)
    {
        var formatDelim = tagContent.IndexOf(':');
        var alignmentDelim = tagContent.IndexOf(',');
        if (formatDelim == -1 && alignmentDelim == -1)
        {
            propertyNameAndDestructuring = tagContent;
            format = null;
            alignment = null;
            return true;
        }

        if (alignmentDelim == -1 || (formatDelim != -1 && alignmentDelim > formatDelim))
        {
            propertyNameAndDestructuring = tagContent.Substring(0, formatDelim);
            format = formatDelim == tagContent.Length - 1 ?
                null :
                tagContent.Substring(formatDelim + 1);
            alignment = null;
            return true;
        }

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
            return true;
        }

        if (alignmentDelim == formatDelim - 1)
        {
            alignment = format = null;
            return false;
        }

        alignment = tagContent.Substring(alignmentDelim + 1, formatDelim - alignmentDelim - 1);
        format = formatDelim == tagContent.Length - 1 ?
            null :
            tagContent.Substring(formatDelim + 1);

        return true;
    }

    bool IsValidInPropertyName(char c) =>
        char.IsLetterOrDigit(c) ||
        c is '_' ||
        _acceptDottedPropertyNames && c is '.';

    static bool TryGetDestructuringHint(char c, out Destructuring destructuring)
    {
        switch (c)
        {
            case '@':
                destructuring = Destructuring.Destructure;
                return true;
            case '$':
                destructuring = Destructuring.Stringify;
                return true;
            default:
                destructuring = Destructuring.Default;
                return false;
        }
    }

    static bool IsValidInDestructuringHint(char c)
    {
        return c is '@' or '$';
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
                c is ' ' or '+');
    }

    static TextToken ParseTextToken(int startAt, string messageTemplate, out int next)
    {
        // If we encounter escape sequences like {{ or }}, the result is not a strict substring of the
        // template. But, this requires allocating a StringBuilder, so we try to parse as far as we can first, and
        // only allocate the StringBuilder/fall through to the slow string-building path if we actually need to.
        // Most of the time we won't hit escapes, so we can get away with just a single Substring() allocation at
        // the end.

        var i = messageTemplate.IndexOfAny(['{', '}'], startAt);
        if (i == -1)
        {
            // No more interesting characters in the template, everything left is text.
            next = messageTemplate.Length;
            return new(messageTemplate[startAt..]);
        }

        StringBuilder accum;
        var ch = messageTemplate[i];
        ++i;

        // The character must be either `{` or `}`, since we found its index.
        if (ch == '{')
        {
            if (i < messageTemplate.Length && messageTemplate[i] == '{')
            {
                // Hit an escape sequence; ignore the second (duplicate) `{`, and push the rest onto the
                // accumulator to start the slow path.
                accum = new(messageTemplate, startAt, i - startAt, messageTemplate.Length - startAt);
                ++i;
            }
            else
            {
                // Hit the start of a property token. We're done, no StringBuilder was required.
                next = i - 1;
                return next == startAt ? EmptyTextToken : new(messageTemplate.Substring(startAt, i - 1 - startAt));
            }
        }
        else // ch == '}'
        {
            accum = new(messageTemplate, startAt, i - startAt, messageTemplate.Length - startAt);
            if (i < messageTemplate.Length && messageTemplate[i] == '}')
            {
                // Hit an escaped `}`; as before, skip the duplicate and start accumulating the result.
                ++i;
            }
        }

        // We must have encountered an escaped character sequence: finish the text token, using the
        // accumulator. This is relatively uncommon so we just to it char-by-char.
        while (i < messageTemplate.Length)
        {
            ch = messageTemplate[i];
            ++i;

            if (ch == '{')
            {
                if (i < messageTemplate.Length && messageTemplate[i] == '{')
                {
                    accum.Append(ch);
                    ++i;
                }
                else
                {
                    next = i - 1;
                    return new(accum.ToString());
                }
            }
            else
            {
                accum.Append(ch);
                if (ch == '}')
                {
                    if (i < messageTemplate.Length && messageTemplate[i] == '}')
                    {
                        ++i;
                    }
                }
            }
        }

        next = i;
        return new(accum.ToString());
    }
}
