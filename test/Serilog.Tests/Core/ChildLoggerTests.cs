using Serilog.Core;
using Serilog.Core.Sinks;
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



        [Theory]
        // Visualizing the pipeline from left to right ....
        //
        //   Event  --> Root Logger --> Child Logger -> YES or
        //    lvl       override/lvl    override/levl     NO ?
        //
        // numbers are relative to incoming event level
        // Information + 1 = Warning
        // Information - 1 = Debug
        //
        // Incoming event is Information
        // with SourceContext Root.N1.N2
        //
        // - default case - no overrides
        [InlineData(null, 0, null, 0, true)]
        // - root overrides with level lower or equal to event
        // ... and child logger is out of the way
        [InlineData("Root", +0, null, +0, true)]
        [InlineData("Root", -1, null, +0, true)]
        [InlineData("Root.N1", +0, null, +0, true)]
        [InlineData("Root.N1", -1, null, +0, true)]
        [InlineData("Root.N1.N2", +0, null, +0, true)]
        [InlineData("Root.N1.N2", -1, null, +0, true)]
        // - root overrides on irrelevant namespaces
        [InlineData("xx", +1, null, +0, true)]
        [InlineData("Root.xx", +1, null, +0, true)]
        [InlineData("Root.N1.xx", +1, null, +0, true)]
        // - root overrides prevent all processing from children
        // even though children would happily accept it
        [InlineData("Root", +1, null, +0, false)]
        [InlineData("Root", +1, "Root", +0, false)]
        [InlineData("Root.N1", +1, null, +0, false)]
        [InlineData("Root.N1", +1, "Root.N1", +0, false)]
        [InlineData("Root.N1.N2", +1, null, +0, false)]
        [InlineData("Root.N1.N2", +1, "Root.N1.N2", +0, false)]
        public void WriteToLoggerWithConfigCallbackMinimumLevelOverrideInheritanceScenarios(
            string rootOverrideSource,
            int rootOverrideLevelIncrement,
            string childOverrideSource,
            int childOverrideLevelIncrement,
            bool eventShouldGetToChild)
        {
            var incomingEventLevel = Information;
            var rootOverrideLevel = incomingEventLevel + rootOverrideLevelIncrement;
            var childOverrideLevel = incomingEventLevel + childOverrideLevelIncrement;

            LogEvent evt = null;
            var sink = new DelegatingSink(e => evt = e);

            var rootLoggerConfig = new LoggerConfiguration()
                .MinimumLevel.Is(LevelAlias.Minimum);

            if (rootOverrideSource != null)
            {
                rootLoggerConfig.MinimumLevel.Override(rootOverrideSource, rootOverrideLevel);
            }

            var logger = rootLoggerConfig
                .WriteTo.Logger(lc =>
                {
                    lc.MinimumLevel.Is(LevelAlias.Minimum);
                    if (childOverrideSource != null)
                    {
                        lc.MinimumLevel.Override(childOverrideSource, childOverrideLevel);
                    }
                    lc.WriteTo.Sink(sink);
                })
                .CreateLogger();

            logger
                .ForContext(Constants.SourceContextPropertyName, "Root.N1.N2")
                .Write(Some.LogEvent(level: incomingEventLevel));

            Assert.Equal(eventShouldGetToChild, evt != null);
        }
    }
}
