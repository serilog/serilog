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
using System.IO;
using Serilog.Events;

namespace Serilog.Formatting.Raw
{
    /// <summary>
    /// Formats log events as a raw dump of the message template and properties.
    /// </summary>
    [Obsolete("A JSON-based formatter such as `Serilog.Formatting.Compact.CompactJsonFormatter` is recommended for this task.")]
    public class RawFormatter : ITextFormatter
    {
        /// <summary>
        /// Format the log event into the output.
        /// </summary>
        /// <param name="logEvent">The event to format.</param>
        /// <param name="output">The output.</param>
        public void Format(LogEvent logEvent, TextWriter output)
        {
            output.Write("[" + logEvent.Timestamp + "] " + logEvent.Level + ": \"");
            output.Write(logEvent.MessageTemplate);
            output.WriteLine("\"");
            if (logEvent.Exception != null)
                output.WriteLine(logEvent.Exception);
            foreach (var property in logEvent.Properties)
            {
                output.Write(property.Key + " = ");
                property.Value.Render(output);
                output.WriteLine();
            }
            output.WriteLine();
        }
    }
}
