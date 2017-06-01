// Copyright 2013-2017 Serilog Contributors
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
using Serilog.Events;
using Serilog.Formatting.Json;
using Serilog.Parsing;

namespace Serilog.Rendering
{
    static class MessageTemplateRenderer
    {
        static JsonValueFormatter JsonValueFormatter = new JsonValueFormatter("$type");

        public static void Render(MessageTemplate messageTemplate, IReadOnlyDictionary<string, LogEventPropertyValue> properties, TextWriter output, string format = null, IFormatProvider formatProvider = null)
        {
            if (messageTemplate == null) throw new ArgumentNullException(nameof(messageTemplate));
            if (properties == null) throw new ArgumentNullException(nameof(properties));
            if (output == null) throw new ArgumentNullException(nameof(output));

            bool isLiteral = false, isJson = false;

            if (format != null && (format.Length == 1 || format.Length == 2))
            {
                isLiteral = format[0] == 'l' || format[1] == 'l';
                isJson = format[0] == 'j' || format[1] == 'j';
            }

            foreach (var token in messageTemplate.TokenArray)
            {
                if (token is TextToken tt)
                {
                    RenderTextToken(tt, output);
                }
                else
                {
                    var pt = (PropertyToken) token;
                    RenderPropertyToken(pt, properties, output, formatProvider, isLiteral, isJson);
                }
            }
        }

        public static void RenderTextToken(TextToken tt, TextWriter output)
        {
            output.Write(tt.Text);
        }

        public static void RenderPropertyToken(PropertyToken pt, IReadOnlyDictionary<string, LogEventPropertyValue> properties, TextWriter output, IFormatProvider formatProvider, bool isLiteral, bool isJson)
        {
            LogEventPropertyValue propertyValue;
            if (!properties.TryGetValue(pt.PropertyName, out propertyValue))
            {
                output.Write(pt.RawText);
                return;
            }

            if (!pt.Alignment.HasValue)
            {
                RenderValue(propertyValue, isLiteral, isJson, output, pt.Format, formatProvider);
                return;
            }

            var valueOutput = new StringWriter();
            RenderValue(propertyValue, isLiteral, isJson, valueOutput, pt.Format, formatProvider);
            var value = valueOutput.ToString();

            if (value.Length >= pt.Alignment.Value.Width)
            {
                output.Write(value);
                return;
            }

            Padding.Apply(output, value, pt.Alignment.Value);
        }

        static void RenderValue(LogEventPropertyValue propertyValue, bool literal, bool json, TextWriter output, string format, IFormatProvider formatProvider)
        {
            if (literal && propertyValue is ScalarValue sv && sv.Value is string str)
            {
                output.Write(str);
            }
            else if (json)
            {
                JsonValueFormatter.Format(propertyValue, output);
            }
            else
            {
                propertyValue.Render(output, format, formatProvider);
            }
        }
    }
}
