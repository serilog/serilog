namespace Serilog.Tests.Core;

public class ChildLoggerKnownLimitationsTests
{
    [Fact]
    public void SpecifyingMinimumLevelOverridesInWriteToLoggerWithConfigCallBackWritesWarningToSelfLog()
    {
        var outputs = new List<string>();
        using (TemporarySelfLog.SaveTo(outputs))
        {
            var configCallBackSink = new CollectingSink();

            var logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .MinimumLevel.Override("Foo.Bar", Warning)
                .WriteTo.Logger(lc => lc
                    .MinimumLevel.Verbose()
                    .MinimumLevel.Override("Foo.Bar", Debug)
                    .WriteTo.Sink(configCallBackSink))
                .CreateLogger();

            var contextLogger = logger.ForContext(Constants.SourceContextPropertyName, "Foo.Bar");
            contextLogger.Write(Some.InformationEvent());
        }

        Assert.EndsWith("Minimum level overrides are not supported on sub-loggers " +
                        "and may be removed completely in a future version.",
            outputs.FirstOrDefault() ?? "");
    }

    [Fact]
    public void SpecifyingMinimumLevelOverridesInWriteToLoggerWritesWarningToSelfLog()
    {
        var outputs = new List<string>();
        using (TemporarySelfLog.SaveTo(outputs))
        {
            var subSink = new CollectingSink();

            var subLogger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .MinimumLevel.Override("Foo.Bar", Debug)
                .WriteTo.Sink(subSink)
                .CreateLogger();

            var logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .MinimumLevel.Override("Foo.Bar", Warning)
                .WriteTo.Logger(subLogger)
                .CreateLogger();

            var contextLogger = logger.ForContext(Constants.SourceContextPropertyName, "Foo.Bar");
            contextLogger.Write(Some.InformationEvent());
        }

        Assert.EndsWith("Minimum level overrides are not supported on sub-loggers " +
                        "and may be removed completely in a future version.",
            outputs.FirstOrDefault() ?? "");
    }

    public static IEnumerable<object?[]> GetMinimumLevelOverrideInheritanceTestCases()
    {
        // Visualizing the pipeline from left to right ....
        //
        //   Event  --> Root Logger --> Child Logger
        //    lvl       override/lvl    override/levl
        //
        static object?[] T(string? rs, int? rl, string cs, int? cl)
        {
            return new object?[] { rs, rl, cs, cl };
        }
        // numbers are relative to incoming event level
        // Information + 1 = Warning
        // Information - 1 = Debug
        //
        // Incoming event is Information
        // with SourceContext Root.N1.N2
        //
        // - no root overrides but children has its own
        yield return T(null, +0, "Root", +1);
        yield return T(null, +0, "Root.N1", +1);
        yield return T(null, +0, "Root.N1.N2", +1);
        // - root overrides let it through but child rejects it
        yield return T("Root", +0, "Root", +1);
        yield return T("Root.N1", +0, "Root", +1);
        yield return T("Root.N1.N2", +0, "Root", +1);
        yield return T("Root", +0, "Root.N1", +1);
        yield return T("Root.N1", +0, "Root.N1", +1);
        yield return T("Root.N1.N2", +0, "Root.N1", +1);
        yield return T("Root", +0, "Root.N1.N2", +1);
        yield return T("Root.N1", +0, "Root.N1.N2", +1);
        yield return T("Root.N1.N2", +0, "Root.N1.N2", +1);
    }

    [Theory]
    [MemberData(nameof(GetMinimumLevelOverrideInheritanceTestCases))]
    public void WriteToLoggerWithConfigCallbackMinimumLevelOverrideInheritanceNotSupportedScenarios(
        string? rootOverrideSource,
        int rootOverrideLevelIncrement,
        string? childOverrideSource,
        int childOverrideLevelIncrement)
    {
        var incomingEventLevel = Information;
        var rootOverrideLevel = incomingEventLevel + rootOverrideLevelIncrement;
        var childOverrideLevel = incomingEventLevel + childOverrideLevelIncrement;

        LogEvent? evt = null;
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

        // even though the user may expect no event
        Assert.NotNull(evt);
    }

    [Theory]
    [MemberData(nameof(GetMinimumLevelOverrideInheritanceTestCases))]
    public void WriteToLoggerMinimumLevelOverrideInheritanceNotSupportedScenarios(
        string? rootOverrideSource,
        int rootOverrideLevelIncrement,
        string? childOverrideSource,
        int childOverrideLevelIncrement)
    {
        var incomingEventLevel = Information;
        var rootOverrideLevel = incomingEventLevel + rootOverrideLevelIncrement;
        var childOverrideLevel = incomingEventLevel + childOverrideLevelIncrement;

        LogEvent? evt = null;
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

        // even though the use may expect no event
        Assert.NotNull(evt);
    }
}
