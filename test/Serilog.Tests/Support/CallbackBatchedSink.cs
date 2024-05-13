namespace Serilog.Tests.Support;

class CallbackBatchedSink(Func<IEnumerable<LogEvent>, Task> callback) : IBatchedLogEventSink
{
    public Task EmitBatchAsync(IReadOnlyCollection<LogEvent> batch)
    {
        return callback(batch);
    }

    public Task OnEmptyBatchAsync()
    {
        return Task.CompletedTask;
    }
}
