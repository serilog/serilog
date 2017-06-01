using System;
using System.Linq;
using Serilog.Events;
using Serilog.Parsing;

namespace Serilog.PerformanceTests.Support
{
    static class Some
    {
        public static LogEvent InformationEvent()
        {
            return new LogEvent(DateTime.Now, LogEventLevel.Information,
                null, new MessageTemplate(Enumerable.Empty<MessageTemplateToken>()), Enumerable.Empty<LogEventProperty>());
        }
        
    }
}
