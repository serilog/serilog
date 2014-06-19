// Copyright 2014 Serilog Contributors
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
using Serilog.Parsing;

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
            if (outputTemplate == null) throw new ArgumentNullException("outputTemplate");
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
            if (logEvent == null) throw new ArgumentNullException("logEvent");
            if (output == null) throw new ArgumentNullException("output");

            // This could be lazier: the output properties include
            // everything from the log event, but often we won't need any more than
            // just the standard timestamp/message etc.
            var outputProperties = OutputProperties.GetOutputProperties(logEvent);
            
            foreach (var token in _outputTemplate.Tokens)
            {
                var pt = token as PropertyToken;
                if (pt == null)
                {
                    token.Render(outputProperties, output, _formatProvider);
                    continue;
                }

                // First variation from normal rendering - if a property is missing,
                // don't render anything (message templates render the raw token here).
                LogEventPropertyValue propertyValue;
                if (!outputProperties.TryGetValue(pt.PropertyName, out propertyValue))
                    continue;

                // Second variation; if the value is a scalar string, use literal
                // rendering and support some additional formats: 'u' for uppercase
                // and 'w' for lowercase.
                var sv = propertyValue as ScalarValue;
                if (sv != null && sv.Value is string)
                {
                    var overridden = new Dictionary<string, LogEventPropertyValue>
                    {
                        { pt.PropertyName, new LiteralStringValue((string) sv.Value) }
                    };

                    token.Render(overridden, output, _formatProvider);
                }
                else
                {
                    token.Render(outputProperties, output, _formatProvider);
                }
            }
        }
    }
}
