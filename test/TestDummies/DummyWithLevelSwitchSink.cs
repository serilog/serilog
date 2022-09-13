namespace TestDummies;

public class DummyWithLevelSwitchSink : ILogEventSink
{
    public DummyWithLevelSwitchSink(LoggingLevelSwitch? loggingControlLevelSwitch)
    {
        ControlLevelSwitch = loggingControlLevelSwitch;
    }

    [ThreadStatic]
    public static LoggingLevelSwitch? ControlLevelSwitch;

    [ThreadStatic]
    // ReSharper disable ThreadStaticFieldHasInitializer
    public static List<LogEvent> Emitted = new();
    // ReSharper restore ThreadStaticFieldHasInitializer

    public void Emit(LogEvent logEvent)
    {
        Emitted.Add(logEvent);
    }
}
