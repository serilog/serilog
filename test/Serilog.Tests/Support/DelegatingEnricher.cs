namespace Serilog.Tests.Support;

class DelegatingEnricher : ILogEventEnricher
{
    readonly Action<LogEvent, ILogEventPropertyFactory> _enrich;

    public DelegatingEnricher(Action<LogEvent, ILogEventPropertyFactory> enrich)
    {
        _enrich = Guard.AgainstNull(enrich);
    }

    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        _enrich(logEvent, propertyFactory);
    }
}
