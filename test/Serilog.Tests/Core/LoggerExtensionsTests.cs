namespace Serilog.Tests.Core;

public class LoggerExtensionsTests
{
    [Theory]
    [InlineData(Debug, Debug)]
    [InlineData(Debug, Information)]
    [InlineData(Debug, Error)]
    [InlineData(Debug, Fatal)]
    [InlineData(Debug, Warning)]
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
    [InlineData(Debug, Verbose)]
    [InlineData(Information, Debug)]
    [InlineData(Warning, Information)]
    [InlineData(Error, Warning)]
    [InlineData(Fatal, Error)]
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
