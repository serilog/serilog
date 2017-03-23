using System;
using Serilog.Core;
using Serilog.Events;
using System.Threading.Tasks;

namespace Serilog.Tests.Support
{
    class DisposeTrackingSink : ILogEventSink, IDisposable
    {
        public bool IsDisposed { get; set; }

        public Task Emit(LogEvent logEvent)
        {
            return Task.FromResult((object)null);
        }

        public void Dispose()
        {
            IsDisposed = true;
        }
    }
}