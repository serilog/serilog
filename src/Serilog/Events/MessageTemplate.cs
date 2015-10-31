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
using System.IO;
using System.Linq;
using Serilog.Debugging;
using Serilog.Parsing;

#if NET40
using IPropertyDictionary = System.Collections.Generic.IDictionary<string, Serilog.Events.LogEventPropertyValue>;
#else
using IPropertyDictionary = System.Collections.Generic.IReadOnlyDictionary<string, Serilog.Events.LogEventPropertyValue>;
#endif

namespace Serilog.Events
{
    /// <summary>
    /// Represents a message template passed to a log method. The template
    /// can subsequently render the template in textual form given the list
    /// of properties.
    /// </summary>
    public class MessageTemplate
    {
        readonly string _text;
        readonly MessageTemplateToken[] _tokens;

        // Optimisation for when the template is bound to
        // property values.
        readonly PropertyToken[] _positionalProperties;
        readonly PropertyToken[] _namedProperties;

        /// <summary>
        /// Construct a message template using manually-defined text and property tokens.
        /// </summary>
        /// <param name="tokens">The text and property tokens defining the template.</param>
        public MessageTemplate(IEnumerable<MessageTemplateToken> tokens)
            : this(string.Join("", tokens), tokens)
        {
        }

        /// <summary>
        /// Construct a message template using manually-defined text and property tokens.
        /// </summary>
        /// <param name="text">The full text of the template; used by Serilog internally to avoid unneeded
        /// string concatenation.</param>
        /// <param name="tokens">The text and property tokens defining the template.</param>
        public MessageTemplate(string text, IEnumerable<MessageTemplateToken> tokens)
        {
            if (text == null) throw new ArgumentNullException("text");
            if (tokens == null) throw new ArgumentNullException("tokens");

            _text = text;
            _tokens = tokens.ToArray();

            var propertyTokens = _tokens.OfType<PropertyToken>().ToArray();
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
                    _positionalProperties = propertyTokens;
                }
                else
                {
                    if (anyPositional)
                        SelfLog.WriteLine("Message template is malformed: {0}", text);

                    _namedProperties = propertyTokens;
                }
            }
        }

        /// <summary>
        /// The raw text describing the template.
        /// </summary>
        public string Text
        {
            get { return _text; }
        }

        /// <summary>
        /// Render the template as a string.
        /// </summary>
        /// <returns>The string representation of the template.</returns>
        public override string ToString()
        {
            return Text;
        }

        /// <summary>
        /// The tokens parsed from the template.
        /// </summary>
        public IEnumerable<MessageTemplateToken> Tokens
        {
            get { return _tokens; }
        }

        internal PropertyToken[] NamedProperties
        {
            get { return _namedProperties; }
        }

        internal PropertyToken[] PositionalProperties
        {
            get { return _positionalProperties; }
        }

        /// <summary>
        /// Convert the message template into a textual message, given the
        /// properties matching the tokens in the message template.
        /// </summary>
        /// <param name="properties">Properties matching template tokens.</param>
        /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
        /// <returns>The message created from the template and properties. If the
        /// properties are mismatched with the template, the template will be
        /// returned with incomplete substitution.</returns>
        public string Render(IPropertyDictionary properties, IFormatProvider formatProvider = null)
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
        public void Render(IPropertyDictionary properties, TextWriter output, IFormatProvider formatProvider = null)
        {
            foreach (var token in _tokens)
            {
                token.Render(properties, output, formatProvider);
            }
        }
    }
}