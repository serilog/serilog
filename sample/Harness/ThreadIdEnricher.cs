using System.Threading;
using Serilog;
using Serilog.Events;

namespace Harness
{
    class ThreadIdEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent)
        {
            logEvent.AddPropertyIfAbsent("ThreadId", Thread.CurrentThread.ManagedThreadId);
        }
    }
}