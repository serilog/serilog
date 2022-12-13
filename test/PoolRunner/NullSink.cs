using Serilog.Core;
using Serilog.Events;

namespace PoolRunner;

internal class NullSink : ILogEventSink
{
    public void Emit(LogEvent logEvent)
    {
    }
}
