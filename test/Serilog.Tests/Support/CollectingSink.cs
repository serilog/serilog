using System;
using System.Collections.Generic;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.Tests.Support
{
    public class CollectingSink : ILogEventSink
    {
        readonly List<LogEvent> _received = new List<LogEvent>();

        public void Emit(LogEvent logEvent)
        {
            if (logEvent == null) throw new ArgumentNullException("logEvent");
            _received.Add(logEvent);
        }

        public List<LogEvent> Received { get { return _received; } } 
    }
}