namespace Serilog.Tests.Support;

class CollectingEnricher : ILogEventEnricher
{
    public List<LogEvent> Events { get; } = [];

    public LogEvent SingleEvent => Events.Single();

    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        Events.Add(logEvent);
    }
}
