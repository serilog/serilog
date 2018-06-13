using System.Collections.Generic;
using System.Linq;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.Tests.Support
{
    class DecorateSink : ILogEventSink
    {
        public DecorateSink(ILogEventSink decoratedSink)
        {
            _decoratedSink = decoratedSink;
        }

        readonly List<LogEvent> _events = new List<LogEvent>();
        private readonly ILogEventSink _decoratedSink;

        public List<LogEvent> Events { get { return _events; } }

        public LogEvent SingleEvent { get { return _events.Single(); } }
 
        public void Emit(LogEvent logEvent)
        {
            _events.Add(logEvent);
            _decoratedSink.Emit(logEvent);
        }
    }
}
