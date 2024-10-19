namespace Serilog.Tests.Support;

public class CollectingFailureListener: ILoggingFailureListener
{
    public readonly record struct LoggingFailure(
        object Sender,
        LoggingFailureKind Kind,
        string Message,
        IReadOnlyCollection<LogEvent>? Events,
        Exception? Exception);

    public List<LoggingFailure> Failures { get; } = new();

    public void OnLoggingFailed(
        object sender,
        LoggingFailureKind kind,
        string message,
        IReadOnlyCollection<LogEvent>? events,
        Exception? exception)
    {
        Failures.Add(new(sender, kind, message, events, exception));
    }
}