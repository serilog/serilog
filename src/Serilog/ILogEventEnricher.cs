namespace Serilog
{
    public interface ILogEventEnricher
    {
        void Enrich(LogEvent logEvent);
    }
}