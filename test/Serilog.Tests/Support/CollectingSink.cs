using Serilog.Core;
using Serilog.Events;
using System.Collections.Generic;
using System.Linq;

namespace Serilog.Tests.Support
{
    class CollectingSink : ILogEventSink
    {
        public List<LogEvent> Events { get; } = new List<LogEvent>();

        public LogEvent SingleEvent => Events.Single();

        public void Emit(LogEvent logEvent)
        {
            Events.Add(logEvent);
        }
    }
}
