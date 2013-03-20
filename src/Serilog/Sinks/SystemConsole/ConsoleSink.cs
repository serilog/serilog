using System;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;

namespace Serilog.Sinks.SystemConsole
{
    class ConsoleSink : ILogEventSink
    {
        readonly ITextFormatter _textFormatter;

        public ConsoleSink(ITextFormatter textFormatter)
        {
            if (textFormatter == null) throw new ArgumentNullException("textFormatter");
            _textFormatter = textFormatter;
        }

        public void Write(LogEvent logEvent)
        {
            if (logEvent == null) throw new ArgumentNullException("logEvent");
            _textFormatter.Format(logEvent, Console.Out);
        }
    }
}
