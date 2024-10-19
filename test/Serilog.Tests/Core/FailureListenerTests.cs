namespace Serilog.Tests.Core;

public class FailureListenerTests
{
    [Fact]
    public void ThrownExceptionsAreSynchronouslyReportedAsFailures()
    {
        var listener = new CollectingFailureListener();

        var disposeTracker = new DisposeTrackingSink();
        var sink = new OptionalInterfaceForwardingSink(new ThrowingSink(), disposeTracker);

        var evt = Some.InformationEvent();
        using (var logger = new LoggerConfiguration()
                   .WriteTo.Fallible(
                       wt => wt.Sink(sink),
                       listener)
                   .CreateLogger())
        {
            logger.Write(evt);
        }

        var failure = Assert.Single(listener.Failures);
        Assert.Same(evt, failure.Events!.Single());
        Assert.NotNull(failure.Exception);
        Assert.Equal(LoggingFailureKind.Permanent, failure.Kind);
        Assert.True(disposeTracker.IsDisposed);
    }

    [Fact]
    public void ShallowFallbackChainsAreEffective()
    {
        var disposeTracker1 = new DisposeTrackingSink();
        var collector = new CollectingSink();
        var fallback = new OptionalInterfaceForwardingSink(collector, disposeTracker1);

        var disposeTracker2 = new DisposeTrackingSink();
        var initial = new OptionalInterfaceForwardingSink(new ThrowingSink(), disposeTracker2);

        var evt = Some.InformationEvent();
        using (var logger = new LoggerConfiguration()
                   .WriteTo.FallbackChain(
                       wt => wt.Sink(initial),
                       wt => wt.Sink(fallback))
                   .CreateLogger())
        {
            logger.Write(evt);
        }

        var logged = Assert.Single(collector.Events);
        Assert.Same(evt, logged);
        Assert.True(disposeTracker1.IsDisposed);
        Assert.True(disposeTracker2.IsDisposed);
    }
}
