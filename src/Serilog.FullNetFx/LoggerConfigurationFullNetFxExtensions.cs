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
using Serilog.Configuration;
using Serilog.Events;
using Serilog.Formatting.Display;
using Serilog.Sinks.DiagnosticTrace;
using Serilog.Sinks.DumpFile;
using Serilog.Sinks.IOFile;
using Serilog.Sinks.RollingFile;
using Serilog.Sinks.SystemConsole;

namespace Serilog
{
    /// <summary>
    /// Extends <see cref="LoggerConfiguration"/> to add Full .NET Framework 
    /// capabilities.
    /// </summary>
    public static class LoggerConfigurationFullNetFxExtensions
    {
        const string DefaultOutputTemplate = "{TimeStamp} [{Level}] {Message:l}{NewLine:l}{Exception:l}";

        /// <summary>
        /// Writes log events to <see cref="System.Console"/>.
        /// </summary>
        /// <param name="sinkConfiguration">Logger sink configuration.</param>
        /// <param name="restrictedToMinimumLevel">The minimum level for
        /// events passed through the sink.</param>
        /// <param name="outputTemplate">A message template describing the format used to write to the sink.
        /// the default is "{TimeStamp} [{Level}] {Message:l}{NewLine:l}{Exception:l}".</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public static LoggerConfiguration Console(
            this LoggerSinkConfiguration sinkConfiguration,
            LogEventLevel restrictedToMinimumLevel = LogEventLevel.Minimum,
            string outputTemplate = DefaultOutputTemplate)
        {
            if (sinkConfiguration == null) throw new ArgumentNullException("sinkConfiguration");
            if (outputTemplate == null) throw new ArgumentNullException("outputTemplate");
            var formatter = new MessageTemplateTextFormatter(outputTemplate);
            return sinkConfiguration.Sink(new ConsoleSink(formatter), restrictedToMinimumLevel);
        }

        /// <summary>
        /// Write log events in a simple text dump format to the specified file.
        /// </summary>
        /// <param name="sinkConfiguration">Logger sink configuration.</param>
        /// <param name="path">Path to the dump file.</param>
        /// <param name="restrictedToMinimumLevel">The minimum level for
        /// events passed through the sink.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public static LoggerConfiguration DumpFile(
            this LoggerSinkConfiguration sinkConfiguration,
            string path, 
            LogEventLevel restrictedToMinimumLevel = LogEventLevel.Minimum)
        {
            if (sinkConfiguration == null) throw new ArgumentNullException("sinkConfiguration");
            if (path == null) throw new ArgumentNullException("path");
            return sinkConfiguration.Sink(new DumpFileSink(path), restrictedToMinimumLevel);
        }

        /// <summary>
        /// Write log events to the specified file.
        /// </summary>
        /// <param name="sinkConfiguration">Logger sink configuration.</param>
        /// <param name="path">Path to the file.</param>
        /// <param name="restrictedToMinimumLevel">The minimum level for
        /// events passed through the sink.</param>
        /// <param name="outputTemplate">A message template describing the format used to write to the sink.
        /// the default is "{TimeStamp} [{Level}] {Message:l}{NewLine:l}{Exception:l}".</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public static LoggerConfiguration File(
            this LoggerSinkConfiguration sinkConfiguration,
            string path,
            LogEventLevel restrictedToMinimumLevel = LogEventLevel.Minimum,
            string outputTemplate = DefaultOutputTemplate)
        {
            if (sinkConfiguration == null) throw new ArgumentNullException("sinkConfiguration");
            if (outputTemplate == null) throw new ArgumentNullException("outputTemplate");
            var formatter = new MessageTemplateTextFormatter(outputTemplate);
            return sinkConfiguration.Sink(new FileSink(path, formatter), restrictedToMinimumLevel);
        }

        /// <summary>
        /// Write log events to a series of files. Each file will be named according to
        /// the date of the first log entry written to it. Only simple date-based rolling is
        /// currently supported.
        /// </summary>
        /// <param name="sinkConfiguration">Logger sink configuration.</param>
        /// <param name="pathFormat">.NET format string describing the location of the log files,
        /// with {0} in the place of the file date. E.g. "Logs\myapp-{0}.log" will result in log
        /// files such as "Logs\myapp-2013-10-20.log", "Logs\myapp-2013-10-21.log" and so on.</param>
        /// <param name="restrictedToMinimumLevel">The minimum level for
        /// events passed through the sink.</param>
        /// <param name="outputTemplate">A message template describing the format used to write to the sink.
        /// the default is "{TimeStamp} [{Level}] {Message:l}{NewLine:l}{Exception:l}".</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public static LoggerConfiguration RollingFile(
            this LoggerSinkConfiguration sinkConfiguration,
            string pathFormat,
            LogEventLevel restrictedToMinimumLevel = LogEventLevel.Minimum,
            string outputTemplate = DefaultOutputTemplate)
        {
            if (sinkConfiguration == null) throw new ArgumentNullException("sinkConfiguration");
            if (outputTemplate == null) throw new ArgumentNullException("outputTemplate");
            var formatter = new MessageTemplateTextFormatter(outputTemplate);
            return sinkConfiguration.Sink(new RollingFileSink(pathFormat, formatter), restrictedToMinimumLevel);
        }

        /// <summary>
        /// Write log events to the <see cref="System.Diagnostics.Trace"/>.
        /// </summary>
        /// <param name="sinkConfiguration">Logger sink configuration.</param>
        /// <param name="restrictedToMinimumLevel">The minimum level for
        /// events passed through the sink.</param>
        /// <param name="outputTemplate">A message template describing the format used to write to the sink.
        /// the default is "{TimeStamp} [{Level}] {Message:l}{NewLine:l}{Exception:l}".</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public static LoggerConfiguration Trace(
            this LoggerSinkConfiguration sinkConfiguration,
            LogEventLevel restrictedToMinimumLevel = LogEventLevel.Minimum,
            string outputTemplate = DefaultOutputTemplate)
        {
            if (sinkConfiguration == null) throw new ArgumentNullException("sinkConfiguration");
            if (outputTemplate == null) throw new ArgumentNullException("outputTemplate");
            var formatter = new MessageTemplateTextFormatter(outputTemplate);
            return sinkConfiguration.Sink(new DiagnosticTraceSink(formatter), restrictedToMinimumLevel);
        }
    }
}
