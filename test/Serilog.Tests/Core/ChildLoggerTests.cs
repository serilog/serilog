using System.Collections.Generic;
using Serilog.Core;
using Serilog.Events;
using Serilog.Tests.Support;
using Xunit;
using static Serilog.Events.LogEventLevel;

namespace Serilog.Tests.Core
{
    public class ChildLoggerTests
    {
        public static IEnumerable<object[]> GetMinimumLevelInheritanceTestCases()
        {
            // Visualizing the pipeline from left to right ....
            //
            //   Event  --> Root Logger --> restrictedTo --> Child Logger -> YES or
            //    lvl        min lvl           param            min lvl       NO ?
            //
            object[] T(LogEventLevel el, int? rl, int? rt, int? cl, bool r)
            {
                return new object[]{ el, rl, rt, cl, r };
            }
            // numbers are relative to incoming event level
            // Information + 1 = Warning
            // Information - 1 = Debug
            // Information + null = default
            //
            // - default case - nothing specified
            // equivalent to "Information+ allowed"
            yield return T(Verbose,     null, null, null, false);
            yield return T(Debug,       null, null, null, false);
            yield return T(Information, null, null, null, true);
            yield return T(Warning,     null, null, null, true);
            yield return T(Error,       null, null, null, true);
            yield return T(Fatal,       null, null, null, true);

            // - cases where event level is high enough all along the pipeline
            //                  e       -->  --> -->  = OK
            yield return T(Verbose,     +0, +0, +0, true);
            yield return T(Debug,       +0, +0, +0, true);
            yield return T(Information, +0, +0, +0, true);
            yield return T(Warning,     +0, +0, +0, true);
            yield return T(Error,       +0, +0, +0, true);
            yield return T(Fatal,       +0, +0, +0, true);

            // - cases where event is blocked by root minimum level
            //                 e        -x>  -   -   = NO
            yield return T(Verbose,     +1, +0, +0, false);
            yield return T(Debug,       +1, +0, +0, false);
            yield return T(Information, +1, +0, +0, false);
            yield return T(Warning,     +1, +0, +0, false);
            yield return T(Error,       +1, +0, +0, false);

            // - cases where event is blocked by param restrictedToMinimumLevel
            //                 e        --> -x>  -   = NO
            yield return T(Verbose,     +0, +1, +0, false);
            yield return T(Debug,       +0, +1, +0, false);
            yield return T(Information, +0, +1, +0, false);
            yield return T(Warning,     +0, +1, +0, false);
            yield return T(Error,       +0, +1, +0, false);

            // - cases where event is blocked by child minimum level
            //                 e        --> --> -x>  = NO
            yield return T(Verbose,     +0, +0, +1, false);
            yield return T(Debug,       +0, +0, +1, false);
            yield return T(Information, +0, +0, +1, false);
            yield return T(Warning,     +0, +0, +1, false);
            yield return T(Error,       +0, +0, +1, false);
        }

        [Theory]
        [MemberData(nameof(GetMinimumLevelInheritanceTestCases))]
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
                                .WriteTo.Sink(sink),
                                restrictedToMinimumLevel: sinkRestrictedToMinimumLevel)
                .CreateLogger();

            logger.Write(Some.LogEvent(level: eventLevel));

            if (eventShouldGetToChild)
            {
                Assert.NotNull(evt);
            }
            else
            {
                Assert.Null(evt);
            }
        }

        [Theory]
        [MemberData(nameof(GetMinimumLevelInheritanceTestCases))]
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

            if (eventShouldGetToChild)
            {
                Assert.NotNull(evt);
            }
            else
            {
                Assert.Null(evt);
            }
        }

        public static IEnumerable<object[]> GetMinimumLevelOverrideInheritanceTestCases()
        {
            // Visualizing the pipeline from left to right ....
            //
            //   Event  --> Root Logger --> Child Logger -> YES or
            //    lvl       override/lvl    override/levl     NO ?
            //
            object[] T(string rs, int? rl, string cs, int? cl, bool r)
            {
                return new object[] { rs, rl, cs, cl, r };
            }
            // numbers are relative to incoming event level
            // Information + 1 = Warning
            // Information - 1 = Debug
            //
            // Incoming event is Information
            // with SourceContext Root.N1.N2
            //
            // - default case - no overrides
            yield return T(null, 0, null, 0, true);
            // - root overrides with level lower or equal to event
            // ... and child logger is out of the way
            yield return T("Root", +0, null, +0, true);
            yield return T("Root", -1, null, +0, true);
            yield return T("Root.N1", +0, null, +0, true);
            yield return T("Root.N1", -1, null, +0, true);
            yield return T("Root.N1.N2", +0, null, +0, true);
            yield return T("Root.N1.N2", -1, null, +0, true);
            // - root overrides on irrelevant namespaces
            yield return T("xx", +1, null, +0, true);
            yield return T("Root.xx", +1, null, +0, true);
            yield return T("Root.N1.xx", +1, null, +0, true);
            // - child overrides on irrelevant namespaces
            yield return T(null, +0, "xx", +1, true);
            yield return T(null, +0, "Root.xx", +1, true);
            yield return T(null, +1, "Root.N1.xx", +1, true);
            // - root overrides prevent all processing from children
            // even though children would happily accept it
            yield return T("Root", +1, null, +0, false);
            yield return T("Root", +1, "Root", +0, false);
            yield return T("Root.N1", +1, null, +0, false);
            yield return T("Root.N1", +1, "Root.N1", +0, false);
            yield return T("Root.N1.N2", +1, null, +0, false);
            yield return T("Root.N1.N2", +1, "Root.N1.N2", +0, false);
        }

        [Theory]
        [MemberData(nameof(GetMinimumLevelOverrideInheritanceTestCases))]
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

            if (eventShouldGetToChild)
            {
                Assert.NotNull(evt);
            }
            else
            {
                Assert.Null(evt);
            }
        }

        [Theory]
        [MemberData(nameof(GetMinimumLevelOverrideInheritanceTestCases))]
        public void WriteToLoggerMinimumLevelOverrideInheritanceScenarios(
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

            var childLoggerConfig = new LoggerConfiguration()
                .MinimumLevel.Is(LevelAlias.Minimum);
            if (childOverrideSource != null)
            {
                childLoggerConfig.MinimumLevel.Override(childOverrideSource, childOverrideLevel);
            }
            childLoggerConfig.WriteTo.Sink(sink);
            var childLogger = childLoggerConfig.CreateLogger();

            var rootLoggerConfig = new LoggerConfiguration()
                .MinimumLevel.Is(LevelAlias.Minimum);

            if (rootOverrideSource != null)
            {
                rootLoggerConfig.MinimumLevel.Override(rootOverrideSource, rootOverrideLevel);
            }

            var logger = rootLoggerConfig
                .WriteTo.Logger(childLogger)
                .CreateLogger();

            logger
                .ForContext(Constants.SourceContextPropertyName, "Root.N1.N2")
                .Write(Some.LogEvent(level: incomingEventLevel));

            if (eventShouldGetToChild)
            {
                Assert.NotNull(evt);
            }
            else
            {
                Assert.Null(evt);
            }
        }
    }
}
