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
using System.Linq;
using Serilog.Debugging;
using Serilog.Events;
using Serilog.Parsing;

namespace Serilog.Parameters
{
    // Performance relevant - on the hot path when creating log events from existing templates.
    class PropertyBinder
    {
        readonly PropertyValueConverter _valueConverter;

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
            if (messageTemplateParameters == null || messageTemplateParameters.Length == 0)
            {
                if (messageTemplate.NamedProperties != null || messageTemplate.PositionalProperties != null)
                    SelfLog.WriteLine("Required properties not provided for: {0}", messageTemplate);

                return NoProperties;
            }

            if (messageTemplate.PositionalProperties != null)
                return ConstructPositionalProperties(messageTemplate, messageTemplateParameters);

            return ConstructNamedProperties(messageTemplate, messageTemplateParameters);
        }

        IEnumerable<LogEventProperty> ConstructPositionalProperties(MessageTemplate template, object[] messageTemplateParameters)
        {
            var positionalProperties = template.PositionalProperties;

            if (positionalProperties.Length != messageTemplateParameters.Length)
                SelfLog.WriteLine("Positional property count does not match parameter count: {0}", template);

            var result = new LogEventProperty[messageTemplateParameters.Length];
            int nextUnmatchedPosition = 0;
            for (int position = 0; position < messageTemplateParameters.Length; position++)
            {
                PropertyToken propertyToken = null;
                for (int propIndex = 0; propIndex < positionalProperties.Length; propIndex++)
                {
                    int propertyTokenPosition;
                    if (positionalProperties[propIndex].TryGetPositionalValue(out propertyTokenPosition)
                        && propertyTokenPosition == position)
                    {
                        propertyToken = positionalProperties[propIndex];
                    }
                }

                if (propertyToken == null)
                {
                    // found no property for this position; generate a default name
                    result[position] = new LogEventProperty(
                        "__" + nextUnmatchedPosition++,
                        _valueConverter.CreatePropertyValue(messageTemplateParameters[position]));
                }
                else
                {
                    result[position] = ConstructProperty(propertyToken, messageTemplateParameters[position]);
                }
            }

            return result;
        }

        IEnumerable<LogEventProperty> ConstructNamedProperties(MessageTemplate template, object[] messageTemplateParameters)
        {
            var namedProperties = template.NamedProperties;
            if (namedProperties == null)
                return Enumerable.Empty<LogEventProperty>();

            var matchedRun = namedProperties.Length;
            if (namedProperties.Length != messageTemplateParameters.Length)
            {
                matchedRun = Math.Min(namedProperties.Length, messageTemplateParameters.Length);
                SelfLog.WriteLine("Named property count does not match parameter count: {0}", template);
            }

            int nextUnmatchedPosition = 0;
            var result = new LogEventProperty[messageTemplateParameters.Length];
            for (var i = 0; i < result.Length; ++i)
            {
                if (i < matchedRun)
                {
                    var property = template.NamedProperties[i];
                    var value = messageTemplateParameters[i];
                    result[i] = ConstructProperty(property, value);
                }
                else
                {
                    // found no property for this position; generate a default name
                    result[i] = new LogEventProperty(
                        "__" + nextUnmatchedPosition++,
                        _valueConverter.CreatePropertyValue(messageTemplateParameters[i]));
                }
            }

            return result;
        }

        LogEventProperty ConstructProperty(PropertyToken propertyToken, object value)
        {
            return new LogEventProperty(
                        propertyToken.PropertyName,
                        _valueConverter.CreatePropertyValue(value, propertyToken.Destructuring));
        }
    }
}
