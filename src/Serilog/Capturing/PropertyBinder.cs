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

namespace Serilog.Capturing
{
    // Performance relevant - on the hot path when creating log events from existing templates.
    class PropertyBinder
    {
        readonly PropertyValueConverter _valueConverter;

        static readonly string[] UnmatchedNames = Enumerable.Range(0, 100).Select(i => "__" + i).ToArray();

        public PropertyBinder(PropertyValueConverter valueConverter)
        {
            _valueConverter = valueConverter;
        }

        /// <summary>
        /// Create properties based on an ordered list of provided values.
        /// </summary>
        /// <param name="messageTemplate">The template that the parameters apply to.</param>
        /// <param name="messageTemplateParameters">Objects corresponding to the properties
        ///     represented in the message template.</param>
        /// <param name="properties">Properties associated with the event, including those presented in <paramref name="messageTemplate"/>.</param>>
        /// <returns>A list of properties; if the template is malformed then
        /// this will be empty.</returns>
        public void ConstructProperties(MessageTemplate messageTemplate, object[] messageTemplateParameters, IDictionary<string, LogEventPropertyValue> properties)
        {
            if (messageTemplateParameters == null || messageTemplateParameters.Length == 0)
            {
                if (messageTemplate.NamedProperties != null || messageTemplate.PositionalProperties != null)
                    SelfLog.WriteLine("Required properties not provided for: {0}", messageTemplate);

                return;
            }

            if (messageTemplate.PositionalProperties != null)
            {
                ConstructPositionalProperties(messageTemplate, messageTemplateParameters, properties);
            }
            else
            {
                ConstructNamedProperties(messageTemplate, messageTemplateParameters, properties);
            }
        }

        void ConstructPositionalProperties(MessageTemplate template, object[] messageTemplateParameters, IDictionary<string, LogEventPropertyValue> properties)
        {
            var positionalProperties = template.PositionalProperties;

            if (positionalProperties.Length != messageTemplateParameters.Length)
                SelfLog.WriteLine("Positional property count does not match parameter count: {0}", template);

            var result = new EventProperty[messageTemplateParameters.Length];
            foreach (var property in positionalProperties)
            {
                if (property.TryGetPositionalValue(out var position))
                {
                    if (position < 0 || position >= messageTemplateParameters.Length)
                        SelfLog.WriteLine("Unassigned positional value {0} in: {1}", position, template);
                    else
                        result[position] = ConstructProperty(property, messageTemplateParameters[position]);
                }
            }

            for (var i = 0; i < result.Length; ++i)
            {
                if (!result[i].Equals(EventProperty.None))
                {
                    properties.Add(result[i].Name, result[i].Value);
                }
            }
        }

        void ConstructNamedProperties(MessageTemplate template, object[] messageTemplateParameters, IDictionary<string, LogEventPropertyValue> properties)
        {
            var namedProperties = template.NamedProperties;
            if (namedProperties == null)
            {
                return;
            }

            var matchedRun = namedProperties.Length;
            if (namedProperties.Length != messageTemplateParameters.Length)
            {
                matchedRun = Math.Min(namedProperties.Length, messageTemplateParameters.Length);
                SelfLog.WriteLine("Named property count does not match parameter count: {0}", template);
            }

            for (var i = 0; i < matchedRun; ++i)
            {
                var property = template.NamedProperties[i];
                var value = messageTemplateParameters[i];
                properties.Add(property.PropertyName, _valueConverter.CreatePropertyValue(value, property.Destructuring));
            }

            for (var i = matchedRun; i < messageTemplateParameters.Length; ++i)
            {
                var value = _valueConverter.CreatePropertyValue(messageTemplateParameters[i]);
                properties.Add(UnmatchedNames[i], value);
            }
        }

        EventProperty ConstructProperty(PropertyToken propertyToken, object value)
        {
            return new EventProperty(
                        propertyToken.PropertyName,
                        _valueConverter.CreatePropertyValue(value, propertyToken.Destructuring));
        }
    }
}
