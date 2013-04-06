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
using Serilog.Sinks.DumpFile;
using Serilog.Sinks.File;
using Serilog.Sinks.RollingFile;
using Serilog.Sinks.SystemConsole;
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
        /// Writes log events to <see cref="System.Console"/>.
        /// </summary>
        /// <param name="restrictedToMinimumLevel">The minimum level for
        /// events passed through the sink.</param>
        /// <param name="outputTemplate">A message template describing the format used to write to the sink.
        /// the default is "{TimeStamp} [{Level}] {Message:l}{NewLine:l}{Exception:l}".</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public LoggerConfiguration Console(
            LogEventLevel restrictedToMinimumLevel = LogEventLevel.Minimum,
            string outputTemplate = DefaultOutputTemplate)
        {
            var formatter = new MessageTemplateTextFormatter(outputTemplate);
            return Sink(new ConsoleSink(formatter), restrictedToMinimumLevel);
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
        /// Write log events in a simple text dump format to the specified file.
        /// </summary>
        /// <param name="path">Path to the dump file.</param>
        /// <param name="restrictedToMinimumLevel">The minimum level for
        /// events passed through the sink.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public LoggerConfiguration DumpFile(string path, LogEventLevel restrictedToMinimumLevel = LogEventLevel.Minimum)
        {
            return Sink(new DumpFileSink(path), restrictedToMinimumLevel);
        }

        /// <summary>
        /// Write log events to the specified file.
        /// </summary>
        /// <param name="path">Path to the file.</param>
        /// <param name="restrictedToMinimumLevel">The minimum level for
        /// events passed through the sink.</param>
        /// <param name="outputTemplate">A message template describing the format used to write to the sink.
        /// the default is "{TimeStamp} [{Level}] {Message:l}{NewLine:l}{Exception:l}".</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public LoggerConfiguration File(
            string path,
            LogEventLevel restrictedToMinimumLevel = LogEventLevel.Minimum,
            string outputTemplate = DefaultOutputTemplate)
        {
            var formatter = new MessageTemplateTextFormatter(outputTemplate);
            return Sink(new FileSink(path, formatter), restrictedToMinimumLevel);
        }

        /// <summary>
        /// Write log events to a series of files. Each file will be named according to
        /// the date of the first log entry written to it. Only simple date-based rolling is
        /// currently supported.
        /// </summary>
        /// <param name="pathFormat">.NET format string describing the location of the log files,
        /// with {0} in the place of the file date. E.g. "Logs\myapp-{0}.log" will result in log
        /// files such as "Logs\myapp-2013-10-20.log", "Logs\myapp-2013-10-21.log" and so on.</param>
        /// <param name="restrictedToMinimumLevel">The minimum level for
        /// events passed through the sink.</param>
        /// <param name="outputTemplate">A message template describing the format used to write to the sink.
        /// the default is "{TimeStamp} [{Level}] {Message:l}{NewLine:l}{Exception:l}".</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public LoggerConfiguration RollingFile(
            string pathFormat,
            LogEventLevel restrictedToMinimumLevel = LogEventLevel.Minimum,
            string outputTemplate = DefaultOutputTemplate)
        {
            var formatter = new MessageTemplateTextFormatter(outputTemplate);
            return Sink(new RollingFileSink(pathFormat, formatter), restrictedToMinimumLevel);
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
