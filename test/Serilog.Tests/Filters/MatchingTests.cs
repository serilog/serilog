using NUnit.Framework;
using Serilog.Filters;
using Serilog.Tests.Support;

namespace Serilog.Tests.Filters
{
    [TestFixture]
    public class MatchingTests
    {
        [Test]
        public void EventsCanBeExcludedBySource()
        {
            var written = false;

            var log = new LoggerConfiguration()
                .Filter.ByExcluding(Matching.FromSource<MatchingTests>())
                .WriteTo.Sink(new DelegatingSink(e => written = true))
                .CreateLogger();

            var sourceContext = log.ForContext<MatchingTests>();
            sourceContext.Write(Some.InformationEvent());

            Assert.IsFalse(written);
        }

        [Test]
        public void EventsCanBeExcludedByPredicate()
        {
            var seen = 0;

            var log = new LoggerConfiguration()
                .Filter.ByExcluding(Matching.WithProperty<int>("Count", p => p < 10))
                .WriteTo.Sink(new DelegatingSink(e => seen++))
                .CreateLogger();

            log.Warning("Unrelated");
            log.Information("{Count}", 5);
            log.Information("{Count}", "wrong type");
            log.Information("{Count}", 15);

            Assert.AreEqual(3, seen);
        }
    }
}
