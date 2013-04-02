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
using System.IO;
using System.Linq;
using Serilog.Events;
using Serilog.Parsing;

namespace Serilog.Core
{
    /// <summary>
    /// Represents a message template passed to a log method. The template
    /// can subsequently render the template in textual form given the list
    /// of properties.
    /// </summary>
    public class MessageTemplate
    {
        readonly string _text;
        readonly IEnumerable<MessageTemplateToken> _tokens;

        internal MessageTemplate(string text, IEnumerable<MessageTemplateToken> tokens)
        {
            _text = text;
            _tokens = tokens.ToArray();
        }

        /// <summary>
        /// The raw text describing the template.
        /// </summary>
        public string Text
        {
            get { return _text; }
        }

        /// <summary>
        /// The tokens parsed from the template.
        /// </summary>
        public IEnumerable<MessageTemplateToken> Tokens
        {
            get { return _tokens; }
        }

        /// <summary>
        /// Convert the message template into a textual message, given the
        /// properties matching the tokens in the message template.
        /// </summary>
        /// <param name="properties">Properties matching template tokens.</param>
        /// <returns>The message created from the template and properties. If the
        /// properties are mismatched with the template, the template will be
        /// returned with incomplete substitution.</returns>
        public string Render(IReadOnlyDictionary<string, LogEventProperty> properties)
        {
            var writer = new StringWriter();
            Render(properties, writer);
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
        public void Render(IReadOnlyDictionary<string, LogEventProperty> properties, TextWriter output)
        {
            foreach (var token in _tokens)
            {
                token.Render(properties, output);
            }
        }
    }
}