using System;
using System.Collections.Generic;
using Serilog.Core;
using Serilog.Events;
using System.Threading.Tasks;

namespace TestDummies
{
    public class DummyRollingFileAuditSink : ILogEventSink
    {
        [ThreadStatic]
        public static List<LogEvent> Emitted = new List<LogEvent>();

        public Task Emit(LogEvent logEvent)
        {
            Emitted.Add(logEvent);
            return Task.FromResult((object)null);
        }
    }
}
