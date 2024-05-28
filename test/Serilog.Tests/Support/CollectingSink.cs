namespace Serilog.Tests.Support;

class CollectingSink : ILogEventSink
{
    public List<LogEvent> Events { get; } = [];

    public LogEvent SingleEvent => Events.Single();

    public void Emit(LogEvent logEvent)
    {
        Events.Add(logEvent);
    }
}
