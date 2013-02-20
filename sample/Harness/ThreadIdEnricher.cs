using System.Threading;
using Serilog;

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