namespace Serilog.Tests.Support;

sealed class InMemoryBatchedSink(TimeSpan batchEmitDelay) : IBatchedLogEventSink, IDisposable
#if FEATURE_ASYNCDISPOSABLE
    , IAsyncDisposable
#endif
{
    readonly object _stateLock = new();
    readonly SyncState _syncState = new();

    sealed class SyncState
    {
        public bool Stopped { get; set; }
        public bool WasCalledAfterDisposal { get; set; }
        public List<IReadOnlyCollection<LogEvent>> Batches { get; } = new();
        public bool IsDisposed { get; set; }
        public bool IsDisposedAsync { get; set; }
    }

    public bool WasCalledAfterDisposal
    {
        get
        {
            lock (_stateLock)
                return _syncState.WasCalledAfterDisposal;
        }
    }

    public IReadOnlyList<IReadOnlyCollection<LogEvent>> Batches
    {
        get
        {
            lock (_stateLock)
                return _syncState.Batches.ToList();
        }
    }

    public bool IsDisposed
    {
        get
        {
            lock (_stateLock)
                return _syncState.IsDisposed;
        }
    }

    public bool IsDisposedAsync
    {
        get
        {
            lock (_stateLock)
                return _syncState.IsDisposedAsync;
        }
    }

    public void Stop()
    {
        lock (_stateLock)
        {
            _syncState.Stopped = true;
        }
    }

    public Task EmitBatchAsync(IReadOnlyCollection<LogEvent> events)
    {
        lock (_stateLock)
        {
            if (_syncState.Stopped)
                return Task.CompletedTask;

            if (IsDisposed)
                _syncState.WasCalledAfterDisposal = true;

            Thread.Sleep(batchEmitDelay);
            _syncState.Batches.Add(events.ToList());
        }

        return Task.CompletedTask;
    }

    public Task OnEmptyBatchAsync() => Task.CompletedTask;

    public void Dispose()
    {
        lock (_stateLock)
            _syncState.IsDisposed = true;
    }

#if FEATURE_ASYNCDISPOSABLE
    public ValueTask DisposeAsync()
    {
        lock (_stateLock)
        {
            _syncState.IsDisposedAsync = true;
            Dispose();
            return default;
        }
    }
#endif
}
