using Serilog.Events;
using Serilog.Core;

namespace Serilog.PerformanceTests.Support
{
    class NullSink : ILogEventSink
    {
        public void Emit(LogEvent logEvent)
        {
        }
    }
}
