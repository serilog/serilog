using System;
using Serilog.Web;

namespace Serilog
{
    public static class LoggerConfigurationExtensions
    {
        public static LoggerConfiguration EnrichedWithHttpRequestProperties(this LoggerConfiguration loggerConfiguration)
        {
            if (loggerConfiguration == null) throw new ArgumentNullException("loggerConfiguration");
            return loggerConfiguration.EnrichedBy(new HttpRequestLogEventEnricher());
        }
    }
}
