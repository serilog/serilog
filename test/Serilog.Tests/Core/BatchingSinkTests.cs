using Serilog.Core.Sinks.Batching;

namespace Serilog.Core.Tests;

public class BatchingSinkTests
{
    static readonly TimeSpan TinyWait = TimeSpan.FromMilliseconds(500);
    static readonly TimeSpan MicroWait = TimeSpan.FromMilliseconds(1);

    [Fact]
    public void WhenAnEventIsEnqueuedItIsWrittenToABatchOnDispose()
    {
        var bs = new InMemoryBatchedSink(TimeSpan.Zero);
        var pbs = new BatchingSink(bs, new() { BatchSizeLimit = 2, BufferingTimeLimit = TinyWait, EagerlyEmitFirstEvent = true });
        var evt = Some.InformationEvent();
        pbs.Emit(evt);
        pbs.Dispose();
        var batch = Assert.Single(bs.Batches);
        var actual = Assert.Single(batch);
        Assert.Same(evt, actual);
        Assert.True(bs.IsDisposed);
        Assert.False(bs.WasCalledAfterDisposal);
    }

#if FEATURE_ASYNCDISPOSABLE
        [Fact]
        public async Task WhenAnEventIsEnqueuedItIsWrittenToABatchOnDisposeAsync()
        {
            var bs = new InMemoryBatchedSink(TimeSpan.Zero);
            var pbs = new BatchingSink(
                bs, new()
                {
                    BatchSizeLimit = 2, BufferingTimeLimit = TinyWait, EagerlyEmitFirstEvent = true
                });
            var evt = Some.InformationEvent();
            pbs.Emit(evt);
            await pbs.DisposeAsync();
            var batch = Assert.Single(bs.Batches);
            var actual = Assert.Single(batch);
            Assert.Same(evt, actual);
            Assert.True(bs.IsDisposed);
            Assert.True(bs.IsDisposedAsync);
            Assert.False(bs.WasCalledAfterDisposal);
        }
#endif

    [Fact]
    public void WhenAnEventIsEnqueuedItIsWrittenToABatchOnTimer()
    {
        var bs = new InMemoryBatchedSink(TimeSpan.Zero);
        var pbs = new BatchingSink(
            bs,
            new()
            {
                BatchSizeLimit = 2, BufferingTimeLimit = TinyWait, EagerlyEmitFirstEvent = true
            });
        var evt = Some.InformationEvent();
        pbs.Emit(evt);
        Thread.Sleep(TinyWait + TinyWait);
        bs.Stop();
        pbs.Dispose();
        Assert.Single(bs.Batches);
        Assert.True(bs.IsDisposed);
        Assert.False(bs.WasCalledAfterDisposal);
    }

    [Fact]
    public void WhenAnEventIsEnqueuedItIsWrittenToABatchOnDisposeWhileRunning()
    {
        var bs = new InMemoryBatchedSink(TinyWait + TinyWait);
        var pbs = new BatchingSink(bs, new() { BatchSizeLimit = 2, BufferingTimeLimit = MicroWait, EagerlyEmitFirstEvent = true });
        var evt = Some.InformationEvent();
        pbs.Emit(evt);
        Thread.Sleep(TinyWait);
        pbs.Dispose();
        Assert.Single(bs.Batches);
        Assert.True(bs.IsDisposed);
        Assert.False(bs.WasCalledAfterDisposal);
    }

