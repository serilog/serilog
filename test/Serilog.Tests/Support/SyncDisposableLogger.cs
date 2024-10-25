#if FEATURE_ASYNCDISPOSABLE && FEATURE_DEFAULT_INTERFACE

namespace Serilog.Tests.Support;

public class SyncDisposableLogger: ILogger, IDisposable
{
    public bool IsDisposed { get; private set; }

    public void Write(LogEvent logEvent)
    {
    }

    public void Dispose()
    {
        IsDisposed = true;
    }
}

#endif
