using System;
using Serilog.Core;
using Serilog.Events;

namespace Harness
{
    class HostNameEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            var property = propertyFactory.CreateProperty("HostName", Environment.MachineName);
            logEvent.AddPropertyIfAbsent(property);
        }
    }
}
