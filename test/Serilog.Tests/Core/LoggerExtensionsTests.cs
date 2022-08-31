namespace Serilog.Tests.Core;

public class LoggerExtensionsTests
{
    [Theory]
    [InlineData(LogEventLevel.Debug, LogEventLevel.Debug)]
    [InlineData(LogEventLevel.Debug, LogEventLevel.Information)]
    [InlineData(LogEventLevel.Debug, LogEventLevel.Error)]
    [InlineData(LogEventLevel.Debug, LogEventLevel.Fatal)]
    [InlineData(LogEventLevel.Debug, LogEventLevel.Warning)]
    public void ShouldEnrichLogEventWhenLevelIsSameOrHigherThanMinLevel(LogEventLevel logMinLevel, LogEventLevel propertyLogLevel)
    {
        var propValue = Guid.NewGuid();
        var propKey = Some.String();
        var sink = new CollectingSink();
        var logger = new LoggerConfiguration()
            .MinimumLevel.Is(logMinLevel)
            .WriteTo.Sink(sink)
            .CreateLogger();

        logger.ForContext(propertyLogLevel, propKey, propValue)
            .Write(logMinLevel, string.Empty);

        Assert.True(sink.SingleEvent.Properties.ContainsKey(propKey));
        Assert.Equal(sink.SingleEvent.Properties[propKey].LiteralValue(), propValue);
    }

    [Theory]
    [InlineData(LogEventLevel.Debug, LogEventLevel.Verbose)]
    [InlineData(LogEventLevel.Information, LogEventLevel.Debug)]
    [InlineData(LogEventLevel.Warning, LogEventLevel.Information)]
    [InlineData(LogEventLevel.Error, LogEventLevel.Warning)]
    [InlineData(LogEventLevel.Fatal, LogEventLevel.Error)]
    public void ShouldNotEnrichLogEventsWhenMinLevelIsHigherThanProvidedLogLevel(LogEventLevel logMinLevel, LogEventLevel propertyLogLevel)
    {
        var propValue = Guid.NewGuid();
        var propKey = Some.String();
        var sink = new CollectingSink();
        var logger = new LoggerConfiguration()
            .MinimumLevel.Is(logMinLevel)
            .WriteTo.Sink(sink)
            .CreateLogger();

        logger.ForContext(propertyLogLevel, propKey, propValue)
            .Write(logMinLevel, string.Empty);

        Assert.False(sink.SingleEvent.Properties.ContainsKey(propKey));
    }
}
