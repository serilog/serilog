using System;
using System.Collections.Generic;

namespace Opi.Core
{
    class AggregateSink : ILogEventSink
    {
        private readonly IEnumerable<ILogEventSink> _sinks;

        public AggregateSink(IEnumerable<ILogEventSink> sinks)
        {
            if (sinks == null) throw new ArgumentNullException("sinks");
            _sinks = sinks;
        }

        public void Write(LogEvent logEvent)
        {
            foreach (var sink in _sinks)
            {
                sink.Write(logEvent);
            }
        }
    }
}
