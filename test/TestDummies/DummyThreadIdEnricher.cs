using Serilog.Core;
using Serilog.Events;

namespace TestDummies;

public class DummyThreadIdEnricher : ILogEventEnricher
{
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        logEvent.AddPropertyIfAbsent(propertyFactory
            .CreateProperty("ThreadId", "SomeId"));
    }
}
