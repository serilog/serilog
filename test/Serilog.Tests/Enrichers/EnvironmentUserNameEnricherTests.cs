using NUnit.Framework;
using Serilog.Events;
using Serilog.Tests.Support;

namespace Serilog.Tests.Enrichers
{
    [TestFixture]
    public class EnvironmentUserNameEnricherTests
    {
        [Test]
        public void EnvironmentUserNameEnricherIsApplied()
        {
            LogEvent evt = null;
            var log = new LoggerConfiguration()
                .Enrich.WithEnvironmentUserName()
                .WriteTo.Sink(new DelegatingSink(e => evt = e))
                .CreateLogger();

            log.Information(@"Has an EnvironmentUserName property with [domain\]userName");

            Assert.IsNotNull(evt);
            Assert.IsNotNullOrEmpty((string)evt.Properties["EnvironmentUserName"].LiteralValue());
        }
    }
}
