namespace TestDummies;

public static class DummyLoggerConfigurationExtensions
{
    public static LoggerConfiguration WithDummyThreadId(this LoggerEnrichmentConfiguration enrich)
    {
        return enrich.With(new DummyThreadIdEnricher());
    }

    public static LoggerConfiguration DummyRollingFile(
        this LoggerSinkConfiguration loggerSinkConfiguration,
        string pathFormat,
        LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
        string? outputTemplate = null,
        IFormatProvider? formatProvider = null)
    {
        return loggerSinkConfiguration.Sink(new DummyRollingFileSink(), restrictedToMinimumLevel);
    }

    public static LoggerConfiguration DummyRollingFile(
        this LoggerSinkConfiguration loggerSinkConfiguration,
        ITextFormatter formatter,
        string pathFormat,
        LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum)
    {
        return loggerSinkConfiguration.Sink(new DummyRollingFileSink(), restrictedToMinimumLevel);
    }

    public static LoggerConfiguration DummyRollingFile(
        this LoggerAuditSinkConfiguration loggerSinkConfiguration,
        LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum)
    {
        return loggerSinkConfiguration.Sink(new DummyRollingFileAuditSink(), restrictedToMinimumLevel);
    }

    public static LoggerConfiguration DummyWithLevelSwitch(
        this LoggerSinkConfiguration loggerSinkConfiguration,
        LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
        LoggingLevelSwitch? controlLevelSwitch = null)
    {
        return loggerSinkConfiguration.Sink(new DummyWithLevelSwitchSink(controlLevelSwitch), restrictedToMinimumLevel);
    }

    public static LoggerConfiguration DummyConsole(
        this LoggerSinkConfiguration loggerSinkConfiguration,
        LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
        ConsoleTheme? theme = null)
    {
        return loggerSinkConfiguration.Sink(new DummyConsoleSink(theme), restrictedToMinimumLevel);
    }

    public static LoggerConfiguration DummyWrapper(
        this LoggerSinkConfiguration loggerSinkConfiguration,
        Action<LoggerSinkConfiguration> wrappedSinkAction)
    {
        return loggerSinkConfiguration.Sink(LoggerSinkConfiguration.Wrap(
            s => new DummyWrappingSink(s),
            wrappedSinkAction));
    }

    public static LoggerConfiguration DummyWrapper(
        this LoggerSinkConfiguration loggerSinkConfiguration,
        Action<LoggerSinkConfiguration> wrappedSinkAction,
        LogEventLevel logEventLevel,
        LoggingLevelSwitch? levelSwitch)
    {
        return loggerSinkConfiguration.Sink(LoggerSinkConfiguration.Wrap(
            s => new DummyWrappingSink(s),
            wrappedSinkAction),
            logEventLevel,
            levelSwitch);
    }

    public static LoggerConfiguration WithDummyHardCodedString(
        this LoggerDestructuringConfiguration loggerDestructuringConfiguration,
        string hardCodedString)
    {
        return loggerDestructuringConfiguration.With(new DummyHardCodedStringDestructuringPolicy(hardCodedString));
    }
}
