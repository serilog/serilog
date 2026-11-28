using Serilog.Expressions.Tests.Support;
using Xunit;

namespace Serilog.Expressions.Tests;

public class LoggingFilterSwitchTests
{
    [Fact]
    public void WhenTheFilterExpressionIsModifiedTheFilterChanges()
    {
        var @switch = new LoggingFilterSwitch();
        var sink = new CollectingSink();

        var log = new LoggerConfiguration()
            .Filter.ControlledBy(@switch)
            .WriteTo.Sink(sink)
            .CreateLogger();

        var v11 = Some.InformationEvent("Adding {Volume} L", 11);

        log.Write(v11);
        Assert.Same(v11, sink.SingleEvent);
        sink.Events.Clear();

        @switch.Expression = "Volume > 12";

        log.Write(v11);
        Assert.Empty(sink.Events);

        @switch.Expression = "Volume > 10";

        log.Write(v11);
        Assert.Same(v11, sink.SingleEvent);
        sink.Events.Clear();

        @switch.Expression = null;

        log.Write(v11);
        Assert.Same(v11, sink.SingleEvent);
    }
}