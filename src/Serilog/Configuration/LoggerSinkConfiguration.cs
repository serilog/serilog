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
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Serilog.Core;
using Serilog.Core.Sinks;
using Serilog.Debugging;
using Serilog.Events;

namespace Serilog.Configuration
{
    /// <summary>
    /// Controls sink configuration.
    /// </summary>
    public class LoggerSinkConfiguration
    {
        readonly LoggerConfiguration _loggerConfiguration;
        readonly Action<ILogEventSink> _addSink;
        readonly Action<LoggerConfiguration> _applyInheritedConfiguration;

        internal LoggerSinkConfiguration(LoggerConfiguration loggerConfiguration, Action<ILogEventSink> addSink, Action<LoggerConfiguration> applyInheritedConfiguration)
        {
            _loggerConfiguration = loggerConfiguration ?? throw new ArgumentNullException(nameof(loggerConfiguration));
            _addSink = addSink ?? throw new ArgumentNullException(nameof(addSink));
            _applyInheritedConfiguration = applyInheritedConfiguration ?? throw new ArgumentNullException(nameof(applyInheritedConfiguration));
        }

        /// <summary>
        /// Write log events to the specified <see cref="ILogEventSink"/>.
        /// </summary>
        /// <param name="logEventSink">The sink.</param>
        /// <param name="restrictedToMinimumLevel">The minimum level for
        /// events passed through the sink.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        /// <remarks>Provided for binary compatibility for earlier versions,
        /// should be removed in 3.0. Not marked obsolete because warnings
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

            _applyInheritedConfiguration(lc);
            configureLogger(lc);

            var subLogger = lc.CreateLogger();
            if (subLogger.HasOverrideMap)
            {
                SelfLog.WriteLine("Minimum level overrides are not supported on sub-loggers " +
                                  "and may be removed completely in a future version.");
            }

            var secondarySink = new SecondaryLoggerSink(subLogger, attemptDispose: true);
            return Sink(secondarySink, restrictedToMinimumLevel, levelSwitch);
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

            if (logger is Logger concreteLogger && concreteLogger.HasOverrideMap)
            {
                SelfLog.WriteLine("Minimum level overrides are not supported on sub-loggers " +
                                  "and may be removed completely in a future version.");
            }

            var secondarySink = new SecondaryLoggerSink(logger, attemptDispose: false);
            return Sink(secondarySink, restrictedToMinimumLevel);
        }

        /// <summary>
        /// Helper method for wrapping sinks.
        /// </summary>
        /// <param name="loggerSinkConfiguration">The parent sink configuration.</param>
        /// <param name="wrapSink">A function that allows for wrapping <see cref="ILogEventSink"/>s
        /// added in <paramref name="configureWrappedSink"/>.</param>
        /// <param name="configureWrappedSink">An action that configures sinks to be wrapped in <paramref name="wrapSink"/>.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        [Obsolete("Please use `LoggerConfiguration.Wrap(loggerSinkConfiguration, wrapSink, configureWrappedSink, restrictedToMinimumLevel, levelSwitch)` instead.")]
        public static LoggerConfiguration Wrap(
            LoggerSinkConfiguration loggerSinkConfiguration,
            Func<ILogEventSink, ILogEventSink> wrapSink,
            Action<LoggerSinkConfiguration> configureWrappedSink)
        {
            return Wrap(loggerSinkConfiguration, wrapSink, configureWrappedSink, LogEventLevel.Verbose, null);
        }

        /// <summary>
        /// Helper method for wrapping sinks.
        /// </summary>
        /// <param name="loggerSinkConfiguration">The parent sink configuration.</param>
        /// <param name="wrapSink">A function that allows for wrapping <see cref="ILogEventSink"/>s
        /// added in <paramref name="configureWrappedSink"/>.</param>
        /// <param name="configureWrappedSink">An action that configures sinks to be wrapped in <paramref name="wrapSink"/>.</param>
        /// <param name="restrictedToMinimumLevel">The minimum level for
        /// events passed through the sink. Ignored when <paramref name="levelSwitch"/> is specified.</param>
        /// <param name="levelSwitch">A switch allowing the pass-through minimum level
        /// to be changed at runtime.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public static LoggerConfiguration Wrap(
            LoggerSinkConfiguration loggerSinkConfiguration,
            Func<ILogEventSink, ILogEventSink> wrapSink,
            Action<LoggerSinkConfiguration> configureWrappedSink,
            LogEventLevel restrictedToMinimumLevel,
            LoggingLevelSwitch levelSwitch)
        {
            if (loggerSinkConfiguration == null) throw new ArgumentNullException(nameof(loggerSinkConfiguration));
            if (wrapSink == null) throw new ArgumentNullException(nameof(wrapSink));
            if (configureWrappedSink == null) throw new ArgumentNullException(nameof(configureWrappedSink));

            var sinksToWrap = new List<ILogEventSink>();

            var capturingConfiguration = new LoggerConfiguration();
            var capturingLoggerSinkConfiguration = new LoggerSinkConfiguration(
                capturingConfiguration,
                sinksToWrap.Add,
                loggerSinkConfiguration._applyInheritedConfiguration);

            // `WriteTo.Sink()` will return the capturing configuration; this ensures chained `WriteTo` gets back
            // to the capturing sink configuration, enabling `WriteTo.X().WriteTo.Y()`.
            capturingConfiguration.WriteTo = capturingLoggerSinkConfiguration;

            configureWrappedSink(capturingLoggerSinkConfiguration);

            if (sinksToWrap.Count == 0)
                return loggerSinkConfiguration._loggerConfiguration;

            var enclosed = sinksToWrap.Count == 1 ?
                sinksToWrap.Single() :
                new SafeAggregateSink(sinksToWrap);

            var wrappedSink = wrapSink(enclosed);

            if (!(wrappedSink is IDisposable))
            {
                SelfLog.WriteLine("Wrapping sink {0} does not implement IDisposable; to ensure " +
                                  "wrapped sinks are properly flushed, wrappers should dispose " +
                                  "their wrapped contents", wrappedSink);
            }

            return loggerSinkConfiguration.Sink(wrappedSink, restrictedToMinimumLevel, levelSwitch);
        }
    }
}
