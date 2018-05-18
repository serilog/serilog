// Copyright 2017 Serilog Contributors
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
using Serilog.Events;
using Serilog.Formatting.Json;

namespace Serilog.Formatting.Display
{
    static class PropertiesOutputFormat
    {
        static readonly JsonValueFormatter JsonValueFormatter = new JsonValueFormatter("$type");

        public static void Render(MessageTemplate template, IReadOnlyDictionary<string, LogEventPropertyValue> properties, MessageTemplate outputTemplate, TextWriter output, string format, IFormatProvider formatProvider = null)
        {
            if (format?.Contains("j") == true)
            {
                var sv = new StructureValue(properties
                    .Where(kvp => !(TemplateContainsPropertyName(template, kvp.Key) ||
                                    TemplateContainsPropertyName(outputTemplate, kvp.Key)))
                    .Select(kvp => new LogEventProperty(kvp.Key, kvp.Value)));
                JsonValueFormatter.Format(sv, output);
                return;
            }

            output.Write("{ ");

            var delim = "";
            foreach (var kvp in properties)
            {
                if (TemplateContainsPropertyName(template, kvp.Key) ||
                    TemplateContainsPropertyName(outputTemplate, kvp.Key))
                {
                    continue;
                }

                output.Write(delim);
                delim = ", ";
                output.Write(kvp.Key);
                output.Write(": ");
                kvp.Value.Render(output, null, formatProvider);
            }

            output.Write(" }");
        }

        static bool TemplateContainsPropertyName(MessageTemplate template, string propertyName)
        {
            if (template.PositionalProperties != null)
            {
                for (var i = 0; i < template.PositionalProperties.Length; i++)
                {
                    var token = template.PositionalProperties[i];
                    if (token.PropertyName == propertyName)
                    {
                        return true;
                    }
                }

                return false;
            }

            if (template.NamedProperties != null)
            {
                for (var i = 0; i < template.NamedProperties.Length; i++)
                {
                    var namedProperty = template.NamedProperties[i];
                    if (namedProperty.PropertyName == propertyName)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
