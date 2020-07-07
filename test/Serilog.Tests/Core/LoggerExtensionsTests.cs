using System;
using Serilog;
using Serilog.Events;
using Serilog.Tests.Support;
using Xunit;

namespace Serilog.Tests.Core
{
    public class LoggerExtensionsTests
    {
        #region Methods

        #region Public Methods

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

            Assert.True(!sink.SingleEvent.Properties.ContainsKey(propKey));
        }

        [Theory]
        [InlineData(LogEventLevel.Verbose, true)]
        [InlineData(LogEventLevel.Debug, true)]
        [InlineData(LogEventLevel.Information, false)]
        [InlineData(LogEventLevel.Warning, false)]
        [InlineData(LogEventLevel.Error, false)]
        [InlineData(LogEventLevel.Fatal, false)]
        public void ShouldNotWriteLogEventsWhenMinLevelIsHigherThanDebug(LogEventLevel logMinLevel, bool shouldBeWritten)
        {
            var sink = new CollectingSink();
            var logger = new LoggerConfiguration()
                .MinimumLevel.Is(logMinLevel)
                .WriteTo.Sink(sink)
                .CreateLogger();

            logger.Debug("This is a {test}", () => "xxx");

            if (shouldBeWritten)
                Assert.True(sink.SingleEvent.Properties.ContainsKey("test"));
            else
                Assert.Empty(sink.Events);
        }

        [Theory]
        [InlineData(LogEventLevel.Verbose, true)]
        [InlineData(LogEventLevel.Debug, true)]
        [InlineData(LogEventLevel.Information, true)]
        [InlineData(LogEventLevel.Warning, true)]
        [InlineData(LogEventLevel.Error, true)]
        [InlineData(LogEventLevel.Fatal, false)]
        public void ShouldNotWriteLogEventsWhenMinLevelIsHigherThanError(LogEventLevel logMinLevel, bool shouldBeWritten)
        {
            var sink = new CollectingSink();
            var logger = new LoggerConfiguration()
                .MinimumLevel.Is(logMinLevel)
                .WriteTo.Sink(sink)
                .CreateLogger();

            logger.Error("This is a {test}", () => "xxx");

            if (shouldBeWritten)
                Assert.True(sink.SingleEvent.Properties.ContainsKey("test"));
            else
                Assert.Empty(sink.Events);
        }

        [Theory]
        [InlineData(LogEventLevel.Verbose, true)]
        [InlineData(LogEventLevel.Debug, true)]
        [InlineData(LogEventLevel.Information, true)]
        [InlineData(LogEventLevel.Warning, false)]
        [InlineData(LogEventLevel.Error, false)]
        [InlineData(LogEventLevel.Fatal, false)]
        public void ShouldNotWriteLogEventsWhenMinLevelIsHigherThanInformation(LogEventLevel logMinLevel, bool shouldBeWritten)
        {
            var sink = new CollectingSink();
            var logger = new LoggerConfiguration()
                .MinimumLevel.Is(logMinLevel)
                .WriteTo.Sink(sink)
                .CreateLogger();

            logger.Information("This is a {test}", () => "xxx");

            if (shouldBeWritten)
                Assert.True(sink.SingleEvent.Properties.ContainsKey("test"));
            else
                Assert.Empty(sink.Events);
        }

