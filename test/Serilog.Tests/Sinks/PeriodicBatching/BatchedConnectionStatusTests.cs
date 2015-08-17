#if !DNXCORE50
using System;
using Xunit;
using Serilog.Sinks.PeriodicBatching;

namespace Serilog.Tests.Sinks.PeriodicBatching
{
    public class BatchedConnectionStatusTests
    {
        readonly TimeSpan DefaultPeriod = TimeSpan.FromSeconds(2);

        [Fact]
        public void WhenNoFailuresHaveOccurredTheRegularIntervalIsUsed()
        {
            var bcs = new BatchedConnectionStatus(DefaultPeriod);
            Assert.Equal(DefaultPeriod, bcs.NextInterval);
        }

        [Fact]
        public void WhenOneFailureHasOccurredTheRegularIntervalIsUsed()
        {
            var bcs = new BatchedConnectionStatus(DefaultPeriod);
            bcs.MarkFailure();
            Assert.Equal(DefaultPeriod, bcs.NextInterval);
        }

        [Fact]
        public void WhenTwoFailuresHaveOccurredTheIntervalBacksOff()
        {
            var bcs = new BatchedConnectionStatus(DefaultPeriod);
            bcs.MarkFailure();
            bcs.MarkFailure();
            Assert.Equal(TimeSpan.FromSeconds(10), bcs.NextInterval);
        }

        [Fact]
        public void WhenABatchSucceedsTheStatusResets()
        {
            var bcs = new BatchedConnectionStatus(DefaultPeriod);
            bcs.MarkFailure();
            bcs.MarkFailure();
            bcs.MarkSuccess();
            Assert.Equal(DefaultPeriod, bcs.NextInterval);
        }

        [Fact]
        public void WhenThreeFailuresHaveOccurredTheIntervalBacksOff()
        {
            var bcs = new BatchedConnectionStatus(DefaultPeriod);
            bcs.MarkFailure();
            bcs.MarkFailure();
            bcs.MarkFailure();
            Assert.Equal(TimeSpan.FromSeconds(20), bcs.NextInterval);
            Assert.False(bcs.ShouldDropBatch);
        }

        [Fact]
        public void WhenFourFailuresHaveOccurredTheIntervalBacksOffAndBatchIsDropped()
        {
            var bcs = new BatchedConnectionStatus(DefaultPeriod);
            bcs.MarkFailure();
            bcs.MarkFailure();
            bcs.MarkFailure();
            bcs.MarkFailure();
            Assert.Equal(TimeSpan.FromSeconds(40), bcs.NextInterval);
            Assert.True(bcs.ShouldDropBatch);
            Assert.False(bcs.ShouldDropQueue);
        }

        [Fact]
        public void WhenSixFailuresHaveOccurredTheQueueIsDropped()
        {
            var bcs = new BatchedConnectionStatus(DefaultPeriod);
            for (var i = 0; i < 6; ++i )
                bcs.MarkFailure();
            Assert.True(bcs.ShouldDropQueue);
        }
    }
}
#endif
