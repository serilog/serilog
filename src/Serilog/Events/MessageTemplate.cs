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

namespace Serilog.Events;

/// <summary>
/// Represents a message template passed to a log method. The template
/// can subsequently render the template in textual form given the list
/// of properties.
/// </summary>
public class MessageTemplate
{
    /// <summary>
    /// Construct a message template using manually-defined text and property tokens.
    /// </summary>
    /// <param name="tokens">The text and property tokens defining the template.</param>
    public MessageTemplate(IEnumerable<MessageTemplateToken> tokens)
        // ReSharper disable PossibleMultipleEnumeration
        : this(string.Concat(tokens), tokens)
    // ReSharper enable PossibleMultipleEnumeration
    {
    }

    /// <summary>
    /// Construct a message template using manually-defined text and property tokens.
    /// </summary>
    /// <param name="text">The full text of the template; used by Serilog internally to avoid unneeded
    /// string concatenation.</param>
    /// <param name="tokens">The text and property tokens defining the template.</param>
    /// <exception cref="ArgumentNullException">When <paramref name="text"/> is <code>null</code></exception>
    /// <exception cref="ArgumentNullException">When <paramref name="tokens"/> is <code>null</code></exception>
    public MessageTemplate(string text, IEnumerable<MessageTemplateToken> tokens)
    {
        Text = Guard.AgainstNull(text);
        TokenArray = Guard.AgainstNull(tokens).ToArray();

        var propertyTokens = GetElementsOfTypeToArray<PropertyToken>(TokenArray);
        if (propertyTokens.Length != 0)
        {
            var allPositional = true;
            var anyPositional = false;
            foreach (var propertyToken in propertyTokens)
            {
                if (propertyToken.IsPositional)
                    anyPositional = true;
                else
                    allPositional = false;
            }

            if (allPositional)
            {
                PositionalProperties = propertyTokens;
            }
            else
            {
                if (anyPositional)
                    SelfLog.WriteLine("Message template is malformed: {0}", text);

                NamedProperties = propertyTokens;
            }
        }
    }

    /// <summary>
    /// Similar to <see cref="Enumerable.OfType{TResult}"/>, but faster.
    /// </summary>
    static TResult[] GetElementsOfTypeToArray<TResult>(MessageTemplateToken[] tokens)
        where TResult : class
    {
        var result = new List<TResult>(tokens.Length / 2);
        for (var i = 0; i < tokens.Length; i++)
        {
            if (tokens[i] is TResult token)
            {
                result.Add(token);
            }
        }
        return result.ToArray();
    }

    /// <summary>
    /// The raw text describing the template.
    /// </summary>
    public string Text { get; }

    /// <summary>
    /// Render the template as a string.
    /// </summary>
    /// <returns>The string representation of the template.</returns>
    public override string ToString() => Text;

    /// <summary>
    /// The tokens parsed from the template.
    /// </summary>
    public IEnumerable<MessageTemplateToken> Tokens => TokenArray;

    internal MessageTemplateToken[] TokenArray { get; }

    internal PropertyToken[]? NamedProperties { get; }

    internal PropertyToken[]? PositionalProperties { get; }

    /// <summary>
    /// Convert the message template into a textual message, given the
    /// properties matching the tokens in the message template.
    /// </summary>
    /// <param name="properties">Properties matching template tokens.</param>
    /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
    /// <returns>The message created from the template and properties. If the
    /// properties are mismatched with the template, the template will be
    /// returned with incomplete substitution.</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="properties"/> is <code>null</code></exception>
    public string Render(IReadOnlyDictionary<string, LogEventPropertyValue> properties, IFormatProvider? formatProvider = null)
    {
        var writer = new StringWriter(formatProvider);
        Render(properties, writer, formatProvider);
        return writer.ToString();
    }

    /// <summary>
    /// Convert the message template into a textual message, given the
    /// properties matching the tokens in the message template.
    /// </summary>
    /// <param name="properties">Properties matching template tokens.</param>
    /// <param name="output">The message created from the template and properties. If the
    /// properties are mismatched with the template, the template will be
    /// returned with incomplete substitution.</param>
    /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
    /// <exception cref="ArgumentNullException">When <paramref name="properties"/> is <code>null</code></exception>
    /// <exception cref="ArgumentNullException">When <paramref name="output"/> is <code>null</code></exception>
    public void Render(IReadOnlyDictionary<string, LogEventPropertyValue> properties, TextWriter output, IFormatProvider? formatProvider = null)
    {
        Guard.AgainstNull(properties);
        Guard.AgainstNull(output);

        MessageTemplateRenderer.Render(this, properties, output, null, formatProvider);
    }
}
