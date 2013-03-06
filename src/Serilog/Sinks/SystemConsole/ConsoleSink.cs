using System;
using Serilog.Core;
using Serilog.Display;
using Serilog.Events;

namespace Serilog.Sinks.SystemConsole
{
    class ConsoleSink : ILogEventSink
    {
        readonly IDisplayFormatter _displayFormatter;

        public ConsoleSink(IDisplayFormatter displayFormatter)
        {
            if (displayFormatter == null) throw new ArgumentNullException("displayFormatter");
            _displayFormatter = displayFormatter;
        }

        public void Write(LogEvent logEvent)
        {
            if (logEvent == null) throw new ArgumentNullException("logEvent");
            _displayFormatter.Format(logEvent, Console.Out);
        }
    }
}
