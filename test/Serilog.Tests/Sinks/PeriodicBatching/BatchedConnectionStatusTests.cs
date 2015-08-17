using System;
using Xunit;
using Serilog.Sinks.PeriodicBatching;

namespace Serilog.Tests.Sinks.PeriodicBatching
{
    public class BatchedConnectionStatusTests
    {
        readonly TimeSpan DefaultPeriod = TimeSpan.FromSeconds(2);

        [Test]
        public void WhenNoFailuresHaveOccurredTheRegularIntervalIsUsed()
        {
            var bcs = new BatchedConnectionStatus(DefaultPeriod);
            Assert.AreEqual(DefaultPeriod, bcs.NextInterval);
        }

        [Test]
        public void WhenOneFailureHasOccurredTheRegularIntervalIsUsed()
        {
            var bcs = new BatchedConnectionStatus(DefaultPeriod);
            bcs.MarkFailure();
            Assert.AreEqual(DefaultPeriod, bcs.NextInterval);
        }

        [Test]
        public void WhenTwoFailuresHaveOccurredTheIntervalBacksOff()
        {
            var bcs = new BatchedConnectionStatus(DefaultPeriod);
            bcs.MarkFailure();
            bcs.MarkFailure();
            Assert.AreEqual(TimeSpan.FromSeconds(10), bcs.NextInterval);
        }

        [Test]
        public void WhenABatchSucceedsTheStatusResets()
        {
            var bcs = new BatchedConnectionStatus(DefaultPeriod);
            bcs.MarkFailure();
            bcs.MarkFailure();
            bcs.MarkSuccess();
            Assert.AreEqual(DefaultPeriod, bcs.NextInterval);
        }

        [Test]
        public void WhenThreeFailuresHaveOccurredTheIntervalBacksOff()
        {
            var bcs = new BatchedConnectionStatus(DefaultPeriod);
            bcs.MarkFailure();
            bcs.MarkFailure();
            bcs.MarkFailure();
            Assert.AreEqual(TimeSpan.FromSeconds(20), bcs.NextInterval);
            Assert.IsFalse(bcs.ShouldDropBatch);
        }

        [Test]
        public void WhenFourFailuresHaveOccurredTheIntervalBacksOffAndBatchIsDropped()
        {
            var bcs = new BatchedConnectionStatus(DefaultPeriod);
            bcs.MarkFailure();
            bcs.MarkFailure();
            bcs.MarkFailure();
            bcs.MarkFailure();
            Assert.AreEqual(TimeSpan.FromSeconds(40), bcs.NextInterval);
            Assert.That(bcs.ShouldDropBatch);
            Assert.IsFalse(bcs.ShouldDropQueue);
        }

        [Test]
        public void WhenSixFailuresHaveOccurredTheQueueIsDropped()
        {
            var bcs = new BatchedConnectionStatus(DefaultPeriod);
            for (var i = 0; i < 6; ++i )
                bcs.MarkFailure();
            Assert.That(bcs.ShouldDropQueue);
        }
    }
}
