using Serilog.Core;
using Serilog.Events;
using System;

namespace TestDummies
{
    public class DummyDisposableSink : ILogEventSink, IDisposable
    {
        public bool IsDisposed { get; private set; }

        public void Dispose()
        {
            IsDisposed = true;
        }

        public void Emit(LogEvent logEvent)
        {
        }
    }
}
