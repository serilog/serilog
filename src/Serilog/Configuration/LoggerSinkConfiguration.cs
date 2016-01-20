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
using System.ComponentModel;
using System.IO;
using Serilog.Core;
using Serilog.Core.Sinks;
using Serilog.Debugging;
using Serilog.Events;
using Serilog.Formatting.Display;
using Serilog.Sinks.IOTextWriter;

namespace Serilog.Configuration
{
    /// <summary>
    /// Controls sink configuration.
    /// </summary>
    public class LoggerSinkConfiguration
    {
        readonly LoggerConfiguration _loggerConfiguration;
        readonly Action<ILogEventSink> _addSink;

        const string DefaultOutputTemplate = "{Timestamp} [{Level}] {Message}{NewLine}{Exception}";

        internal LoggerSinkConfiguration(LoggerConfiguration loggerConfiguration, Action<ILogEventSink> addSink)
        {
            if (loggerConfiguration == null) throw new ArgumentNullException(nameof(loggerConfiguration));
            if (addSink == null) throw new ArgumentNullException(nameof(addSink));
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
        /// <remarks>Provided for binary compatibility for earlier versions,
        /// should be removed in 2.0. Not marked obsolete because warnings
        /// would be syntactically annoying to avoid.</remarks>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public LoggerConfiguration Sink(
            ILogEventSink logEventSink,
            LogEventLevel restrictedToMinimumLevel)
        {
            return Sink(logEventSink, restrictedToMinimumLevel, null);
        }

        /// <summary>
        /// Write log events to the specified <see cref="ILogEventSink"/>.
        /// </summary>
        /// <param name="logEventSink">The sink.</param>
        /// <param name="restrictedToMinimumLevel">The minimum level for
        /// events passed through the sink. Ignored when <paramref name="levelSwitch"/> is specified.</param>
        /// <param name="levelSwitch">A switch allowing the pass-through minimum level
        /// to be changed at runtime.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public LoggerConfiguration Sink(
            ILogEventSink logEventSink,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
            // ReSharper disable once MethodOverloadWithOptionalParameter
            LoggingLevelSwitch levelSwitch = null)
        {
            var sink = logEventSink;
            if (levelSwitch != null)
            {
                if (restrictedToMinimumLevel != LevelAlias.Minimum)
                    SelfLog.WriteLine("Sink {0} was configured with both a level switch and minimum level '{1}'; the minimum level will be ignored and the switch level used", sink, restrictedToMinimumLevel);

                sink = new RestrictedSink(sink, levelSwitch);
            }
            else if (restrictedToMinimumLevel > LevelAlias.Minimum)
            {
                sink = new RestrictedSink(sink, new LoggingLevelSwitch(restrictedToMinimumLevel));
            }

            _addSink(sink);
            return _loggerConfiguration;
        }

        /// <summary>
        /// Write log events to the specified <see cref="ILogEventSink"/>.
        /// </summary>
        /// <typeparam name="TSink">The sink.</typeparam>
        /// <param name="restrictedToMinimumLevel">The minimum level for
        /// events passed through the sink. Ignored when <paramref name="levelSwitch"/> is specified.</param>
        /// <param name="levelSwitch">A switch allowing the pass-through minimum level
        /// to be changed at runtime.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public LoggerConfiguration Sink<TSink>(
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
            LoggingLevelSwitch levelSwitch = null)
            where TSink : ILogEventSink, new()
        {
            return Sink(new TSink(), restrictedToMinimumLevel, levelSwitch);
        }

        /// <summary>
        /// Write log events to the provided <see cref="TextWriter"/>.
        /// </summary>
        /// <param name="textWriter">The text writer to write log events to.</param>
        /// <param name="outputTemplate">Message template describing the output format.</param>
        /// <param name="restrictedToMinimumLevel">The minimum level for
        /// events passed through the sink. Ignored when <paramref name="levelSwitch"/> is specified.</param>
        /// <param name="levelSwitch">A switch allowing the pass-through minimum level
        /// to be changed at runtime.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public LoggerConfiguration TextWriter(
            TextWriter textWriter,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
            string outputTemplate = DefaultOutputTemplate,
            IFormatProvider formatProvider = null,
            LoggingLevelSwitch levelSwitch = null)
        {
            if (textWriter == null) throw new ArgumentNullException(nameof(textWriter));
            if (outputTemplate == null) throw new ArgumentNullException(nameof(outputTemplate));

            var formatter = new MessageTemplateTextFormatter(outputTemplate, formatProvider);
            var sink = new TextWriterSink(textWriter, formatter);
            return Sink(sink, restrictedToMinimumLevel, levelSwitch);
        }

        /// <summary>
        /// Write log events to a sub-logger, where further processing may occur. Events through
        /// the sub-logger will be constrained by filters and enriched by enrichers that are
        /// active in the parent. A sub-logger cannot be used to log at a more verbose level, but
        /// a less verbose level is possible.
        /// </summary>
        /// <param name="configureLogger">An action that configures the sub-logger.</param>
        /// <param name="restrictedToMinimumLevel">The minimum level for
        /// events passed through the sink. Ignored when <paramref name="levelSwitch"/> is specified.</param>
        /// <param name="levelSwitch">A switch allowing the pass-through minimum level
        /// to be changed at runtime.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public LoggerConfiguration Logger(
            Action<LoggerConfiguration> configureLogger,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
            LoggingLevelSwitch levelSwitch = null)
        {
            if (configureLogger == null) throw new ArgumentNullException(nameof(configureLogger));
            var lc = new LoggerConfiguration();
            configureLogger(lc);
            return Sink(new SecondaryLoggerSink(lc.CreateLogger(), attemptDispose: true), restrictedToMinimumLevel);
        }

        /// <summary>
        /// Write log events to a sub-logger, where further processing may occur. Events through
        /// the sub-logger will be constrained by filters and enriched by enrichers that are
        /// active in the parent. A sub-logger cannot be used to log at a more verbose level, but
        /// a less verbose level is possible.
        /// </summary>
        /// <param name="logger">The sub-logger. This will <em>not</em> be shut down automatically when the
        /// parent logger is disposed.</param>
        /// <param name="restrictedToMinimumLevel">The minimum level for
        /// events passed through the sink.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public LoggerConfiguration Logger(
            ILogger logger,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum)
        {
            if (logger == null) throw new ArgumentNullException(nameof(logger));
            return Sink(new SecondaryLoggerSink(logger, attemptDispose: false), restrictedToMinimumLevel);
        } 
    }
}
