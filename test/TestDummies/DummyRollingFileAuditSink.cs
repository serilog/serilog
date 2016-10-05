using System;
using System.Collections.Generic;
using Serilog.Core;
using Serilog.Events;

namespace TestDummies
{
    public class DummyRollingFileAuditSink : ILogEventSink
    {
        [ThreadStatic]
        public static List<LogEvent> Emitted = new List<LogEvent>();

        public void Emit(LogEvent logEvent)
        {
            Emitted.Add(logEvent);
        }
    }
}
