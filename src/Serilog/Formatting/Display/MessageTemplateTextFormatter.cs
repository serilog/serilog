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
using System.IO;
using Serilog.Events;
using Serilog.Parsing;
using Serilog.Rendering;

namespace Serilog.Formatting.Display
{
    /// <summary>
    /// A <see cref="ITextFormatter"/> that supports the Serilog
    /// message template format. Formatting log events for display
    /// has a different set of requirements and expectations from
    /// rendering the data within them. To meet this, the formatter
    /// overrides some behavior: First, strings are always output
    /// as literals (not quoted) unless some other format is applied
    /// to them. Second, tokens without matching properties are skipped
    /// rather than being written as raw text.
    /// </summary>
    public class MessageTemplateTextFormatter : ITextFormatter
    {
        readonly IFormatProvider _formatProvider;
        readonly MessageTemplate _outputTemplate;

        /// <summary>
        /// Construct a <see cref="MessageTemplateTextFormatter"/>.
        /// </summary>
        /// <param name="outputTemplate">A message template describing the
        /// output messages.</param>
        /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
        public MessageTemplateTextFormatter(string outputTemplate, IFormatProvider formatProvider)
        {
            if (outputTemplate == null) throw new ArgumentNullException(nameof(outputTemplate));
            _outputTemplate = new MessageTemplateParser().Parse(outputTemplate);
            _formatProvider = formatProvider;
        }

        /// <summary>
        /// Format the log event into the output.
        /// </summary>
        /// <param name="logEvent">The event to format.</param>
        /// <param name="output">The output.</param>
        public void Format(LogEvent logEvent, TextWriter output)
        {
            if (logEvent == null) throw new ArgumentNullException(nameof(logEvent));
            if (output == null) throw new ArgumentNullException(nameof(output));

            foreach (var token in _outputTemplate.Tokens)
            {
                if (token is TextToken tt)
                {
                    MessageTemplateRenderer.RenderTextToken(tt, output);
                    continue;
                }

                var pt = (PropertyToken)token;
                if (pt.PropertyName == OutputProperties.LevelPropertyName)
                {
                    var moniker = LevelOutputFormat.GetLevelMoniker(logEvent.Level, pt.Format);
                    Padding.Apply(output, moniker, pt.Alignment);
                }
                else if (pt.PropertyName == OutputProperties.NewLinePropertyName)
                {
                    Padding.Apply(output, Environment.NewLine, pt.Alignment);
                }
                else if (pt.PropertyName == OutputProperties.ExceptionPropertyName)
                {
                    var exception = logEvent.Exception == null ? "" : logEvent.Exception + Environment.NewLine;
                    Padding.Apply(output, exception, pt.Alignment);
                }
                else
                {
                    // In this block, `writer` may be used to buffer output so that
                    // padding can be applied.
                    var writer = pt.Alignment.HasValue ? new StringWriter() : output;

                    if (pt.PropertyName == OutputProperties.MessagePropertyName)
                    {
                        MessageTemplateRenderer.Render(logEvent.MessageTemplate, logEvent.Properties, writer, pt.Format, _formatProvider);
                    }
                    else if (pt.PropertyName == OutputProperties.TimestampPropertyName)
                    {
                        ScalarValue.Render(logEvent.Timestamp, writer, pt.Format, _formatProvider);
                    }
                    else if (pt.PropertyName == OutputProperties.PropertiesPropertyName)
                    {
                        PropertiesOutputFormat.Render(logEvent.MessageTemplate, logEvent.Properties, _outputTemplate, writer, pt.Format, _formatProvider);
                    }
                    else
                    {
                        // If a property is missing, don't render anything (message templates render the raw token here).
                        LogEventPropertyValue propertyValue;
                        if (!logEvent.Properties.TryGetValue(pt.PropertyName, out propertyValue))
                            continue;

                        // If the value is a scalar string, support some additional formats: 'u' for uppercase
                        // and 'w' for lowercase.
                        var sv = propertyValue as ScalarValue;
                        if (sv?.Value is string literalString)
                        {
                            var cased = Casing.Format(literalString, pt.Format);
                            writer.Write(cased);
                        }
                        else
                        {
                            propertyValue.Render(writer, pt.Format, _formatProvider);
                        }
                    }

                    if (pt.Alignment.HasValue)
                        Padding.Apply(output, ((StringWriter)writer).ToString(), pt.Alignment);
                }
            }
        }
    }
}
