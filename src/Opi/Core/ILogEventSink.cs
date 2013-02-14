namespace Opi.Core
{
    public interface ILogEventSink
    {
        void Write(LogEvent logEvent);
    }
}