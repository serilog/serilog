namespace Serilog.Tests.Support;

class ListenerTrackingSink : ILogEventSink, ISetLoggingFailureListener
{
    public ILoggingFailureListener? Listener { get; private set; }

    public void Emit(LogEvent logEvent) { }

    public void SetFailureListener(ILoggingFailureListener failureListener)
    {
        Listener = failureListener;
    }
}
