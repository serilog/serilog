using System;
using NLog;
using Serilog.Core;
using Serilog.Debugging;
using Serilog.Events;

namespace Serilog.Sinks.NLog
{
    class NLogSink : ILogEventSink
    {
        readonly IFormatProvider formatProvider;

        public NLogSink(IFormatProvider formatProvider = null)
        {
            this.formatProvider = formatProvider;
        }

        public void Emit(LogEvent logEvent)
        {
            var loggerName = "Default";

            LogEventPropertyValue sourceContext;
            if (logEvent.Properties.TryGetValue(Constants.SourceContextPropertyName, out sourceContext))
            {
                var sv = sourceContext as ScalarValue;
                if (sv != null && sv.Value is string)
                {
                    loggerName = (string)sv.Value;
                }
            }

            var level = GetMappedLevel(logEvent);
            var message = logEvent.RenderMessage(formatProvider);
            var exception = logEvent.Exception;

            var nlogEvent = new LogEventInfo(level, loggerName, message)
            {
                Exception = exception
            };

            // pass along the event's properties to nlog
            foreach (var property in logEvent.Properties)
            {
                // format simple scalar strings without wrapping quotes, which is more likely to be what nlog users expect
                var format = (property.Value is ScalarValue && property.Value.GetType() == typeof(string))
                    ? "l" // literal
                    : null;

                nlogEvent.Properties[property.Key] = property.Value.ToString(format, null);
            }

            var logger = LogManager.GetLogger(loggerName);
            logger.Log(nlogEvent);
        }

        private static LogLevel GetMappedLevel(LogEvent logEvent)
        {
            switch (logEvent.Level)
            {
                case LogEventLevel.Verbose:
                    return LogLevel.Trace;
                case LogEventLevel.Debug:
                    return LogLevel.Debug;
                case LogEventLevel.Information:
                    return LogLevel.Info;
                case LogEventLevel.Warning:
                    return LogLevel.Warn;
                case LogEventLevel.Error:
                    return LogLevel.Error;
                case LogEventLevel.Fatal:
                    return LogLevel.Fatal;
                default:
                    SelfLog.WriteLine("Unexpected logging level, writing to NLog as Info");
                    return LogLevel.Info;
            }
        }
    }
}
