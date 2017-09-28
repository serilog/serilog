using Serilog.Tests.Support;
using Xunit;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.Tests.Core
{
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
        public void ChildLoggerInheritsParentLevelByDefault()
        {
            var sink = new CollectingSink();

            var logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Logger(lc => lc
                    .WriteTo.Sink(sink))
                .CreateLogger();

            logger.Write(Some.DebugEvent());

            Assert.Equal(1, sink.Events.Count);
        }

        [Fact]
        public void ChildLoggerCanOverrideInheritedLevel()
        {
            var sink = new CollectingSink();

            var logger = new LoggerConfiguration()
                .MinimumLevel.ControlledBy(new LoggingLevelSwitch(LogEventLevel.Debug))
                .WriteTo.Logger(lc => lc
                    .MinimumLevel.Error()
                    .WriteTo.Sink(sink))
                .CreateLogger();

            logger.Write(Some.DebugEvent());

            Assert.Equal(0, sink.Events.Count);
        }

        [Fact]
        public void Issue967_ChildLoggersCanHaveMinimumLevelOverrides()
        {
            var sink = new CollectingSink();
            var logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Logger(lc => lc
                    .MinimumLevel.Information()
                    .MinimumLevel.Override("Serilog.Tests", LogEventLevel.Error)
                    .WriteTo.Sink(sink))
                .CreateLogger();

            logger.Write(Some.InformationEvent());
            logger.Write(Some.LogEvent(level: LogEventLevel.Error));

            var contextLogger = logger.ForContext<LoggerConfigurationTests>();
            contextLogger.Write(Some.InformationEvent());
            contextLogger.Write(Some.LogEvent(level: LogEventLevel.Error));

            Assert.Equal(3, sink.Events.Count);
        }

        [Fact]
        public void ChildLoggerCanHaveMinimumLevelOverrides()
        {
            var sink = new CollectingSink();

            var logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Logger(lc => lc
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                    .WriteTo.Sink(sink))
                .CreateLogger();

            logger.ForContext(Constants.SourceContextPropertyName, "Microsoft.Foo").Write(Some.DebugEvent());
            Assert.Equal(0, sink.Events.Count);

            logger.ForContext(Constants.SourceContextPropertyName, "Microsoft.Foo").Write(Some.InformationEvent());
            Assert.Equal(1, sink.Events.Count);
        }
    }
}
