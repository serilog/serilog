namespace Serilog.Tests.Support;

sealed class DisposeTrackingSink : ILogEventSink, IDisposable
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
