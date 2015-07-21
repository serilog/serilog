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
        public void ChangesToSettingsReadFromAppConfigFileAreApplied()
        {
            const string appTitleKey = "serilog:enrich:with-property:AppTitle";
            const string appTitleValue = "Serilog.Tests";

            const string appDescriptionKey = "serilog:enrich:with-property:AppDescription";
            const string appDescriptionValue = "Tests for Serilog";

            const string appCopyrightKey = "serilog:enrich:with-property:AppCopyright";
            const string appCopyrightValue = "Serilog Contributors 2015";

            // Make sure we have the expected keys in the App.config
            Assert.AreEqual(appTitleValue, ConfigurationManager.AppSettings[appTitleKey]);
            Assert.AreEqual(appDescriptionValue, ConfigurationManager.AppSettings[appDescriptionKey]);
            Assert.IsNull(ConfigurationManager.AppSettings[appCopyrightKey]);

            LogEvent evt = null;
            var log = new LoggerConfiguration()
                .ReadFrom.AppSettings(settings =>
                {
                    settings.Remove(appDescriptionKey);
                    settings.Add(appCopyrightKey, appCopyrightValue);
                })
                .WriteTo.Sink(new DelegatingSink(e => evt = e))
                .CreateLogger();

            log.Information("Has AppTitle and AppCopyright property, but no AppDescription");

            Assert.IsNotNull(evt);
            Assert.AreEqual(appTitleValue, evt.Properties["AppTitle"].LiteralValue());
            Assert.AreEqual(appCopyrightValue, evt.Properties["AppCopyright"].LiteralValue());
            Assert.IsFalse(evt.Properties.ContainsKey("AppDescription"));
        }
    }
}
