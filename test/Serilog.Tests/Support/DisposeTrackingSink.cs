namespace Serilog.Tests.Support;

class DisposeTrackingSink : ILogEventSink, IDisposable
{
    public bool IsDisposed { get; set; }

    public void Emit(LogEvent logEvent)
    {
    }

    public void Dispose()
    {
        IsDisposed = true;
    }
}
