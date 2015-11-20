using Serilog.Events;
using Serilog.Tests.Support;
using Xunit;

namespace Serilog.Tests.Enrichers
{
    public class EnvironmentUserNameEnricherTests
    {
        [Fact]
        public void EnvironmentUserNameEnricherIsApplied()
        {
            LogEvent evt = null;
            var log = new LoggerConfiguration()
                .Enrich.WithEnvironmentUserName()
                .WriteTo.Sink(new DelegatingSink(e => evt = e))
                .CreateLogger();

            log.Information(@"Has an EnvironmentUserName property with [domain\]userName");

            Assert.NotNull(evt);
            Assert.NotEmpty((string)evt.Properties["EnvironmentUserName"].LiteralValue());
        }
    }
}
