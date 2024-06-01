namespace Serilog.Tests.Support;

sealed class InMemoryBatchedSink(TimeSpan batchEmitDelay) : IBatchedLogEventSink, IDisposable
#if FEATURE_ASYNCDISPOSABLE
    , IAsyncDisposable
#endif
{
    readonly object _stateLock = new();
    bool _stopped;

    // Postmortem only
    public bool WasCalledAfterDisposal { get; private set; }
    public IList<IList<LogEvent>> Batches { get; } = new List<IList<LogEvent>>();
    public bool IsDisposed { get; private set; }

    public void Stop()
    {
        lock (_stateLock)
        {
            _stopped = true;
        }
    }

    public Task EmitBatchAsync(IReadOnlyCollection<LogEvent> events)
    {
        lock (_stateLock)
        {
            if (_stopped)
                return Task.CompletedTask;

            if (IsDisposed)
                WasCalledAfterDisposal = true;

            Thread.Sleep(batchEmitDelay);
            Batches.Add(events.ToList());
        }

        return Task.CompletedTask;
    }

    public Task OnEmptyBatchAsync() => Task.CompletedTask;

    public void Dispose()
    {
        lock (_stateLock)
            IsDisposed = true;
    }

#if FEATURE_ASYNCDISPOSABLE
    public bool IsDisposedAsync { get; private set; }

    public ValueTask DisposeAsync()
    {
        lock (_stateLock)
        {
            IsDisposedAsync = true;
            Dispose();
            return default;
        }
    }
#endif
}
