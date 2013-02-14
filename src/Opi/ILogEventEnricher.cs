namespace Opi
{
    public interface ILogEventEnricher
    {
        void Enrich(LogEvent logEvent);
    }
}