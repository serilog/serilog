#if !DNXCORE50
using System;
using System.Configuration;
using Xunit;
using Serilog.Events;
using Serilog.Tests.Support;

namespace Serilog.Extras.AppSettings.Tests
{
    public class AppSettingsTests
    {
        [Fact]
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

        [Fact]
        public void CanUseCustomPrefixToConfigureSettings()
        {
            const string prefix1 = "custom1";
            const string prefix2 = "custom2";

            // Make sure we have the expected keys in the App.config
            Assert.Equal("Warning", ConfigurationManager.AppSettings[prefix1 + ":serilog:minimum-level"]);
            Assert.Equal("Error", ConfigurationManager.AppSettings[prefix2 + ":serilog:minimum-level"]);

            var log1 = new LoggerConfiguration()
                .ReadFrom.AppSettings(prefix1)
                .CreateLogger();

            var log2 = new LoggerConfiguration()
                .ReadFrom.AppSettings(prefix2)
                .CreateLogger();

            Assert.False(log1.IsEnabled(LogEventLevel.Information));
            Assert.True(log1.IsEnabled(LogEventLevel.Warning));

            Assert.False(log2.IsEnabled(LogEventLevel.Warning));
            Assert.True(log2.IsEnabled(LogEventLevel.Error));
        }

        [Fact]
        public void CustomPrefixCannotContainColon()
        {
            Assert.Throws<ArgumentException>(() =>
                new LoggerConfiguration().ReadFrom.AppSettings("custom1:custom2"));
        }

        [Fact]
        public void CustomPrefixCannotBeSerilog()
        {
            Assert.Throws<ArgumentException>(() =>
                new LoggerConfiguration().ReadFrom.AppSettings("serilog"));
        }
    }
}
#endif
