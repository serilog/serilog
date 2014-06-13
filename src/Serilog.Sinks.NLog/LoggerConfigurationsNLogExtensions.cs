using System;
using Serilog.Sinks.NLog;
using Serilog.Configuration;
using Serilog.Events;

namespace Serilog
{
    public static class LoggerConfigurationsNLogExtensions
    {
        public static LoggerConfiguration NLog(
            this LoggerSinkConfiguration loggerConfiguration,
            LogEventLevel logLevel = LogEventLevel.Debug,
            IFormatProvider formatProvider = null)
        {
            if (loggerConfiguration == null)
            {
                throw new ArgumentNullException("loggerConfiguration");
            }

            return loggerConfiguration.Sink(new NLogSink(formatProvider), logLevel);
        }
    }
}
