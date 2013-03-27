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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Serilog.Debugging;
using Serilog.Events;
using Serilog.Parsing;

namespace Serilog.Core
{
    /// <summary>
    /// Represents a message template passed to a log method. The template
    /// can create properties based on an ordered list of provided values, and
    /// can subsequently render the template in textual form given the list
    /// of properties.
    /// </summary>
    public class MessageTemplate
    {
        static readonly object[] NoParameters = new object[0];
        static readonly LogEventProperty[] NoProperties = new LogEventProperty[0];
        readonly IEnumerable<MessageTemplateToken> _tokens;

        internal MessageTemplate(IEnumerable<MessageTemplateToken> tokens)
        {
            _tokens = tokens;
        }

        /// <summary>
        /// Create properties based on an ordered list of provided values.
        /// </summary>
        /// <param name="messageTemplateParameters">Objects corresponding to the properties
        /// represented in the message template.</param>
        /// <returns>A list of properties; if the template is malformed then
        /// this will be empty.</returns>
        public IEnumerable<LogEventProperty> ConstructProperties(object[] messageTemplateParameters)
        {
            return ConstructPositionalProperties(messageTemplateParameters ?? NoParameters);
        }

        IEnumerable<LogEventProperty> ConstructPositionalProperties(object[] messageTemplateParameters)
        {
            var pcount = 0;
            var pmax = -1;
            List<Tuple<int, PropertyToken>> positionalsInTemplate = null;

            foreach (var propertyToken in _tokens.OfType<PropertyToken>())
            {
                int position;
                if (!propertyToken.TryGetPositionalValue(out position))
                    return ConstructNamedProperties(messageTemplateParameters);

                ++pcount;
                pmax = Math.Max(pmax, position);
                positionalsInTemplate = positionalsInTemplate ?? new List<Tuple<int, PropertyToken>>(messageTemplateParameters.Length);
                positionalsInTemplate.Add(Tuple.Create(position, propertyToken));
            }

            if (pcount != messageTemplateParameters.Length || pmax != pcount - 1)
                SelfLog.WriteLine("Positional properties in {0} do not line up with parameters.", this);

            if (positionalsInTemplate == null)
                return NoProperties;

            return positionalsInTemplate
                .Where(p => p.Item1 < messageTemplateParameters.Length)
                .Select(p => ConstructProperty(p.Item2, messageTemplateParameters[p.Item1]));
        }

        IEnumerable<LogEventProperty> ConstructNamedProperties(object[] messageTemplateParameters)
        {
            var mismatchWarningIssued = false;

            var next = 0;
            foreach (var propertyToken in _tokens.OfType<PropertyToken>())
            {
                if (propertyToken.IsPositional && !mismatchWarningIssued)
                {
                    mismatchWarningIssued = true;
                    SelfLog.WriteLine("Message template is malformed: {0}.", this);
                }

                if (next < messageTemplateParameters.Length)
                {
                    var value = messageTemplateParameters[next];
                    yield return ConstructProperty(propertyToken, value);
                }
                else
                {
                    if (!mismatchWarningIssued)
                    {
                        mismatchWarningIssued = true;
                        SelfLog.WriteLine("Message template has more parameters than provided: {0}.", this);
                    }
                }
                next++;
            }

            if (next != messageTemplateParameters.Length - 1 && !mismatchWarningIssued)
                SelfLog.WriteLine("Too many parameters provided for message template: {0}.", this);
        }

        static LogEventProperty ConstructProperty(PropertyToken propertyToken, object value)
        {
            return new LogEventProperty(
                        propertyToken.PropertyName,
                        LogEventPropertyValue.For(value, propertyToken.Destructuring));
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