        [Theory]
        [InlineData(LogEventLevel.Verbose, LogEventLevel.Verbose, true)]
        [InlineData(LogEventLevel.Verbose, LogEventLevel.Debug, true)]
        [InlineData(LogEventLevel.Verbose, LogEventLevel.Information, true)]
        [InlineData(LogEventLevel.Verbose, LogEventLevel.Warning, true)]
        [InlineData(LogEventLevel.Verbose, LogEventLevel.Error, true)]
        [InlineData(LogEventLevel.Verbose, LogEventLevel.Fatal, true)]
        [InlineData(LogEventLevel.Debug, LogEventLevel.Verbose, false)]
        [InlineData(LogEventLevel.Debug, LogEventLevel.Debug, true)]
        [InlineData(LogEventLevel.Debug, LogEventLevel.Information, true)]
        [InlineData(LogEventLevel.Debug, LogEventLevel.Warning, true)]
        [InlineData(LogEventLevel.Debug, LogEventLevel.Error, true)]
        [InlineData(LogEventLevel.Debug, LogEventLevel.Fatal, true)]
        [InlineData(LogEventLevel.Information, LogEventLevel.Verbose, false)]
        [InlineData(LogEventLevel.Information, LogEventLevel.Debug, false)]
        [InlineData(LogEventLevel.Information, LogEventLevel.Information, true)]
        [InlineData(LogEventLevel.Information, LogEventLevel.Warning, true)]
        [InlineData(LogEventLevel.Information, LogEventLevel.Error, true)]
        [InlineData(LogEventLevel.Information, LogEventLevel.Fatal, true)]
        [InlineData(LogEventLevel.Warning, LogEventLevel.Verbose, false)]
        [InlineData(LogEventLevel.Warning, LogEventLevel.Debug, false)]
        [InlineData(LogEventLevel.Warning, LogEventLevel.Information, false)]
        [InlineData(LogEventLevel.Warning, LogEventLevel.Warning, true)]
        [InlineData(LogEventLevel.Warning, LogEventLevel.Error, true)]
        [InlineData(LogEventLevel.Warning, LogEventLevel.Fatal, true)]
        [InlineData(LogEventLevel.Error, LogEventLevel.Verbose, false)]
        [InlineData(LogEventLevel.Error, LogEventLevel.Debug, false)]
        [InlineData(LogEventLevel.Error, LogEventLevel.Information, false)]
        [InlineData(LogEventLevel.Error, LogEventLevel.Warning, false)]
        [InlineData(LogEventLevel.Error, LogEventLevel.Error, true)]
        [InlineData(LogEventLevel.Error, LogEventLevel.Fatal, true)]
        [InlineData(LogEventLevel.Fatal, LogEventLevel.Verbose, false)]
        [InlineData(LogEventLevel.Fatal, LogEventLevel.Debug, false)]
        [InlineData(LogEventLevel.Fatal, LogEventLevel.Information, false)]
        [InlineData(LogEventLevel.Fatal, LogEventLevel.Warning, false)]
        [InlineData(LogEventLevel.Fatal, LogEventLevel.Error, false)]
        [InlineData(LogEventLevel.Fatal, LogEventLevel.Fatal, true)]
        public void ShouldNotWriteLogEventsWhenMinLevelIsHigherThanProvidedLogLevel(LogEventLevel logMinLevel, LogEventLevel logLevelToWrite, bool shouldBeWritten)
        {
            var sink = new CollectingSink();
            var logger = new LoggerConfiguration()
                .MinimumLevel.Is(logMinLevel)
                .WriteTo.Sink(sink)
                .CreateLogger();

            logger.Write(logLevelToWrite, "This is a {test}", () => "xxx");

            if (shouldBeWritten)
                Assert.True(sink.SingleEvent.Properties.ContainsKey("test"));
            else
                Assert.Empty(sink.Events);
        }

        [Theory]
        [InlineData(LogEventLevel.Verbose, true)]
        [InlineData(LogEventLevel.Debug, false)]
        [InlineData(LogEventLevel.Information, false)]
        [InlineData(LogEventLevel.Warning, false)]
        [InlineData(LogEventLevel.Error, false)]
        [InlineData(LogEventLevel.Fatal, false)]
        public void ShouldNotWriteLogEventsWhenMinLevelIsHigherThanVerbose(LogEventLevel logMinLevel, bool shouldBeWritten)
        {
            var sink = new CollectingSink();
            var logger = new LoggerConfiguration()
                .MinimumLevel.Is(logMinLevel)
                .WriteTo.Sink(sink)
                .CreateLogger();

            logger.Verbose("This is a {test}", () => "xxx");

            if (shouldBeWritten)
                Assert.True(sink.SingleEvent.Properties.ContainsKey("test"));
            else
                Assert.Empty(sink.Events);
        }

        [Theory]
        [InlineData(LogEventLevel.Verbose, true)]
        [InlineData(LogEventLevel.Debug, true)]
        [InlineData(LogEventLevel.Information, true)]
        [InlineData(LogEventLevel.Warning, true)]
        [InlineData(LogEventLevel.Error, false)]
        [InlineData(LogEventLevel.Fatal, false)]
        public void ShouldNotWriteLogEventsWhenMinLevelIsHigherThanWarning(LogEventLevel logMinLevel, bool shouldBeWritten)
        {
            var sink = new CollectingSink();
            var logger = new LoggerConfiguration()
                .MinimumLevel.Is(logMinLevel)
                .WriteTo.Sink(sink)
                .CreateLogger();

            logger.Warning("This is a {test}", () => "xxx");

            if (shouldBeWritten)
                Assert.True(sink.SingleEvent.Properties.ContainsKey("test"));
            else
                Assert.Empty(sink.Events);
        }

        #endregion Public Methods

        #endregion Methods
    }
}
