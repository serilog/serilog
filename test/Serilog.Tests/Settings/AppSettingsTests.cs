using System.Configuration;
using Xunit;
using Serilog.Events;
using Serilog.Tests.Support;

namespace Serilog.Extras.AppSettings.Tests
{
    public class AppSettingsTests
    {
        [Fact(Skip = "Fails on DNX451")]
        public void EnvironmentVariableExpansionIsApplied()
        {
            // Make sure we have the expected key in the App.config
            Assert.Equal("%PATH%", ConfigurationManager.AppSettings["serilog:enrich:with-property:Path"]);

            LogEvent evt = null;
            var log = new LoggerConfiguration()
                .ReadFrom.AppSettings() 
                .WriteTo.Sink(new DelegatingSink(e => evt = e))
                .CreateLogger();

            log.Information("Has a Path property with value expanded from the environment variable");

            Assert.NotNull(evt);
            Assert.NotEmpty((string)evt.Properties["Path"].LiteralValue());
            Assert.NotEqual("%PATH%", evt.Properties["Path"].LiteralValue());
        }
    }
}
