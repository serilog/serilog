using System.Threading.Tasks;
using Xunit;
using Serilog.Filters;
using Serilog.Tests.Support;

namespace Serilog.Tests.Filters
{
    public class MatchingTests
    {
        [Fact]
        public async Task EventsCanBeExcludedBySource()
        {
            var written = false;

            var log = new LoggerConfiguration()
                .Filter.ByExcluding(Matching.FromSource<MatchingTests>())
                .WriteTo.Sink(new DelegatingSink(e => written = true))
                .CreateLogger();

            var sourceContext = log.ForContext<MatchingTests>();
            await sourceContext.Write(Some.InformationEvent());

            Assert.False(written);
        }

        [Fact]
        public async Task EventsCanBeExcludedByPredicate()
        {
            var seen = 0;

            var log = new LoggerConfiguration()
                .Filter.ByExcluding(Matching.WithProperty<int>("Count", p => p < 10))
                .WriteTo.Sink(new DelegatingSink(e => seen++))
                .CreateLogger();

            await log.Warning("Unrelated");
            await log.Information("{Count}", 5);
            await log.Information("{Count}", "wrong type");
            await log.Information("{Count}", 15);

            Assert.Equal(3, seen);
        }

        [Fact]
        public async Task SourceFiltersWorkOnNamespaces()
        {
            var written = false;
            var log = new LoggerConfiguration()
                .Filter.ByExcluding(Matching.FromSource("Serilog.Tests"))
                .WriteTo.Sink(new DelegatingSink(e => written = true))
                .CreateLogger()
                .ForContext<MatchingTests>();

            await log.Write(Some.InformationEvent());
            Assert.False(written);
        }
    }
}
