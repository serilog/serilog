using System.Threading;
using Opi;

namespace Harness
{
    class ThreadIdEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent)
        {
            logEvent.AddPropertyIfAbsent(
                new LogEventProperty("thread-id", LogEventPropertyValue.For(Thread.CurrentThread.ManagedThreadId)));
        }
    }
}