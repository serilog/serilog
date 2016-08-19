using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
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
