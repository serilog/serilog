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

        [Test]
        public void CanUseCustomPrefixToConfigureSettings()
        {
            const string prefix1 = "custom1";
            const string prefix2 = "custom2";

            // Make sure we have the expected keys in the App.config
            Assert.AreEqual("Warning", ConfigurationManager.AppSettings[prefix1 + ":serilog:minimum-level"]);
            Assert.AreEqual("Error", ConfigurationManager.AppSettings[prefix2 + ":serilog:minimum-level"]);

            var log1 = new LoggerConfiguration()
                .WriteTo.Observers(o => { })
                .ReadFrom.AppSettings(prefix1)
                .CreateLogger();

            var log2 = new LoggerConfiguration()
                .WriteTo.Observers(o => { })
                .ReadFrom.AppSettings(prefix2)
                .CreateLogger();

            Assert.IsFalse(log1.IsEnabled(LogEventLevel.Information));
            Assert.IsTrue(log1.IsEnabled(LogEventLevel.Warning));

            Assert.IsFalse(log2.IsEnabled(LogEventLevel.Warning));
            Assert.IsTrue(log2.IsEnabled(LogEventLevel.Error));
        }

        [Test]
        public void CanUseCustomSettingDelimiterToConfigureSettings()
        {
            const string prefix1 = "|";
            const string prefix2 = "!";

            // Make sure we have the expected keys in the App.config
            Assert.AreEqual("Warning", ConfigurationManager.AppSettings["serilog|minimum-level"]);
            Assert.AreEqual("Error", ConfigurationManager.AppSettings["serilog!minimum-level"]);

            var log1 = new LoggerConfiguration()
           .WriteTo.Observers(o => { })
           .ReadFrom.AppSettings(null,prefix1)
           .CreateLogger();

            var log2 = new LoggerConfiguration()
                .WriteTo.Observers(o => { })
                .ReadFrom.AppSettings(null,prefix2)
                .CreateLogger();

            Assert.IsFalse(log1.IsEnabled(LogEventLevel.Information));
            Assert.IsTrue(log1.IsEnabled(LogEventLevel.Warning));

            Assert.IsFalse(log2.IsEnabled(LogEventLevel.Warning));
            Assert.IsTrue(log2.IsEnabled(LogEventLevel.Error));
        }
    }
}
