using System;
using Serilog.Core;
using Serilog.Events;

namespace Harness
{
    class HostNameEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent)
        {
            logEvent.AddPropertyIfAbsent("HostName", Environment.MachineName);
        }
    }
}