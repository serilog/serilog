#if FEATURE_ASYNCDISPOSABLE

namespace Serilog.Tests.Support;

sealed class AsyncDisposeTrackingSink : ILogEventSink, IDisposable, IAsyncDisposable
{
    public bool IsDisposed { get; set; }
    public bool IsDisposedAsync { get; set; }

    public void Emit(LogEvent logEvent)
    {
    }

    public void Dispose()
    {
        IsDisposed = true;
    }

    public ValueTask DisposeAsync()
    {
        Dispose();
        IsDisposedAsync = true;
        return default;
    }
}

#endif
