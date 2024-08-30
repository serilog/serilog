namespace Serilog.Tests.Core;

public class FailureListenerTests
{
    [Fact]
    public void ThrownExceptionsAreSynchronouslyReportedAsFailures()
    {
        var listener = new CollectingFailureListener();

        using var logger = new LoggerConfiguration()
            .WriteTo.Fallible(
                wt => wt.Sink(new ThrowingSink()),
                listener)
            .CreateLogger();

        var evt = Some.InformationEvent();
        logger.Write(evt);

        var failure = Assert.Single(listener.Failures);
        Assert.Same(evt, failure.Events!.Single());
        Assert.NotNull(failure.Exception);
        Assert.Equal(LoggingFailureKind.Permanent, failure.Kind);
    }
}
