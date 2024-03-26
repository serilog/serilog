namespace Serilog.Tests.Core;

public class SecondaryLoggerSinkTests
{
    [Fact]
    public void ModifyingCopiesPassedThroughTheSinkPreservesOriginal()
    {
        var secondary = new CollectingSink();
        var secondaryLogger = new LoggerConfiguration()
            .WriteTo.Sink(secondary)
            .CreateLogger();

        var e = Some.InformationEvent();
        new LoggerConfiguration()
            .WriteTo.Logger(secondaryLogger)
            .CreateLogger()
            .Write(e);

        Assert.NotSame(e, secondary.SingleEvent);
        var p = Some.LogEventProperty();
        secondary.SingleEvent.AddPropertyIfAbsent(p);
        Assert.True(secondary.SingleEvent.Properties.ContainsKey(p.Name));
        Assert.False(e.Properties.ContainsKey(p.Name));
    }

    [Fact]
    public void WhenOwnedByCallerSecondaryLoggerIsNotDisposed()
    {
        var secondary = new DisposeTrackingSink();
        var secondaryLogger = new LoggerConfiguration()
            .WriteTo.Sink(secondary)
            .CreateLogger();

        new LoggerConfiguration()
            .WriteTo.Logger(secondaryLogger)
            .CreateLogger()
            .Dispose();

        Assert.False(secondary.IsDisposed);
    }

    [Fact]
    public void WhenOwnedByPrimaryLoggerSecondaryIsDisposed()
    {
        var secondary = new DisposeTrackingSink();

        new LoggerConfiguration()
            .WriteTo.Logger(lc => lc.WriteTo.Sink(secondary))
            .CreateLogger()
            .Dispose();

        Assert.True(secondary.IsDisposed);
    }

    [Fact]
    public void SecondaryLoggerInheritsPrimaryLevelByDefault()
    {
        var sink = new CollectingSink();

        var logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Logger(lc => lc
                .WriteTo.Sink(sink))
            .CreateLogger();

        logger.Write(Some.DebugEvent());

        Assert.Single(sink.Events);
    }

    [Fact]
    public void SecondaryLoggerCanOverrideInheritedLevel()
    {
        var sink = new CollectingSink();

        var logger = new LoggerConfiguration()
            .MinimumLevel.ControlledBy(new(Debug))
            .WriteTo.Logger(lc => lc
                .MinimumLevel.Error()
                .WriteTo.Sink(sink))
            .CreateLogger();

        logger.Write(Some.DebugEvent());

        Assert.Empty(sink.Events);
    }

#if FEATURE_ASYNCDISPOSABLE
    [Fact]
    public async Task OwnedSecondaryLoggerIsDisposedAsyncWhenPrimaryIsDisposedAsync()
    {
        var sink = new AsyncDisposeTrackingSink();

        var root = new LoggerConfiguration()
            .WriteTo.Logger(lc => lc.WriteTo.Sink(sink))
            .CreateLogger();

        await root.DisposeAsync();

        Assert.True(sink.IsDisposedAsync);
    }

    [Fact]
    public async Task IndependentChildLoggerIsNotDisposedWhenPrimaryIsDisposedAsync()
    {
        var sink = new AsyncDisposeTrackingSink();
        var child = new LoggerConfiguration()
            .WriteTo.Sink(sink)
            .CreateLogger();

        var root = new LoggerConfiguration()
            .WriteTo.Logger(child)
            .CreateLogger();

        await root.DisposeAsync();

        Assert.False(sink.IsDisposed);
        Assert.False(sink.IsDisposedAsync);
    }
#endif
}
