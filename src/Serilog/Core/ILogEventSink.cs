using Serilog.Events;

namespace Serilog.Core
{
    public interface ILogEventSink
    {
        void Write(LogEvent logEvent);
    }
}