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
using System.IO;
using Serilog.Events;
using Serilog.Parsing;

namespace Serilog.Formatting.Display
{
    /// <summary>
    /// A <see cref="ITextFormatter"/> that supports the Serilog
    /// message template format.
    /// </summary>
    public class MessageTemplateTextFormatter : ITextFormatter
    {
        private readonly MessageTemplate _outputTemplate;

        /// <summary>
        /// Construct a <see cref="MessageTemplateTextFormatter"/>.
        /// </summary>
        /// <param name="outputTemplate">A message template describing the
        /// output messages.</param>
        public MessageTemplateTextFormatter(string outputTemplate)
        {
            if (outputTemplate == null) throw new ArgumentNullException("outputTemplate");
            _outputTemplate = new MessageTemplateParser().Parse(outputTemplate);
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
            var outputProperties = OutputProperties.GetOutputProperties(logEvent);
            _outputTemplate.Render(outputProperties, output);            
        }
    }
}
