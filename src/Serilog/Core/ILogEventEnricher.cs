using Serilog.Events;

namespace Serilog.Core
{
    public interface ILogEventEnricher
    {
        void Enrich(LogEvent logEvent);
    }
}