    [Fact]
    public void ExecutionContextDoesNotFlowToBatchedSink()
    {
        var local = new AsyncLocal<int>
        {
            Value = 5
        };

        var observed = 17;
        var bs = new CallbackBatchedSink(_ =>
        {
            observed = local.Value;
            return Task.CompletedTask;
        });

        var pbs = new BatchingSink(bs, new());
        var evt = Some.InformationEvent();
        pbs.Emit(evt);
        pbs.Dispose();

        Assert.Equal(default, observed);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task EagerlyEmitFirstEventCausesQuickInitialBatch(bool eagerlyEmit)
    {
        long emitted = 0;
        var bs = new CallbackBatchedSink(_ =>
        {
            // ReSharper disable once AccessToModifiedClosure
            Interlocked.Increment(ref emitted);
            return Task.CompletedTask;
        });

        var options = new BatchingOptions
        {
            BufferingTimeLimit = TimeSpan.FromSeconds(3),
            EagerlyEmitFirstEvent = eagerlyEmit,
            BatchSizeLimit = 10,
            QueueLimit = 1000
        };

        var pbs = new BatchingSink(bs, options);

        var evt = Some.InformationEvent();
        pbs.Emit(evt);

        await Task.Delay(1900);
        Assert.Equal(eagerlyEmit, Interlocked.Read(ref emitted) == 1);

#if FEATURE_ASYNCDISPOSABLE
        await pbs.DisposeAsync();
#else
        pbs.Dispose();
#endif

        Assert.Equal(1, Interlocked.Read(ref emitted));
    }

    [Fact]
    public void ABatchWaitingToBeRetriedAtShutdownIsReportedAsDroppedAndReachesTheFallbackSink()
    {
        const int eventCount = 20;
        var attempts = 0;
        var bs = new CallbackBatchedSink(_ =>
        {
            Interlocked.Increment(ref attempts);
            throw new InvalidOperationException("sink down");
        });

        // The whole first batch is pulled from the queue, and the long buffering time limit keeps it waiting for a
        // retry until shutdown; the longer retry time limit stops it being dropped before then.
        var pbs = new BatchingSink(bs, new() { BatchSizeLimit = eventCount, BufferingTimeLimit = TimeSpan.FromHours(1), RetryTimeLimit = TimeSpan.FromDays(1), EagerlyEmitFirstEvent = false });
        var recorder = new RecordingFailureListener();
        var fallback = new CollectingSink();
        var logger = new LoggerConfiguration()
            .WriteTo.FallbackChain(
                wt => wt.Sink(new ListenerInterceptingSink(pbs, recorder)),
                wt => wt.Sink(fallback))
            .CreateLogger();

        var events = Enumerable.Range(0, eventCount).Select(_ => Some.InformationEvent()).ToList();
        foreach (var evt in events)
            logger.Write(evt);

        // Once the failure has been reported, the batch is retained for a retry.
        Assert.True(recorder.TemporaryReports.Wait(TimeSpan.FromSeconds(30)));

        logger.Dispose();

        Assert.Equal(1, attempts);
        Assert.Collection(
            recorder.Failures,
            f =>
            {
                Assert.Equal(LoggingFailureKind.Temporary, f.Kind);
                Assert.Equal(events, f.Events);
            },
            f =>
            {
                Assert.Equal(LoggingFailureKind.Permanent, f.Kind);
                Assert.Equal(events, f.Events);
                Assert.IsType<InvalidOperationException>(f.Exception);
            });
        Assert.Equal(events, fallback.Events);
    }

    [Fact]
    public void APartialBatchWaitingToBeRetriedAtShutdownIsReportedAsDroppedWithoutAnotherAttempt()
    {
        const int eventCount = 20;
        var firstAttemptStarted = new ManualResetEventSlim();
        var releaseFirstAttempt = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        var bs = new ScriptedBatchedSink(beforeAttempt: n =>
        {
            if (n != 1) return Task.CompletedTask;
            firstAttemptStarted.Set();
            return releaseFirstAttempt.Task;
        });

        // As in the issue, the batch size limit is far above the number of events, so after a failure the batch
        // waits for its retry in the next fill wait. After the second failure, that wait is at least ten seconds.
        var pbs = new BatchingSink(bs, new() { BatchSizeLimit = 1000, BufferingTimeLimit = TimeSpan.FromMilliseconds(10), EagerlyEmitFirstEvent = true });
        var recorder = new RecordingFailureListener();
        var fallback = new CollectingSink();
        var logger = new LoggerConfiguration()
            .WriteTo.FallbackChain(
                wt => wt.Sink(new ListenerInterceptingSink(pbs, recorder)),
                wt => wt.Sink(fallback))
            .CreateLogger();

        var events = Enumerable.Range(0, eventCount).Select(_ => Some.InformationEvent()).ToList();

        // The first event is emitted eagerly, and the rest are queued while that attempt is in progress, so the
        // retry includes all of them.
        logger.Write(events[0]);
        Assert.True(firstAttemptStarted.Wait(TimeSpan.FromSeconds(30)));
        foreach (var evt in events.Skip(1))
            logger.Write(evt);
        releaseFirstAttempt.SetResult(true);

        Assert.True(recorder.TemporaryReports.Wait(TimeSpan.FromSeconds(30)));
        Assert.True(recorder.TemporaryReports.Wait(TimeSpan.FromSeconds(30)));

        logger.Dispose();

        Assert.Collection(
            bs.Attempts,
            a => Assert.Equal([events[0]], a),
            a => Assert.Equal(events, a));
        Assert.Collection(
            recorder.Failures,
            f =>
            {
                Assert.Equal(LoggingFailureKind.Temporary, f.Kind);
                Assert.Equal([events[0]], f.Events);
            },
            f =>
            {
                Assert.Equal(LoggingFailureKind.Temporary, f.Kind);
                Assert.Equal(events, f.Events);
            },
            f =>
            {
                Assert.Equal(LoggingFailureKind.Permanent, f.Kind);
                Assert.Equal(events, f.Events);
                Assert.IsType<InvalidOperationException>(f.Exception);
            });
        Assert.Equal(events, fallback.Events);
    }

    [Fact]
    public void EventsAddedToABatchWaitingToBeRetriedAreFlushedAtShutdown()
    {
        var secondAttemptStarted = new ManualResetEventSlim();
        var releaseSecondAttempt = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        var bs = new ScriptedBatchedSink(
            beforeAttempt: n =>
            {
                if (n != 2) return Task.CompletedTask;
                secondAttemptStarted.Set();
                return releaseSecondAttempt.Task;
            },
            shouldFail: n => n <= 2);

        var pbs = new BatchingSink(bs, new() { BatchSizeLimit = 1000, BufferingTimeLimit = TimeSpan.FromMilliseconds(10), EagerlyEmitFirstEvent = true });
        var recorder = new RecordingFailureListener();
        var fallback = new CollectingSink();
        var logger = new LoggerConfiguration()
            .WriteTo.FallbackChain(
                wt => wt.Sink(new ListenerInterceptingSink(pbs, recorder)),
                wt => wt.Sink(fallback))
            .CreateLogger();

        var attempted = Some.InformationEvent();
        var added = Some.InformationEvent();

        logger.Write(attempted);

        // Queued while the retry is in progress, `added` joins the batch as it waits for its next retry.
        Assert.True(secondAttemptStarted.Wait(TimeSpan.FromSeconds(30)));
        logger.Write(added);
        releaseSecondAttempt.SetResult(true);

        Assert.True(recorder.TemporaryReports.Wait(TimeSpan.FromSeconds(30)));
        Assert.True(recorder.TemporaryReports.Wait(TimeSpan.FromSeconds(30)));

        logger.Dispose();

        Assert.Collection(
            bs.Attempts,
            a => Assert.Equal([attempted], a),
            a => Assert.Equal([attempted], a),
            a => Assert.Equal([added], a));
        Assert.Collection(
            recorder.Failures,
            f => Assert.Equal(LoggingFailureKind.Temporary, f.Kind),
            f => Assert.Equal(LoggingFailureKind.Temporary, f.Kind),
            f =>
            {
                Assert.Equal(LoggingFailureKind.Permanent, f.Kind);
                Assert.Equal([attempted], f.Events);
            });
        Assert.Equal([attempted], fallback.Events);
    }

    [Fact]
    public void QueuedEventsAreFlushedAtShutdownWhenReportingAWaitingBatchThrows()
    {
        var bs = new ScriptedBatchedSink(shouldFail: n => n == 1);
        var recorder = new RecordingFailureListener { ThrowOnPermanent = true };
        var pbs = new BatchingSink(bs, new() { BatchSizeLimit = 3, BufferingTimeLimit = TimeSpan.FromHours(1), RetryTimeLimit = TimeSpan.FromDays(1), EagerlyEmitFirstEvent = false });
        pbs.SetFailureListener(recorder);

        var events = Enumerable.Range(0, 4).Select(_ => Some.InformationEvent()).ToList();
        foreach (var evt in events.Take(3))
            pbs.Emit(evt);

        // The failed batch waits out its backoff without reading from the queue, so the last event stays queued.
        Assert.True(recorder.TemporaryReports.Wait(TimeSpan.FromSeconds(30)));
        pbs.Emit(events[3]);

        pbs.Dispose();

        Assert.Collection(
            bs.Attempts,
            a => Assert.Equal(events.Take(3), a),
            a => Assert.Equal([events[3]], a));
        Assert.Collection(
            recorder.Failures,
            f => Assert.Equal(LoggingFailureKind.Temporary, f.Kind),
            f =>
            {
                Assert.Equal(LoggingFailureKind.Permanent, f.Kind);
                Assert.Equal(events.Take(3), f.Events);
            },
            f =>
            {
                // The listener's exception still faults the sink's worker, and is reported by `Dispose()`.
                Assert.Equal(LoggingFailureKind.Final, f.Kind);
                Assert.IsType<NotSupportedException>(Assert.IsType<AggregateException>(f.Exception).InnerException);
            });
    }

#if FEATURE_ASYNCDISPOSABLE
    [Fact]
    public async Task ABatchThatFailsAfterShutdownIsSignaledIsReportedOnlyAsDroppedOnDisposeAsync()
    {
        var bs = new BlockingFailingBatchedSink();
        var recorder = new RecordingFailureListener();
        var pbs = new BatchingSink(bs, new() { BatchSizeLimit = 3, BufferingTimeLimit = TimeSpan.FromHours(1), RetryTimeLimit = TimeSpan.FromDays(1), EagerlyEmitFirstEvent = false });
        pbs.SetFailureListener(recorder);

        var events = Enumerable.Range(0, 3).Select(_ => Some.InformationEvent()).ToList();
        foreach (var evt in events)
            pbs.Emit(evt);

        Assert.True(bs.AttemptStarted.Wait(TimeSpan.FromSeconds(30)));

        // Shutdown is signaled synchronously, before the pending attempt is allowed to fail.
        var disposal = pbs.DisposeAsync();
        bs.FailAttempt();
        await disposal;

        Assert.Equal(1, bs.Attempts);
        var failure = Assert.Single(recorder.Failures);
        Assert.Equal(LoggingFailureKind.Permanent, failure.Kind);
        Assert.Equal(events, failure.Events);
        Assert.IsType<InvalidOperationException>(failure.Exception);
    }

    class BlockingFailingBatchedSink : IBatchedLogEventSink
    {
        readonly TaskCompletionSource<bool> _fail = new(TaskCreationOptions.RunContinuationsAsynchronously);
        int _attempts;

        public int Attempts => Volatile.Read(ref _attempts);

        public ManualResetEventSlim AttemptStarted { get; } = new();

        public void FailAttempt() => _fail.TrySetResult(true);

        public async Task EmitBatchAsync(IReadOnlyCollection<LogEvent> batch)
        {
            Interlocked.Increment(ref _attempts);
            AttemptStarted.Set();
            await _fail.Task;
            throw new InvalidOperationException("sink down");
        }

        public Task OnEmptyBatchAsync() => Task.CompletedTask;
    }
#endif

    // Records a copy of each attempted batch; attempts are numbered from 1, and fail unless `shouldFail` says otherwise.
    class ScriptedBatchedSink(Func<int, Task>? beforeAttempt = null, Func<int, bool>? shouldFail = null) : IBatchedLogEventSink
    {
        readonly List<List<LogEvent>> _attempts = [];

        public List<List<LogEvent>> Attempts
        {
            get
            {
                lock (_attempts)
                    return [.._attempts];
            }
        }

        public async Task EmitBatchAsync(IReadOnlyCollection<LogEvent> batch)
        {
            int attempt;
            lock (_attempts)
            {
                _attempts.Add(batch.ToList());
                attempt = _attempts.Count;
            }

            if (beforeAttempt != null)
                await beforeAttempt(attempt);

            if (shouldFail?.Invoke(attempt) ?? true)
                throw new InvalidOperationException("sink down");
        }

        public Task OnEmptyBatchAsync() => Task.CompletedTask;
    }

    // Records failures, copying the reported events because the sink reuses its batch collection, then forwards
    // them to `Next`.
    class RecordingFailureListener : ILoggingFailureListener
    {
        readonly List<(LoggingFailureKind Kind, List<LogEvent> Events, Exception? Exception)> _failures = [];

        public ILoggingFailureListener? Next { get; set; }

        // Released once for each `Temporary` failure report.
        public SemaphoreSlim TemporaryReports { get; } = new(0);

        public bool ThrowOnPermanent { get; set; }

        public List<(LoggingFailureKind Kind, List<LogEvent> Events, Exception? Exception)> Failures
        {
            get
            {
                lock (_failures)
                    return [.._failures];
            }
        }

        public void OnLoggingFailed(object sender, LoggingFailureKind kind, string message, IReadOnlyCollection<LogEvent>? events, Exception? exception)
        {
            lock (_failures)
                _failures.Add((kind, events?.ToList() ?? [], exception));

            Next?.OnLoggingFailed(sender, kind, message, events, exception);

            if (kind == LoggingFailureKind.Temporary)
                TemporaryReports.Release();

            if (kind == LoggingFailureKind.Permanent && ThrowOnPermanent)
                throw new NotSupportedException("listener down");
        }
    }

    // Lets the test observe failures that the fallback chain's listener receives from the wrapped sink.
    sealed class ListenerInterceptingSink(BatchingSink inner, RecordingFailureListener recorder)
        : ILogEventSink, ISetLoggingFailureListener, IDisposable
    {
        public void Emit(LogEvent logEvent) => inner.Emit(logEvent);

        public void SetFailureListener(ILoggingFailureListener failureListener)
        {
            recorder.Next = failureListener;
            inner.SetFailureListener(recorder);
        }

        public void Dispose() => inner.Dispose();
    }
}
