using Serilog.Core.Sinks.Batching;

namespace Serilog.Core.Tests;

public class BatchingSinkTests
{
    static readonly TimeSpan TinyWait = TimeSpan.FromMilliseconds(200);
    static readonly TimeSpan MicroWait = TimeSpan.FromMilliseconds(1);

    [Fact]
    public void WhenAnEventIsEnqueuedItIsWrittenToABatchOnDispose()
    {
        var bs = new InMemoryBatchedSink(TimeSpan.Zero);
        var pbs = new BatchingSink(bs, new() { BatchSizeLimit = 2, BufferingTimeLimit = TinyWait, EagerlyEmitFirstEvent = true });
        var evt = Some.InformationEvent();
        pbs.Emit(evt);
        pbs.Dispose();
        Assert.Single(bs.Batches);
        Assert.Single(bs.Batches[0]);
        Assert.Same(evt, bs.Batches[0][0]);
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
            Assert.Single(bs.Batches);
            Assert.Single(bs.Batches[0]);
            Assert.Same(evt, bs.Batches[0][0]);
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
}
