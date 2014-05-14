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
using System.Diagnostics;
using Microsoft.Owin.Logging;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.Extras.MSOwin
{
    /// <summary>
    /// Implementation of Microsoft.Owin.Logger.ILoggerFactory.
    /// </summary>
    public class LoggerFactory : ILoggerFactory
    {
        readonly Func<ILogger> _getLogger;
        readonly Func<TraceEventType, LogEventLevel> _getLogEventLevel;

        /// <summary>
        /// Create a logger factory.
        /// </summary>
        /// <param name="logger">The logger; if not provided the global <see cref="Log.Logger"/> will be used.</param>
        /// <param name="getLogEventLevel"></param>
        public LoggerFactory(ILogger logger = null, Func<TraceEventType, LogEventLevel> getLogEventLevel = null)
        {
            _getLogger = logger == null ? (Func<ILogger>) (() => Log.Logger) : (() => logger);
            _getLogEventLevel = getLogEventLevel ?? ToLogEventLevel;
        }

        /// <summary>
        /// Creates a new ILogger instance of the given name.
        /// </summary>
        /// <param name="name">The logger context name.</param>
        /// <returns>A logger instance.</returns>
        public Microsoft.Owin.Logging.ILogger Create(string name)
        {
            return new Logger(_getLogger().ForContext(Constants.SourceContextPropertyName, name), _getLogEventLevel);
        }

        static LogEventLevel ToLogEventLevel(TraceEventType traceEventType)
        {
            switch (traceEventType)
            {
                case TraceEventType.Critical:
                    return LogEventLevel.Fatal;
                case TraceEventType.Error:
                    return LogEventLevel.Error;
                case TraceEventType.Warning:
                    return LogEventLevel.Warning;
                case TraceEventType.Information:
                    return LogEventLevel.Information;
                case TraceEventType.Verbose:
                    return LogEventLevel.Verbose;
                case TraceEventType.Start:
                    return LogEventLevel.Debug;
                case TraceEventType.Stop:
                    return LogEventLevel.Debug;
                case TraceEventType.Suspend:
                    return LogEventLevel.Debug;
                case TraceEventType.Resume:
                    return LogEventLevel.Debug;
                case TraceEventType.Transfer:
                    return LogEventLevel.Debug;
                default:
                    throw new ArgumentOutOfRangeException("traceEventType");
            }
        }

        class Logger : Microsoft.Owin.Logging.ILogger
        {
            readonly ILogger _logger;
            readonly Func<TraceEventType, LogEventLevel> _getLogEventLevel;

            internal Logger(ILogger logger, Func<TraceEventType, LogEventLevel> getLogEventLevel)
            {
                _logger = logger;
                _getLogEventLevel = getLogEventLevel;
            }

            public bool WriteCore(TraceEventType eventType, int eventId, object state, Exception exception, Func<object, Exception, string> formatter)
            {
                LogEventLevel level = _getLogEventLevel(eventType);

                // According to docs http://katanaproject.codeplex.com/SourceControl/latest#src/Microsoft.Owin/Logging/ILogger.cs
                // "To check IsEnabled call WriteCore with only TraceEventType and check the return value, no event will be written."
                if (state == null)
                {
                    return _logger.IsEnabled(level);
                }
                if (!_logger.IsEnabled(level))
                {
                    return false;
                }
                _logger.Write(level, exception, formatter(state, exception));
                return true;
            }
        }
    }
}