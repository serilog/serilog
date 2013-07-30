using System;
using Serilog.Events;

namespace Serilog.Sinks.Glimpse
{
    internal class LogEventMessage
    {
        internal LogEventMessage(LogEvent logEvent, IFormatProvider formatProvider)
        {
            LogEvent = logEvent;
            FormatProvider = formatProvider;
        }

        public LogEvent LogEvent { get; set; }
        public IFormatProvider FormatProvider { get; set; }
    }
}