using System;
using Serilog.Core;
using Serilog.Events;
using System.Collections.Generic;

namespace TestDummies
{
    public class DummyWrappingSink : ILogEventSink
    {
        [ThreadStatic]
        // ReSharper disable ThreadStaticFieldHasInitializer
        public static List<LogEvent> Emitted = new List<LogEvent>();
        // ReSharper restore ThreadStaticFieldHasInitializer

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
