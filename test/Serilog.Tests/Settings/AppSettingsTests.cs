using System.Configuration;
using System.Linq;
using NUnit.Framework;
using Serilog.Events;
using Serilog.Tests.Support;

namespace Serilog.Extras.AppSettings.Tests
{
    [TestFixture]
    public class AppSettingsTests
    {
        [Test]
        public void EnvironmentVariableExpansionIsAppliedUsingAppSettings()
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

        [Test]
        public void EnvironmentVariableExpansionIsAppliedUsingKeyValuePairs()
        {
            var appSettings = ConfigurationManager.AppSettings.AllKeys
                .ToDictionary(k => k, k => ConfigurationManager.AppSettings[k]);

            // Make sure we have the expected key in the App.config
            Assert.AreEqual("%PATH%", appSettings["serilog:enrich:with-property:Path"]);

            LogEvent evt = null;
            var log = new LoggerConfiguration()
                .ReadFrom.AppSettings(appSettings)
                .WriteTo.Sink(new DelegatingSink(e => evt = e))
                .CreateLogger();

            log.Information("Has a Path property with value expanded from the environment variable");

            Assert.IsNotNull(evt);
            Assert.IsNotNullOrEmpty((string)evt.Properties["Path"].LiteralValue());
            Assert.AreNotEqual("%PATH%", evt.Properties["Path"].LiteralValue());
        }
    }
}
