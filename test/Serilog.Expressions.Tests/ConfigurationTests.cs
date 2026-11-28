using Serilog.Expressions.Tests.Support;
using Xunit;

namespace Serilog.Expressions.Tests;

public class ConfigurationTests
{
    [Fact]
    public void ExpressionsControlConditionalSinks()
    {
        var sink = new CollectingSink();
        var logger = new LoggerConfiguration()
            .WriteTo.Conditional("A = 1 or A = 2", wt => wt.Sink(sink))
            .CreateLogger();

        foreach (var a in Enumerable.Range(0, 5))
            logger.Information("{A}", a);

        Assert.Equal(2, sink.Events.Count);
    }

    [Fact]
    public void ExpressionsControlConditionalEnrichment()
    {
        var sink = new CollectingSink();
        var logger = new LoggerConfiguration()
            .Enrich.When("A = 1 or A = 2", e => e.WithProperty("B", 1))
            .WriteTo.Sink(sink)
            .CreateLogger();

        foreach (var a in Enumerable.Range(0, 5))
            logger.Information("{A}", a);

        Assert.Equal(2, sink.Events.Count(e => e.Properties.ContainsKey("B")));
    }
}