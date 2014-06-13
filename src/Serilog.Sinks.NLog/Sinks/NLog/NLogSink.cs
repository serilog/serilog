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
            string loggerName = "Default";

            LogEventPropertyValue sourceContext;
            if (logEvent.Properties.TryGetValue(Constants.SourceContextPropertyName, out sourceContext))
            {
                var sv = sourceContext as ScalarValue;
                if (sv != null && sv.Value is string)
                {
                    loggerName = (string)sv.Value;
                }
            }

            var message = logEvent.RenderMessage(formatProvider);
            var exception = logEvent.Exception;

            var logger = LogManager.GetLogger(loggerName);
            switch (logEvent.Level)
            {
                case LogEventLevel.Verbose:
                case LogEventLevel.Debug:
                    logger.Debug(message, exception);
                    break;
                case LogEventLevel.Information:
                    logger.Info(message, exception);
                    break;
                case LogEventLevel.Warning:
                    logger.Warn(message, exception);
                    break;
                case LogEventLevel.Error:
                    logger.Error(message, exception);
                    break;
                case LogEventLevel.Fatal:
                    logger.Fatal(message, exception);
                    break;
                default:
                    SelfLog.WriteLine("Unexpected logging level, writing to log4net as Info");
                    logger.Info(message, exception);
                    break;
            }
        }
    }
}
