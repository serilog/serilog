namespace Serilog.Tests;

[Collection("Log.Logger")]
public class LogTests
{
    [Fact]
    public void TheUninitializedLoggerIsSilent()
    {
        // This test depends on being there first executed from
        // the collection.
        Assert.IsType<SilentLogger>(Log.Logger);
    }

    [Fact]
    public void CloseAndFlushDisposesTheLogger()
    {
        var disposableLogger = new DisposableLogger();
        Log.Logger = disposableLogger;
        Log.CloseAndFlush();
        Assert.True(disposableLogger.IsDisposed);
    }

    [Fact]
    public void CloseAndFlushResetsLoggerToSilentLogger()
    {
        Log.Logger = new DisposableLogger();
        Log.CloseAndFlush();
        Assert.IsType<SilentLogger>(Log.Logger);
    }

#if FEATURE_ASYNCDISPOSABLE

    [Fact]
    public async Task CloseAndFlushAsyncDisposesTheLogger()
    {
        var disposableLogger = new DisposableLogger();
        Log.Logger = disposableLogger;
        await Log.CloseAndFlushAsync();
        Assert.True(disposableLogger.IsDisposedAsync);
    }

    [Fact]
    public async Task CloseAndFlushAsyncDisposesTheLoggerEvenWhenItIsNotAsyncDisposable()
    {
        var disposableLogger = new SyncDisposableLogger();
        Assert.IsNotAssignableFrom<IAsyncDisposable>(disposableLogger);
        Log.Logger = disposableLogger;
        await Log.CloseAndFlushAsync();
        Assert.True(disposableLogger.IsDisposed);
    }

    [Fact]
    public async Task CloseAndFlushAsyncResetsLoggerToSilentLogger()
    {
        Log.Logger = new DisposableLogger();
        await Log.CloseAndFlushAsync();
        Assert.IsType<SilentLogger>(Log.Logger);
    }

#endif
}
