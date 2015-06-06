using System.Configuration;
using NUnit.Framework;
using Serilog.Events;
using Serilog.Tests.Support;

namespace Serilog.Extras.AppSettings.Tests
{
    [TestFixture]
    public class AppSettingsTests
    {
        [Test]
        public void EnvironmentVariableExpansionIsApplied()
        {
            // Make sure we have the expected key in the App.config
            Assert.AreEqual("%PATH%", ConfigurationManager.AppSettings["serilog:enrich:with-property:Path"]);

            LogEvent evt = null;
            var log = new LoggerConfiguration()
                .ReadFrom.AppSettings() 
                .WriteTo.Sink(new DelegatingSink(e => evt = e))
                .CreateLogger();

            log.Information("Has a Path property with value expanded from the environment variable");

            Assert.IsNotNull(evt);
            Assert.IsNotNullOrEmpty((string)evt.Properties["Path"].LiteralValue());
            Assert.AreNotEqual("%PATH%", evt.Properties["Path"].LiteralValue());
        }
    }
}
