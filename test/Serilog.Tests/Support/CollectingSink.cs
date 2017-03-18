using System.Collections.Generic;
using System.Linq;
using Serilog.Core;
using Serilog.Events;
using System.Threading.Tasks;

namespace Serilog.Tests.Support
{
    class CollectingSink : ILogEventSink
    {
        readonly List<LogEvent> _events = new List<LogEvent>();

        public List<LogEvent> Events { get { return _events; } }

        public LogEvent SingleEvent { get { return _events.Single(); } }
 
        public Task Emit(LogEvent logEvent)
        {
            _events.Add(logEvent);
            return Task.FromResult((object)null);
        }
    }
}
