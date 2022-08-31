namespace Serilog.Tests.Support;

class CollectingSink : ILogEventSink
{
    public List<LogEvent> Events { get; } = new();

    public LogEvent SingleEvent => Events.Single();

    public void Emit(LogEvent logEvent)
    {
        Events.Add(logEvent);
    }
}
