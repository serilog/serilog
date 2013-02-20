using System.Collections.Concurrent;
using Serilog.Core;

namespace Serilog.Sinks.Http
{
    class HttpServerSink : ILogEventSink
    {
        BlockingCollection<LogEvent> _buffer;
        const int MaxBuffer = 1024; // events

        public HttpServerSink(string baseUrl)
        {
            _buffer = new BlockingCollection<LogEvent>(new ConcurrentQueue<LogEvent>(), MaxBuffer);
        }

        public void Write(LogEvent logEvent)
        {
        }
    }
}
