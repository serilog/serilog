using System.Threading;
using Opi;

namespace Harness
{
    class ThreadIdEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent)
        {
            logEvent.AddPropertyIfAbsent("thread-id", Thread.CurrentThread.ManagedThreadId);
        }
    }
}