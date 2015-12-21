#if APPSETTINGS

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;
using Serilog.Events;
using Serilog.Settings.KeyValuePairs;
using Serilog.Sinks.RollingFile;
using Serilog.Tests.Support;

namespace Serilog.Tests.AppSettings.Tests
{
    public class KeyValuePairSettingsTests
    {
        [Fact]
        public void ConvertibleValuesConvertToTIfTargetIsNullable()
        {
            var result = (int?)KeyValuePairSettings.ConvertToType("3", typeof(int?));
            Assert.True(result == 3);
        }

        [Fact]
        public void NullValuesConvertToNullIfTargetIsNullable()
        {
            var result = (int?)KeyValuePairSettings.ConvertToType(null, typeof(int?));
            Assert.True(result == null);
        }

        [Fact]
        public void EmptyStringValuesConvertToNullIfTargetIsNullable()
        {
            var result = (int?)KeyValuePairSettings.ConvertToType("", typeof(int?));
            Assert.True(result == null);
        }

        [Fact]
        public void ValuesConvertToNullableTimeSpan()
        {
            var result = (System.TimeSpan?)KeyValuePairSettings.ConvertToType("00:01:00", typeof(System.TimeSpan?));
            Assert.Equal(System.TimeSpan.FromMinutes(1), result);
        }

        [Fact]
        public void ValuesConvertToEnumMembers()
        {
            var result = (LogEventLevel)KeyValuePairSettings.ConvertToType("Information", typeof(LogEventLevel));
            Assert.Equal(LogEventLevel.Information, result);
        }

        [Fact]
        public void FindsConfigurationAssemblies()
        {
            var configurationAssemblies = KeyValuePairSettings.LoadConfigurationAssemblies(new Dictionary<string, string>()).ToList();

            // The core Serilog assembly is always considered
            Assert.Equal(1, configurationAssemblies.Count);
        }

        [Fact]
        public void FindsConfigurationMethodsWithinAnAssembly()
        {
            var configurationMethods = KeyValuePairSettings
                .FindSinkConfigurationMethods(new[] { typeof(RollingFileSink)
#if DNXCORE50
                    .GetTypeInfo()
#endif
                    .Assembly
                    })
                .Select(m => m.Name)
                .Distinct()
                .ToList();

            Assert.Equal(6, configurationMethods.Count);

            Assert.True(configurationMethods.Contains("ColoredConsole"));
            Assert.True(configurationMethods.Contains("Console"));
            Assert.True(configurationMethods.Contains("DumpFile"));
            Assert.True(configurationMethods.Contains("File"));
            Assert.True(configurationMethods.Contains("RollingFile"));
            Assert.True(configurationMethods.Contains("Trace"));
        }

        [Fact]
        public void PropertyEnrichmentIsApplied()
        {
            LogEvent evt = null;
            var log = new LoggerConfiguration()
                .ReadFrom.KeyValuePairs(new Dictionary<string, string>
                {
                    {"enrich:with-property:App", "Test"}
                })
                .WriteTo.Sink(new DelegatingSink(e => evt = e))
                .CreateLogger();

            log.Information("Has a test property");

            Assert.NotNull(evt);
            Assert.Equal("Test", evt.Properties["App"].LiteralValue());
        }
    }
}
#endif
