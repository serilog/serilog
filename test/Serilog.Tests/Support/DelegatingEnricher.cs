using System;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.Tests.Support
{
    class DelegatingEnricher : ILogEventEnricher
    {
        readonly Action<LogEvent, ILogEventPropertyFactory> _enrich;

        public DelegatingEnricher(Action<LogEvent, ILogEventPropertyFactory> enrich)
        {
            if (enrich == null) throw new ArgumentNullException(nameof(enrich));
            _enrich = enrich;
        }

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            _enrich(logEvent, propertyFactory);
        }
    }
}
