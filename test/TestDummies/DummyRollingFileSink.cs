using System;
using System.Collections.Generic;
using Serilog.Core;
using Serilog.Events;

namespace TestDummies;

public class DummyRollingFileSink : ILogEventSink
{
    [ThreadStatic]
    static List<LogEvent>? _emitted;

    public static List<LogEvent> Emitted => _emitted ?? (_emitted = new());

    public void Emit(LogEvent logEvent)
    {
        Emitted.Add(logEvent);
    }

    public static void Reset()
    {
        _emitted = null;
    }
}
