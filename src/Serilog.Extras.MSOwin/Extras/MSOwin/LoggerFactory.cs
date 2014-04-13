using System;
using System.Diagnostics;
using Microsoft.Owin.Logging;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.Extras.MSOwin
{
    public class LoggerFactory : ILoggerFactory
    {
        readonly Func<ILogger> _getLogger;
        readonly Func<TraceEventType, LogEventLevel> _getLogEventLevel;

        public LoggerFactory(ILogger logger = null, Func<TraceEventType, LogEventLevel> getLogEventLevel = null)
        {
            _getLogger = logger == null ? (Func<ILogger>) (() => Log.Logger) : (() => logger);
            _getLogEventLevel = getLogEventLevel ?? ToLogEventLevel;
        }

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