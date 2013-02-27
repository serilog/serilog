using System;

namespace Serilog.Tests.Support
{
    class DelegatingEnricher : ILogEventEnricher
    {
        readonly Action<LogEvent> _enrich;

        public DelegatingEnricher(Action<LogEvent> enrich)
        {
            if (enrich == null) throw new ArgumentNullException("enrich");
            _enrich = enrich;
        }

        public void Enrich(LogEvent logEvent)
        {
            _enrich(logEvent);
        }
    }
}
