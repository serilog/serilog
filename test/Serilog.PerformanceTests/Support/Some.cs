using System;
using Serilog.Events;

namespace Serilog.PerformanceTests.Support
{
    static class Some
    {
        public static LogEvent InformationEvent(string messageTemplate = "Hello, world!", params object?[]? propertyValues)
        {
            var logger = new LoggerConfiguration().CreateLogger();
#pragma warning disable Serilog004 // Constant MessageTemplate verifier
            if (logger.BindMessageTemplate(messageTemplate, propertyValues, out var parsedTemplate, out var boundProperties))
            {
                return new LogEvent(DateTime.Now, LogEventLevel.Information, null, parsedTemplate, boundProperties);
            }
            throw new InvalidOperationException();
#pragma warning restore Serilog004 // Constant MessageTemplate verifier
        }
    }
}
