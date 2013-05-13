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
using System.Linq;
using Serilog.Core;
using Serilog.Debugging;
using Serilog.Events;
using Serilog.Parsing;

namespace Serilog.Parameters
{
    class PropertyBinder
    {
        readonly PropertyValueConverter _valueConverter;

        static readonly object[] NoParameters = new object[0];
        static readonly LogEventProperty[] NoProperties = new LogEventProperty[0];

        public PropertyBinder(PropertyValueConverter valueConverter)
        {
            _valueConverter = valueConverter;
        }

        /// <summary>
        /// Create properties based on an ordered list of provided values.
        /// </summary>
        /// <param name="messageTemplate">The template that the parameters apply to.</param>
        /// <param name="messageTemplateParameters">Objects corresponding to the properties
        /// represented in the message template.</param>
        /// <returns>A list of properties; if the template is malformed then
        /// this will be empty.</returns>
        public IEnumerable<LogEventProperty> ConstructProperties(MessageTemplate messageTemplate, object[] messageTemplateParameters)
        {
            return ConstructPositionalProperties(messageTemplate, messageTemplateParameters ?? NoParameters);
        }

        IEnumerable<LogEventProperty> ConstructPositionalProperties(MessageTemplate messageTemplate, object[] messageTemplateParameters)
        {
            var pcount = 0;
            var pmax = -1;
            List<Tuple<int, PropertyToken>> positionalsInTemplate = null;

            foreach (var propertyToken in messageTemplate.Tokens.OfType<PropertyToken>())
            {
                int position;
                if (!propertyToken.TryGetPositionalValue(out position))
                    return ConstructNamedProperties(messageTemplate, messageTemplateParameters);

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

        IEnumerable<LogEventProperty> ConstructNamedProperties(MessageTemplate messageTemplate, object[] messageTemplateParameters)
        {
            var mismatchWarningIssued = false;

            var next = 0;
            foreach (var propertyToken in messageTemplate.Tokens.OfType<PropertyToken>())
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

            if (next > messageTemplateParameters.Length && !mismatchWarningIssued)
                SelfLog.WriteLine("Too many parameters provided for message template: {0}.", this);
        }

        LogEventProperty ConstructProperty(PropertyToken propertyToken, object value)
        {
            return new LogEventProperty(
                        propertyToken.PropertyName,
                        _valueConverter.CreatePropertyValue(value, propertyToken.Destructuring));
        }
    }
}
