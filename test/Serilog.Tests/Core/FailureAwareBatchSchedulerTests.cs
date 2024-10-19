using Serilog.Core.Sinks.Batching;

namespace Serilog.Core.Tests;

public class FailureAwareBatchSchedulerTests
{
    TimeSpan _bufferingTimeLimit = TimeSpan.FromSeconds(2), _retryTimeLimit = TimeSpan.FromMinutes(10);
    TestTimeProvider _timeProvider = new();
    FailureAwareBatchScheduler _scheduler { get; }

    public FailureAwareBatchSchedulerTests()
    {
        _scheduler = new(_bufferingTimeLimit, _retryTimeLimit, _timeProvider);
    }

    [Fact]
    public void WhenNoFailuresHaveOccurredTheInitialIntervalIsUsed()
    {
        Assert.Equal(_bufferingTimeLimit, _scheduler.NextInterval);
    }

    [Fact]
    public void WhenOneFailureHasOccurredTheInitialIntervalIsUsed()
    {
        _scheduler.MarkFailure(out _, out _);
        Assert.Equal(_bufferingTimeLimit, _scheduler.NextInterval);
    }

    [Fact]
    public void WhenTwoFailuresHaveOccurredTheIntervalBacksOff()
    {
        _scheduler.MarkFailure(out _, out _);
        _scheduler.MarkFailure(out _, out _);
        Assert.Equal(TimeSpan.FromSeconds(10), _scheduler.NextInterval);
    }

    [Fact]
    public void WhenABatchSucceedsTheStatusResets()
    {
        _scheduler.MarkFailure(out _, out _);
        _scheduler.MarkFailure(out _, out _);
        _scheduler.MarkSuccess();
        Assert.Equal(_bufferingTimeLimit, _scheduler.NextInterval);
    }

    [Fact]
    public void WhenThreeFailuresHaveOccurredTheIntervalBacksOff()
    {
        _scheduler.MarkFailure(out _, out _);
        _scheduler.MarkFailure(out _, out _);
        _scheduler.MarkFailure(out var shouldDropBatch, out _);
        Assert.Equal(TimeSpan.FromSeconds(20), _scheduler.NextInterval);
        Assert.False(shouldDropBatch);
    }

    [Fact]
    public void WhenRetryTimeLimitHasElapsedTheBatchIsDropped()
    {
        for (var i = 0; i < 8; ++i)
        {
            _scheduler.MarkFailure(out var shouldDropBatch, out var shouldDropQueue);
            Assert.False(shouldDropBatch);
            Assert.False(shouldDropQueue);
        }

        _timeProvider.Advance(_retryTimeLimit);
        _scheduler.MarkFailure(out var shouldDropBatch_, out var shouldDropQueue_);

        Assert.True(shouldDropBatch_);
        Assert.False(shouldDropQueue_);
    }

    [Fact]
    public void WhenTenConsecutiveBatchesAreDroppedTheQueueIsDropped()
    {
        _scheduler.MarkFailure(out var shouldDropBatch, out var shouldDropQueue);
        _timeProvider.Advance(_retryTimeLimit);

        for (var i = 0; i < 9; ++i)
        {
            _scheduler.MarkFailure(out shouldDropBatch, out shouldDropQueue);
            Assert.True(shouldDropBatch);
            Assert.False(shouldDropQueue);
        }

        _scheduler.MarkFailure(out shouldDropBatch, out shouldDropQueue);

        Assert.True(shouldDropBatch);
        Assert.True(shouldDropQueue);
    }
}
