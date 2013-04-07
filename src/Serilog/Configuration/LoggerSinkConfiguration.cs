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
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting.Display;
using Serilog.Sinks.Trace;

namespace Serilog.Configuration
{
    /// <summary>
    /// Controls sink configuration.
    /// </summary>
    public class LoggerSinkConfiguration
    {
        readonly LoggerConfiguration _loggerConfiguration;
        readonly Action<ILogEventSink> _addSink;

        const string DefaultOutputTemplate = "{TimeStamp} [{Level}] {Message:l}{NewLine:l}{Exception:l}";

        internal LoggerSinkConfiguration(LoggerConfiguration loggerConfiguration, Action<ILogEventSink> addSink)
        {
            if (loggerConfiguration == null) throw new ArgumentNullException("loggerConfiguration");
            if (addSink == null) throw new ArgumentNullException("addSink");
            _loggerConfiguration = loggerConfiguration;
            _addSink = addSink;
        }

        /// <summary>
        /// Write log events to the specified <see cref="ILogEventSink"/>.
        /// </summary>
        /// <param name="logEventSink">The sink.</param>
        /// <param name="restrictedToMinimumLevel">The minimum level for
        /// events passed through the sink.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public LoggerConfiguration Sink(ILogEventSink logEventSink, LogEventLevel restrictedToMinimumLevel = LogEventLevel.Minimum)
        {
            var sink = logEventSink;
            if (restrictedToMinimumLevel > LogEventLevel.Minimum)
                sink = new RestrictedSink(sink, restrictedToMinimumLevel);

            _addSink(sink);
            return _loggerConfiguration;
        }

        /// <summary>
        /// Write log events to the <see cref="System.Diagnostics.Trace"/>.
        /// </summary>
        /// <param name="restrictedToMinimumLevel">The minimum level for
        /// events passed through the sink.</param>
        /// <param name="outputTemplate">A message template describing the format used to write to the sink.
        /// the default is "{TimeStamp} [{Level}] {Message:l}{NewLine:l}{Exception:l}".</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public LoggerConfiguration Trace(
            LogEventLevel restrictedToMinimumLevel = LogEventLevel.Minimum,
            string outputTemplate = DefaultOutputTemplate)
        {
            var formatter = new MessageTemplateTextFormatter(outputTemplate);
            return Sink(new DiagnosticTraceSink(formatter), restrictedToMinimumLevel);
        }
    }
}
