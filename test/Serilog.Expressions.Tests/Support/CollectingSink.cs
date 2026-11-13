using Serilog.Core;
using Serilog.Events;

namespace Serilog.Expressions.Tests.Support;

class CollectingSink : ILogEventSink
{
    readonly List<LogEvent> _events = new();

    public List<LogEvent> Events { get { return _events; } }

    public LogEvent SingleEvent { get { return _events.Single(); } }

    public void Emit(LogEvent logEvent)
    {
        _events.Add(logEvent);
    }
}