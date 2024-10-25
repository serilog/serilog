namespace TestDummies;

public class DummyRollingFileAuditSink : ILogEventSink
{
    [ThreadStatic]
    static List<LogEvent>? _emitted;

    public static List<LogEvent> Emitted => _emitted ??= [];

    public void Emit(LogEvent logEvent)
    {
        Emitted.Add(logEvent);
    }

    public static void Reset()
    {
        _emitted = null;
    }
}
