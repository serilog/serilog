using System;
using System.Collections.Generic;
using Serilog.Debugging;
using Serilog.Events;

namespace Serilog.Core
{
    class SafeAggregateSink : ILogEventSink
    {
        private readonly IEnumerable<ILogEventSink> _sinks;

        public SafeAggregateSink(IEnumerable<ILogEventSink> sinks)
        {
            if (sinks == null) throw new ArgumentNullException("sinks");
            _sinks = sinks;
        }

        public void Write(LogEvent logEvent)
        {
            foreach (var sink in _sinks)
            {
                try
                {
                    sink.Write(logEvent);
                }
                catch (Exception ex)
                {
                    SelfLog.WriteLine("Caught exception {0} while writing to sink {1}.", ex, sink);
                }
            }
        }
    }
}

