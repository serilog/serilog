using System;

namespace Serilog.Core
{
    class RestrictedSink : ILogEventSink
    {
        private readonly ILogEventSink _sink;
        private readonly LogEventLevel _restrictedMinimumLevel;

        public RestrictedSink(ILogEventSink sink, LogEventLevel restrictedMinimumLevel)
        {
            if (sink == null) throw new ArgumentNullException("sink");
            _sink = sink;
            _restrictedMinimumLevel = restrictedMinimumLevel;
        }

        public void Write(LogEvent logEvent)
        {
            if (logEvent == null) throw new ArgumentNullException("logEvent");

            if (logEvent.Level < _restrictedMinimumLevel)
                return;

            _sink.Write(logEvent);
        }
    }
}
