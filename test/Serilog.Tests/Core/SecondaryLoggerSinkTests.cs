using Serilog.Core;
using Serilog.Events;
using Serilog.Tests.Support;
using Xunit;

namespace Serilog.Tests.Core
{
    public class SecondaryLoggerSinkTests
    {
        [Fact]
        public void ModifyingCopiesPassedThroughTheSinkPreservesOriginal()
        {
            CollectingSink secondary = new();
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
            DisposeTrackingSink secondary = new();
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
            DisposeTrackingSink secondary = new();

            new LoggerConfiguration()
                .WriteTo.Logger(lc => lc.WriteTo.Sink(secondary))
                .CreateLogger()
                .Dispose();

            Assert.True(secondary.IsDisposed);
        }

        [Fact]
        public void ChildLoggerInheritsParentLevelByDefault()
        {
            CollectingSink sink = new();

            var logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Logger(lc => lc
                    .WriteTo.Sink(sink))
                .CreateLogger();

            logger.Write(Some.DebugEvent());

            Assert.Single(sink.Events);
        }

        [Fact]
        public void ChildLoggerCanOverrideInheritedLevel()
        {
            CollectingSink sink = new();

            var logger = new LoggerConfiguration()
                .MinimumLevel.ControlledBy(new LoggingLevelSwitch(LogEventLevel.Debug))
                .WriteTo.Logger(lc => lc
                    .MinimumLevel.Error()
                    .WriteTo.Sink(sink))
                .CreateLogger();

            logger.Write(Some.DebugEvent());

            Assert.Empty(sink.Events);
        }
    }
}
