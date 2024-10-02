namespace Serilog.PerformanceTests.Support;

class NullSink : ILogEventSink
{
    public void Emit(LogEvent logEvent)
    {
    }
}

class DisposableNullSink : ILogEventSink, IDisposable
{
    public void Emit(LogEvent logEvent)
    {
    }
    public void Dispose()
    {
    }
}
