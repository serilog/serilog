using System;
using Serilog.Events;
using Serilog.Sinks.Http;

namespace Serilog
{
    public static class LoggerConfigurationHttpExtensions
    {
        public static LoggerConfiguration WithHttpSink(this LoggerConfiguration loggerConfiguration, string baseUrl, LogEventLevel restrictedToMinimumLevel = LogEventLevel.Minimum)
        {
            if (loggerConfiguration == null) throw new ArgumentNullException("loggerConfiguration");
            return loggerConfiguration.WithSink(new HttpServerSink(baseUrl), restrictedToMinimumLevel);
        }
    }
}
