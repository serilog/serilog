namespace Serilog.Events
{
    public interface ILogEventEnricher
    {
        void Enrich(LogEvent logEvent);
    }
}