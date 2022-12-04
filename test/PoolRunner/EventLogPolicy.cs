using Microsoft.Extensions.ObjectPool;
using Serilog.Events;

namespace PoolRunner;

internal class EventLogPolicy : PooledObjectPolicy<LogEvent>
{
    public override LogEvent Create()
    {
        return new LogEvent();
    }

    public override bool Return(LogEvent obj)
    {
        obj.Reset();
        return true;
    }
}
