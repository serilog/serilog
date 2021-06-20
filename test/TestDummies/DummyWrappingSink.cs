using System;
using Serilog.Core;
using Serilog.Events;
using System.Collections.Generic;

namespace TestDummies
{
    public class DummyWrappingSink : ILogEventSink
    {
        [ThreadStatic]
        static List<LogEvent>? _emitted;

        public static List<LogEvent> Emitted => _emitted ?? (_emitted = new());

        readonly ILogEventSink _sink;

        public DummyWrappingSink(ILogEventSink sink)
        {
            _sink = sink;
        }

        public void Emit(LogEvent logEvent)
        {
            Emitted.Add(logEvent);
            _sink.Emit(logEvent);
        }

        public static void Reset()
        {
            _emitted = null;
        }
    }
}
