using Serilog.Core;
using Serilog.Events;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;


namespace Serilog.Tests.Support
{
    /// <summary>
    /// Dummy sink which simply collects the events in memory.
    /// Thread-safe.
    /// NB: The order of the events is not guaranteed.
    /// </summary>
    class CollectingSink : ILogEventSink
    {
        private ConcurrentBag<LogEvent> mEvents = new ConcurrentBag<LogEvent>();

        public IReadOnlyCollection<LogEvent> Events => mEvents.ToList();

        public LogEvent SingleEvent => mEvents.Single();


        void ILogEventSink.Emit(LogEvent logEvent)
        {
            mEvents.Add(logEvent);
        }


        public void Clear()
        {
            // Clear() is missing in .NET Framework 4.x
            mEvents = new ConcurrentBag<LogEvent>();
        }
    }
}
