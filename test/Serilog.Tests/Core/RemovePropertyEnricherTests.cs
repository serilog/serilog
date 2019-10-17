using Serilog.Core.Enrichers;
using Serilog.Tests.Support;
using Xunit;

namespace Serilog.Tests.Core
{
    public class RemovePropertyEnricherTests
    {
        [Fact]
        public void TheEnricherHasNoEffectIfThePropertyIsMissing()
        {
            var logEvent = Some.LogEvent(l => l.ForContext("A", 1).Information("Hello"));
            var enricher = new RemovePropertyEnricher("B", false);

            enricher.Enrich(logEvent, Some.LogEventPropertyFactory());

            Assert.True(logEvent.Properties.ContainsKey("A"));
            Assert.False(logEvent.Properties.ContainsKey("B"));
        }

        [Fact]
        public void TheEnricherRemovesThePropertyIfPresent()
        {
            var logEvent = Some.LogEvent(l => l.ForContext("A", 1).Information("Hello"));
            var enricher = new RemovePropertyEnricher("A", false);

            enricher.Enrich(logEvent, Some.LogEventPropertyFactory());

            Assert.False(logEvent.Properties.ContainsKey("A"));
        }

        [Fact]
        public void TheEnricherRemovesThePropertyEvenIfIncludedInTheMessage()
        {
            var logEvent = Some.LogEvent(l => l.Information("Hello, {A}", 1));
            var enricher = new RemovePropertyEnricher("A", false);

            enricher.Enrich(logEvent, Some.LogEventPropertyFactory());

            Assert.False(logEvent.Properties.ContainsKey("A"));
        }

        [Fact]
        public void TheEnricherPreservesThePropertyIfIncludedInTheMessageAndInstructedToRetain()
        {
            var logEvent = Some.LogEvent(l => l.Information("Hello, {A}", 1));
            var enricher = new RemovePropertyEnricher("A", true);

            enricher.Enrich(logEvent, Some.LogEventPropertyFactory());

            Assert.True(logEvent.Properties.ContainsKey("A"));
        }
    }
}
