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
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using Serilog.Configuration;
using Serilog.Debugging;
using Serilog.Enrichers;
using Serilog.Events;
using Serilog.Formatting.Display;
using Serilog.Formatting.Raw;
using Serilog.Settings.AppSettings;
using Serilog.Sinks.DiagnosticTrace;
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
        const string DefaultOutputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}";
        const string DefaultConsoleOutputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {Message}{NewLine}{Exception}";
        const long DefaultFileSizeLimitBytes = 1L * 1024 * 1024 * 1024;
        const int DefaultRetainedFileCountLimit = 31; // A long month of logs

        /// <summary>
        /// Writes log events to <see cref="System.Console"/>.
        /// </summary>
        /// <param name="sinkConfiguration">Logger sink configuration.</param>
        /// <param name="restrictedToMinimumLevel">The minimum level for
        /// events passed through the sink.</param>
        /// <param name="outputTemplate">A message template describing the format used to write to the sink.
        /// the default is "{Timestamp} [{Level}] {Message}{NewLine}{Exception}".</param>
        /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public static LoggerConfiguration Console(
            this LoggerSinkConfiguration sinkConfiguration,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
            string outputTemplate = DefaultConsoleOutputTemplate,
            IFormatProvider formatProvider = null)
        {
            if (sinkConfiguration == null) throw new ArgumentNullException("sinkConfiguration");
            if (outputTemplate == null) throw new ArgumentNullException("outputTemplate");
            var formatter = new MessageTemplateTextFormatter(outputTemplate, formatProvider);
            return sinkConfiguration.Sink(new ConsoleSink(formatter), restrictedToMinimumLevel);
        }

        /// <summary>
        /// Writes log events to <see cref="System.Console"/>, using color to differentiate
        /// between levels.
        /// </summary>
        /// <param name="sinkConfiguration">Logger sink configuration.</param>
        /// <param name="restrictedToMinimumLevel">The minimum level for
        /// events passed through the sink.</param>
        /// <param name="outputTemplate">A message template describing the format used to write to the sink.
        /// the default is "{Timestamp} [{Level}] {Message}{NewLine}{Exception}".</param>
        /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public static LoggerConfiguration ColoredConsole(
            this LoggerSinkConfiguration sinkConfiguration,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
            string outputTemplate = DefaultConsoleOutputTemplate,
            IFormatProvider formatProvider = null)
        {
            if (sinkConfiguration == null) throw new ArgumentNullException("sinkConfiguration");
            if (outputTemplate == null) throw new ArgumentNullException("outputTemplate");
            return sinkConfiguration.Sink(new ColoredConsoleSink(outputTemplate, formatProvider), restrictedToMinimumLevel);
        }

        /// <summary>
        /// Write log events in a simple text dump format to the specified file.
        /// </summary>
        /// <param name="sinkConfiguration">Logger sink configuration.</param>
        /// <param name="path">Path to the dump file.</param>
        /// <param name="restrictedToMinimumLevel">The minimum level for
        /// events passed through the sink.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        [Obsolete("Please use WriteTo.Sink(new FileSink(path, new RawFormatter(), null)) instead", true), EditorBrowsable(EditorBrowsableState.Never)]
        public static LoggerConfiguration DumpFile(
            this LoggerSinkConfiguration sinkConfiguration,
            string path,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum)
        {
            if (sinkConfiguration == null) throw new ArgumentNullException("sinkConfiguration");
            if (path == null) throw new ArgumentNullException("path");
            return sinkConfiguration.Sink(new FileSink(path, new RawFormatter(), null), restrictedToMinimumLevel);
        }

        /// <summary>
        /// Write log events to the specified file.
        /// </summary>
        /// <param name="sinkConfiguration">Logger sink configuration.</param>
        /// <param name="path">Path to the file.</param>
        /// <param name="restrictedToMinimumLevel">The minimum level for
        /// events passed through the sink.</param>
        /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
        /// <param name="outputTemplate">A message template describing the format used to write to the sink.
        /// the default is "{Timestamp} [{Level}] {Message}{NewLine}{Exception}".</param>
        /// <param name="fileSizeLimitBytes">The maximum size, in bytes, to which a log file will be allowed to grow.
        /// For unrestricted growth, pass null. The default is 1 GB.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        /// <remarks>The file will be written using the UTF-8 character set.</remarks>
        public static LoggerConfiguration File(
            this LoggerSinkConfiguration sinkConfiguration,
            string path,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
            string outputTemplate = DefaultOutputTemplate,
            IFormatProvider formatProvider = null,
            long? fileSizeLimitBytes = DefaultFileSizeLimitBytes)
        {
            if (sinkConfiguration == null) throw new ArgumentNullException("sinkConfiguration");
            if (outputTemplate == null) throw new ArgumentNullException("outputTemplate");
            var formatter = new MessageTemplateTextFormatter(outputTemplate, formatProvider);
            
            FileSink sink;
            try
            {
                sink = new FileSink(path, formatter, fileSizeLimitBytes);
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                SelfLog.WriteLine("Unable to open file sink for {0}: {1}", path, ex);
                return sinkConfiguration.Sink(new NullSink());
            }

            return sinkConfiguration.Sink(sink, restrictedToMinimumLevel);
        }

        /// <summary>
        /// Write log events to a series of files. Each file will be named according to
        /// the date of the first log entry written to it. Only simple date-based rolling is
        /// currently supported.
        /// </summary>
        /// <param name="sinkConfiguration">Logger sink configuration.</param>
        /// <param name="pathFormat">String describing the location of the log files,
        /// with {Date} in the place of the file date. E.g. "Logs\myapp-{Date}.log" will result in log
        /// files such as "Logs\myapp-2013-10-20.log", "Logs\myapp-2013-10-21.log" and so on.</param>
        /// <param name="restrictedToMinimumLevel">The minimum level for
        /// events passed through the sink.</param>
        /// <param name="outputTemplate">A message template describing the format used to write to the sink.
        /// the default is "{Timestamp} [{Level}] {Message}{NewLine}{Exception}".</param>
        /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
        /// <param name="fileSizeLimitBytes">The maximum size, in bytes, to which any single log file will be allowed to grow.
        /// For unrestricted growth, pass null. The default is 1 GB.</param>
        /// <param name="retainedFileCountLimit">The maximum number of log files that will be retained,
        /// including the current log file. For unlimited retention, pass null. The default is 31.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        /// <remarks>The file will be written using the UTF-8 character set.</remarks>
        public static LoggerConfiguration RollingFile(
            this LoggerSinkConfiguration sinkConfiguration,
            string pathFormat,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
            string outputTemplate = DefaultOutputTemplate,
            IFormatProvider formatProvider = null,
            long? fileSizeLimitBytes = DefaultFileSizeLimitBytes,
            int? retainedFileCountLimit = DefaultRetainedFileCountLimit)
        {
            if (sinkConfiguration == null) throw new ArgumentNullException("sinkConfiguration");
            if (outputTemplate == null) throw new ArgumentNullException("outputTemplate");
            var formatter = new MessageTemplateTextFormatter(outputTemplate, formatProvider);
            var sink = new RollingFileSink(pathFormat, formatter, fileSizeLimitBytes, retainedFileCountLimit);
            return sinkConfiguration.Sink(sink, restrictedToMinimumLevel);
        }

        /// <summary>
        /// Write log events to the <see cref="System.Diagnostics.Trace"/>.
        /// </summary>
        /// <param name="sinkConfiguration">Logger sink configuration.</param>
        /// <param name="restrictedToMinimumLevel">The minimum level for
        /// events passed through the sink.</param>
        /// <param name="outputTemplate">A message template describing the format used to write to the sink.
        /// the default is "{Timestamp} [{Level}] {Message}{NewLine}{Exception}".</param>
        /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public static LoggerConfiguration Trace(
            this LoggerSinkConfiguration sinkConfiguration,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
            string outputTemplate = DefaultOutputTemplate,
            IFormatProvider formatProvider = null)
        {
            if (sinkConfiguration == null) throw new ArgumentNullException("sinkConfiguration");
            if (outputTemplate == null) throw new ArgumentNullException("outputTemplate");
            var formatter = new MessageTemplateTextFormatter(outputTemplate, formatProvider);
            return sinkConfiguration.Sink(new DiagnosticTraceSink(formatter), restrictedToMinimumLevel);
        }

        /// <summary>
        /// Enrich log events with properties from <see cref="Context.LogContext"/>.
        /// </summary>
        /// <param name="enrichmentConfiguration">Logger enrichment configuration.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static LoggerConfiguration FromLogContext(
            this LoggerEnrichmentConfiguration enrichmentConfiguration)
        {
            if (enrichmentConfiguration == null) throw new ArgumentNullException("enrichmentConfiguration");
            return enrichmentConfiguration.With<LogContextEnricher>();
        }

        /// <summary>
        /// Enrich log events with a ThreadId property containing the current <see cref="Thread.ManagedThreadId"/>.
        /// </summary>
        /// <param name="enrichmentConfiguration">Logger enrichment configuration.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static LoggerConfiguration WithThreadId(
            this LoggerEnrichmentConfiguration enrichmentConfiguration)
        {
            if (enrichmentConfiguration == null) throw new ArgumentNullException("enrichmentConfiguration");
            return enrichmentConfiguration.With<ThreadIdEnricher>();
        }

        /// <summary>
        /// Enrich log events with a ProcessId property containing the current <see cref="Process.Id"/>.
        /// </summary>
        /// <param name="enrichmentConfiguration">Logger enrichment configuration.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public static LoggerConfiguration WithProcessId(
           this LoggerEnrichmentConfiguration enrichmentConfiguration)
        {
            if (enrichmentConfiguration == null) throw new ArgumentNullException("enrichmentConfiguration");
            return enrichmentConfiguration.With<ProcessIdEnricher>();
        }

        /// <summary>
        /// Enrich log events with a MachineName property containing the current <see cref="Environment.MachineName"/>.
        /// </summary>
        /// <param name="enrichmentConfiguration">Logger enrichment configuration.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public static LoggerConfiguration WithMachineName(
           this LoggerEnrichmentConfiguration enrichmentConfiguration)
        {
            if (enrichmentConfiguration == null) throw new ArgumentNullException("enrichmentConfiguration");
            return enrichmentConfiguration.With<MachineNameEnricher>();
        }

        /// <summary>
        /// Reads the &lt;appSettings&gt; element of App.config or Web.config, searching for for keys
        /// that look like: <code>serilog:*</code>, which are used to configure
        /// the logger. To add a sink, use a key like <code>serilog:write-to:File.path</code> for
        /// each parameter to the sink's configuration method. To add an additional assembly
        /// containing sinks, use <code>serilog:using</code>. To set the level use 
        /// <code>serilog:minimum-level</code>.
        /// </summary>
        /// <param name="settingConfiguration">Logger setting configuration.</param>
        /// <param name="exposeSettings">Action allowing changes to the Serilog settings read from the App.config.</param>
        /// <returns>An object allowing configuration to continue.</returns>
        public static LoggerConfiguration AppSettings(
            this LoggerSettingsConfiguration settingConfiguration, Action<Dictionary<string, string>> exposeSettings = null)
        {
            if (settingConfiguration == null) throw new ArgumentNullException("settingConfiguration");
            return settingConfiguration.Settings(new AppSettingsSettings(exposeSettings));
        }
    }
}
