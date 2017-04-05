using System;
using Serilog.Core;
using Serilog.Events;
using System.Collections.Generic;

namespace TestDummies
{
    public class DummyWrappingSink : ILogEventSink
    {
        [ThreadStatic]
        public static List<LogEvent> Emitted = new List<LogEvent>();

        private readonly ILogEventSink _sink;

        public DummyWrappingSink(ILogEventSink sink)
        {
            _sink = sink;
        }

        public void Emit(LogEvent logEvent)
        {
            Emitted.Add(logEvent);
            _sink.Emit(logEvent);
        }
    }
}