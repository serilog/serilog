namespace Serilog.Tests.Support;

public class DelegatingSink : ILogEventSink
{
    readonly Action<LogEvent> _write;

    public DelegatingSink(Action<LogEvent> write)
    {
        _write = write ?? throw new ArgumentNullException(nameof(write));
    }

    public void Emit(LogEvent logEvent)
    {
        _write(logEvent);
    }

    public static LogEvent GetLogEvent(Action<ILogger> writeAction)
    {
        LogEvent? result = null;
        var l = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .WriteTo.Sink(new DelegatingSink(le => result = le))
            .CreateLogger();

        writeAction(l);
        Assert.NotNull(result);
        return result!;
    }
}
