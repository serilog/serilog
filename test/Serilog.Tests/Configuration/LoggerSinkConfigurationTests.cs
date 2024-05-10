namespace Serilog.Tests.Configuration;

public class LoggerSinkConfigurationTests
{
    [Fact]
    public void CreateSinkSucceedsWhenNoSinksAreConfigured()
    {
        var sink = LoggerSinkConfiguration.CreateSink(_ => { });
        Assert.NotNull(sink);

        // Does nothing.
        sink.Emit(Some.LogEvent());
    }

    [Fact]
    public void CreateSinkSucceedsWhenOneSinkIsConfigured()
    {
        var single = new CollectingSink();
        var sink = LoggerSinkConfiguration.CreateSink(wt => wt.Sink(single));
        Assert.Same(single, sink);
    }

    [Fact]
    public void CreateSinkSucceedsWhenManySinksConfigured()
    {
        var first = new CollectingSink();
        var second = new CollectingSink();
        var sink = LoggerSinkConfiguration.CreateSink(wt => wt.Sink(first).WriteTo.Sink(second));
        var evt = Some.LogEvent();
        sink.Emit(evt);
        Assert.Same(evt, first.SingleEvent);
        Assert.Same(evt, second.SingleEvent);
    }

    [Fact]
    public void WrapDelegatesToTheEnclosedSink()
    {
        var enclosed = new CollectingSink();
        var wrapper = LoggerSinkConfiguration.Wrap(s => new DelegatingSink(s.Emit), wt => wt.Sink(enclosed));
        Assert.IsType<DelegatingSink>(wrapper);
        var evt = Some.LogEvent();
        wrapper.Emit(evt);
        Assert.Same(evt, enclosed.SingleEvent);
    }

    [Fact]
    public void WrapPropagatesDisposalToTheEnclosedSink()
    {
        var enclosed = new DisposeTrackingSink();
        var wrapper = LoggerSinkConfiguration.Wrap(s => new DelegatingSink(s.Emit), wt => wt.Sink(enclosed));

        // DelegatingSink is not IDisposable, but DisposeTrackingSink is, so a proxy is returned.
        Assert.IsNotType<DelegatingSink>(wrapper);

        var disposable = Assert.IsAssignableFrom<IDisposable>(wrapper);
        disposable.Dispose();
        Assert.True(enclosed.IsDisposed);
    }

    [Fact]
    public void WrappingDoesNotPermitEnrichment()
    {
        var enclosed = new CollectingSink();
        var propertyName = Some.String();
        var wrapper = LoggerSinkConfiguration.Wrap(
            s => new DelegatingSink(s.Emit),
            w => w
                .Sink(enclosed)
                .Enrich.WithProperty(propertyName, 1));

        var evt = Some.InformationEvent();
        wrapper.Emit(evt);

        Assert.Same(evt, enclosed.SingleEvent);
        Assert.False(evt.Properties.ContainsKey(propertyName));
    }
}
