using System;
using System.IO;
using Serilog.Core;
using Serilog.Display;
using Serilog.Events;

namespace Serilog.Sinks.Trace
{
    class DiagnosticTraceSink : ILogEventSink
    {
        readonly IDisplayFormatter _displayFormatter;

        public DiagnosticTraceSink(IDisplayFormatter displayFormatter)
        {
            if (displayFormatter == null) throw new ArgumentNullException("displayFormatter");
            _displayFormatter = displayFormatter;
        }

        public void Write(LogEvent logEvent)
        {
            if (logEvent == null) throw new ArgumentNullException("logEvent");
            var sr = new StringWriter();
            _displayFormatter.Format(logEvent, sr);
            System.Diagnostics.Trace.Write(sr.ToString());
        }
    }
}
