namespace TestDummies.Console;

public class DummyConsoleSink : ILogEventSink
{
    public DummyConsoleSink(ConsoleTheme? theme = null)
    {
        Theme = theme ?? ConsoleTheme.None;
    }

    [ThreadStatic]
    public static ConsoleTheme? Theme;

    public void Emit(LogEvent logEvent)
    {
    }
}
