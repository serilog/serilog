using Serilog.Core.Sinks.Batching;

namespace Serilog.Core.Tests;

public class FailureAwareBatchSchedulerTests
{
    static TimeSpan Period => TimeSpan.FromSeconds(2);
    FailureAwareBatchScheduler Scheduler { get; } = new(Period);

    [Fact]
    public void WhenNoFailuresHaveOccurredTheInitialIntervalIsUsed()
    {
        Assert.Equal(Period, Scheduler.NextInterval);
    }

    [Fact]
    public void WhenOneFailureHasOccurredTheInitialIntervalIsUsed()
    {
        Scheduler.MarkFailure();
        Assert.Equal(Period, Scheduler.NextInterval);
    }

    [Fact]
    public void WhenTwoFailuresHaveOccurredTheIntervalBacksOff()
    {
        Scheduler.MarkFailure();
        Scheduler.MarkFailure();
        Assert.Equal(TimeSpan.FromSeconds(10), Scheduler.NextInterval);
    }

    [Fact]
    public void WhenABatchSucceedsTheStatusResets()
    {
        Scheduler.MarkFailure();
        Scheduler.MarkFailure();
        Scheduler.MarkSuccess();
        Assert.Equal(Period, Scheduler.NextInterval);
    }

    [Fact]
    public void WhenThreeFailuresHaveOccurredTheIntervalBacksOff()
    {
        Scheduler.MarkFailure();
        Scheduler.MarkFailure();
        Scheduler.MarkFailure();
        Assert.Equal(TimeSpan.FromSeconds(20), Scheduler.NextInterval);
        Assert.False(Scheduler.ShouldDropBatch);
    }

    [Fact]
    public void WhenEightFailuresHaveOccurredTheIntervalBacksOffAndBatchIsDropped()
    {
        for (var i = 0; i < 8; ++i)
        {
            Assert.False(Scheduler.ShouldDropBatch);
            Scheduler.MarkFailure();
        }
        Assert.Equal(TimeSpan.FromMinutes(10), Scheduler.NextInterval);
        Assert.True(Scheduler.ShouldDropBatch);
        Assert.False(Scheduler.ShouldDropQueue);
    }

    [Fact]
    public void WhenTenFailuresHaveOccurredTheQueueIsDropped()
    {
        for (var i = 0; i < 10; ++i)
        {
            Assert.False(Scheduler.ShouldDropQueue);
            Scheduler.MarkFailure();
        }
        Assert.True(Scheduler.ShouldDropQueue);
    }

    [Fact]
    public void AtTheDefaultIntervalRetriesForTenMinutesBeforeDroppingBatch()
    {
        var cumulative = TimeSpan.Zero;
        do
        {
            Scheduler.MarkFailure();

            if (!Scheduler.ShouldDropBatch)
                cumulative += Scheduler.NextInterval;
        } while (!Scheduler.ShouldDropBatch);

        Assert.False(Scheduler.ShouldDropQueue);
        Assert.Equal(TimeSpan.Parse("00:10:32", CultureInfo.InvariantCulture), cumulative);
    }

    [Fact]
    public void AtTheDefaultIntervalRetriesForThirtyMinutesBeforeDroppingQueue()
    {
        var cumulative = TimeSpan.Zero;
        do
        {
            Scheduler.MarkFailure();

            if (!Scheduler.ShouldDropQueue)
                cumulative += Scheduler.NextInterval;
        } while (!Scheduler.ShouldDropQueue);

        Assert.Equal(TimeSpan.Parse("00:30:32", CultureInfo.InvariantCulture), cumulative);
    }
}
