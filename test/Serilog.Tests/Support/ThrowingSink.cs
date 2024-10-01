namespace Serilog.Tests.Support;

class ThrowingSink : ILogEventSink
{
    public void Emit(LogEvent logEvent)
    {
        throw new InvalidOperationException("Failed.");
    }
}