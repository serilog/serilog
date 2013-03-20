using System;
using System.IO;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;

namespace Serilog.Sinks.Trace
{
    class DiagnosticTraceSink : ILogEventSink
    {
        readonly ITextFormatter _textFormatter;

        public DiagnosticTraceSink(ITextFormatter textFormatter)
        {
            if (textFormatter == null) throw new ArgumentNullException("textFormatter");
            _textFormatter = textFormatter;
        }

        public void Write(LogEvent logEvent)
        {
            if (logEvent == null) throw new ArgumentNullException("logEvent");
            var sr = new StringWriter();
            _textFormatter.Format(logEvent, sr);
            System.Diagnostics.Trace.Write(sr.ToString());
        }
    }
}
