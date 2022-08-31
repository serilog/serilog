namespace Serilog.Tests.Filters;

public class MatchingTests
{
    [Fact]
    public void EventsCanBeExcludedBySource()
    {
        var written = false;

        var log = new LoggerConfiguration()
            .Filter.ByExcluding(Matching.FromSource<MatchingTests>())
            .WriteTo.Sink(new DelegatingSink(_ => written = true))
            .CreateLogger();

        var sourceContext = log.ForContext<MatchingTests>();
        sourceContext.Write(Some.InformationEvent());

        Assert.False(written);
    }

    [Fact]
    public void EventsCanBeExcludedByPredicate()
    {
        var seen = 0;

        var log = new LoggerConfiguration()
            .Filter.ByExcluding(Matching.WithProperty<int>("Count", p => p < 10))
            .WriteTo.Sink(new DelegatingSink(_ => seen++))
            .CreateLogger();

        log.Warning("Unrelated");
        log.Information("{Count}", 5);
        log.Information("{Count}", "wrong type");
        log.Information("{Count}", 15);

        Assert.Equal(3, seen);
    }

    [Fact]
    public void SourceFiltersWorkOnNamespaces()
    {
        var written = false;
        var log = new LoggerConfiguration()
            .Filter.ByExcluding(Matching.FromSource("Serilog.Tests"))
            .WriteTo.Sink(new DelegatingSink(_ => written = true))
            .CreateLogger()
            .ForContext<MatchingTests>();

        log.Write(Some.InformationEvent());
        Assert.False(written);
    }

    [Fact]
    public void SourceFiltersSkipNonNamespaces()
    {
        var written = false;
        var log = new LoggerConfiguration()
            .Filter.ByExcluding(Matching.FromSource("Serilog.Tests"))
            .WriteTo.Sink(new DelegatingSink(_ => written = true))
            .CreateLogger()
            .ForContext(Serilog.Core.Constants.SourceContextPropertyName, "Serilog.TestsLong");

        log.Write(Some.InformationEvent());
        Assert.True(written);
    }
}
