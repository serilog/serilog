using System;
using System.Collections.Generic;
using Serilog.Events;

namespace Serilog.Core
{
    public class FixedPropertyEnricher : ILogEventEnricher
    {
        private readonly IEnumerable<LogEventProperty> _logEventProperties;

        public FixedPropertyEnricher(IEnumerable<LogEventProperty> logEventProperties)
        {
            if (logEventProperties == null) throw new ArgumentNullException("logEventProperties");
            _logEventProperties = logEventProperties;
        }

        public void Enrich(LogEvent logEvent)
        {
            if (logEvent == null) throw new ArgumentNullException("logEvent");
            foreach (var property in _logEventProperties)
            {
                logEvent.AddPropertyIfAbsent(property);
            }
        }
    }
}