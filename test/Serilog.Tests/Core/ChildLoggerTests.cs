using Serilog.Events;
using Serilog.Tests.Support;
using Xunit;
using static Serilog.Events.LogEventLevel;

namespace Serilog.Tests.Core
{
    public class ChildLoggerTests
    {

        [Theory]
        // Visualizing the pipeline from left to right ....
        //
        //   Event  --> Root Logger --> restrictedTo --> Child Logger -> YES or
        //    lvl        min lvl           param            min lvl       NO ?
        //
        // numbers are relative to incoming event level
        // Information + 1 = Warning
        // Information - 1 = Debug
        // Information + null = default
        //
        // - default case - nothing specified
        // equivalent to "Information+ allowed"
        [InlineData(Verbose,     null, null, null, false)]
        [InlineData(Debug,       null, null, null, false)]
        [InlineData(Information, null, null, null, true)]
        [InlineData(Warning,     null, null, null, true)]
        [InlineData(Error,       null, null, null, true)]
        [InlineData(Fatal,       null, null, null, true)]

        // - cases where event level is high enough all along the pipeline
        //              e       -->  --> -->  = OK
        [InlineData(Verbose,     +0, +0, +0, true)]
        [InlineData(Debug,       +0, +0, +0, true)]
        [InlineData(Information, +0, +0, +0, true)]
        [InlineData(Warning,     +0, +0, +0, true)]
        [InlineData(Error,       +0, +0, +0, true)]
        [InlineData(Fatal,       +0, +0, +0, true)]

        // - cases where event is blocked by root minimum level
        //              e        -x>  -   -   = NO
        [InlineData(Verbose,     +1, +0, +0, false)]
        [InlineData(Debug,       +1, +0, +0, false)]
        [InlineData(Information, +1, +0, +0, false)]
        [InlineData(Warning,     +1, +0, +0, false)]
        [InlineData(Error,       +1, +0, +0, false)]

        // - cases where event is blocked by param restrictedToMinimumLevel
        //              e        --> -x>  -   = NO
        [InlineData(Verbose,     +0, +1, +0, false)]
        [InlineData(Debug,       +0, +1, +0, false)]
        [InlineData(Information, +0, +1, +0, false)]
        [InlineData(Warning,     +0, +1, +0, false)]
        [InlineData(Error,       +0, +1, +0, false)]

        // - cases where event is blocked by child minimum level
        //              e        --> --> -x>  = NO
        [InlineData(Verbose,     +0, +0, +1, false)]
        [InlineData(Debug,       +0, +0, +1, false)]
        [InlineData(Information, +0, +0, +1, false)]
        [InlineData(Warning,     +0, +0, +1, false)]
        [InlineData(Error,       +0, +0, +1, false)]
        public void WriteToLoggerWithConfigCallbackMinimumLevelInheritanceScenarios(
            LogEventLevel eventLevel,
            int? rootMinimumLevelIncrement,
            int? sinkRestrictedToIncrement,
            int? childMinimumLevelIncrement,
            bool eventShouldGetToChild)
        {
            var rootMinimumLevel = eventLevel + rootMinimumLevelIncrement ?? Information;
            var sinkRestrictedToMinimumLevel = eventLevel + sinkRestrictedToIncrement ?? Verbose;
            var childMinimumLevel = eventLevel + childMinimumLevelIncrement ?? Information;

            LogEvent evt = null;
            var sink = new DelegatingSink(e => evt = e);

            var logger = new LoggerConfiguration()
                .MinimumLevel.Is(rootMinimumLevel)
                .WriteTo.Logger(lc => lc
                                .MinimumLevel.Is(childMinimumLevel)
                                .WriteTo.Sink(sink)
                                , restrictedToMinimumLevel: sinkRestrictedToMinimumLevel)
                .CreateLogger();

            logger.Write(Some.LogEvent(level: eventLevel));

            Assert.Equal(eventShouldGetToChild, evt!=null);
        }

        [Theory]
         // Visualizing the pipeline from left to right ....
        //
        //   Event  --> Root Logger --> restrictedTo --> Child Logger -> YES or
        //    lvl        min lvl           param            min lvl       NO ?
        //
        // numbers are relative to incoming event level
        // Information + 1 = Warning
        // Information - 1 = Debug
        // Information + null = default
        //
        // - default case - nothing specified
        // equivalent to "Information+ allowed"
        [InlineData(Verbose,     null, null, null, false)]
        [InlineData(Debug,       null, null, null, false)]
        [InlineData(Information, null, null, null, true)]
        [InlineData(Warning,     null, null, null, true)]
        [InlineData(Error,       null, null, null, true)]
        [InlineData(Fatal,       null, null, null, true)]

        // - cases where event level is high enough all along the pipeline
        //              e       -->  --> -->  = OK
        [InlineData(Verbose,     +0, +0, +0, true)]
        [InlineData(Debug,       +0, +0, +0, true)]
        [InlineData(Information, +0, +0, +0, true)]
        [InlineData(Warning,     +0, +0, +0, true)]
        [InlineData(Error,       +0, +0, +0, true)]
        [InlineData(Fatal,       +0, +0, +0, true)]

        // - cases where event is blocked by root minimum level
        //              e        -x>  -   -   = NO
        [InlineData(Verbose,     +1, +0, +0, false)]
        [InlineData(Debug,       +1, +0, +0, false)]
        [InlineData(Information, +1, +0, +0, false)]
        [InlineData(Warning,     +1, +0, +0, false)]
        [InlineData(Error,       +1, +0, +0, false)]

        // - cases where event is blocked by param restrictedToMinimumLevel
        //              e        --> -x>  -   = NO
        [InlineData(Verbose,     +0, +1, +0, false)]
        [InlineData(Debug,       +0, +1, +0, false)]
        [InlineData(Information, +0, +1, +0, false)]
        [InlineData(Warning,     +0, +1, +0, false)]
        [InlineData(Error,       +0, +1, +0, false)]

        // - cases where event is blocked by child minimum level
        //              e        --> --> -x>  = NO
        [InlineData(Verbose,     +0, +0, +1, false)]
        [InlineData(Debug,       +0, +0, +1, false)]
        [InlineData(Information, +0, +0, +1, false)]
        [InlineData(Warning,     +0, +0, +1, false)]
        [InlineData(Error,       +0, +0, +1, false)]
        public void WriteToLoggerMinimumLevelInheritanceScenarios(
            LogEventLevel eventLevel,
            int? rootMinimumLevelIncrement,
            int? sinkRestrictedToIncrement,
            int? childMinimumLevelIncrement,
            bool eventShouldGetToChild)
        {
            var rootMinimumLevel = eventLevel + rootMinimumLevelIncrement ?? Information;
            var sinkRestrictedToMinimumLevel = eventLevel + sinkRestrictedToIncrement ?? Verbose;
            var childMinimumLevel = eventLevel + childMinimumLevelIncrement ?? Information;

            LogEvent evt = null;
            var sink = new DelegatingSink(e => evt = e);

            var childLogger = new LoggerConfiguration()
                .MinimumLevel.Is(childMinimumLevel)
                .WriteTo.Sink(sink)
                .CreateLogger();

            var logger = new LoggerConfiguration()
                .MinimumLevel.Is(rootMinimumLevel)
                .WriteTo.Logger(childLogger, restrictedToMinimumLevel: sinkRestrictedToMinimumLevel)
                .CreateLogger();

            logger.Write(Some.LogEvent(level: eventLevel));

            Assert.Equal(eventShouldGetToChild, evt != null);
        }

    }
}
