using System.Collections.Generic;
using System.Linq;
using Xunit;
using Serilog.Events;
using Serilog.Settings.KeyValuePairs;
using Serilog.Sinks.RollingFile;
using Serilog.Tests.Support;

namespace Serilog.Extras.AppSettings.Tests
{
    public class KeyValuePairSettingsTests
    {
        [Test]
        public void ConvertibleValuesConvertToTIfTargetIsNullable()
        {
            var result = (int?)KeyValuePairSettings.ConvertToType("3", typeof(int?));
            Assert.That(result == 3);
        }

        [Test]
        public void NullValuesConvertToNullIfTargetIsNullable()
        {
            var result = (int?)KeyValuePairSettings.ConvertToType(null, typeof(int?));
            Assert.That(result == null);
        }

        [Test]
        public void EmptyStringValuesConvertToNullIfTargetIsNullable()
        {
            var result = (int?)KeyValuePairSettings.ConvertToType("", typeof(int?));
            Assert.That(result == null);
        }

        [Test]
        public void ValuesConvertToNullableTimeSpan()
        {
            var result = (System.TimeSpan?)KeyValuePairSettings.ConvertToType("00:01:00", typeof(System.TimeSpan?));
            Assert.AreEqual(System.TimeSpan.FromMinutes(1), result);
        }

        [Test]
        public void ValuesConvertToEnumMembers()
        {
            var result = (LogEventLevel)KeyValuePairSettings.ConvertToType("Information", typeof(LogEventLevel));
            Assert.AreEqual(LogEventLevel.Information, result);
        }

        [Test]
        public void FindsConfigurationAssemblies()
        {
            var configurationAssemblies = KeyValuePairSettings.LoadConfigurationAssemblies(new Dictionary<string, string>
            {
                {"using:FullNetFx", "Serilog.FullNetFx"}
            }).ToList();

            // The core Serilog assembly is always considered
            Assert.AreEqual(2, configurationAssemblies.Count);
        }

        [Test]
        public void FindsConfigurationMethodsWithinAnAssembly()
        {
            var configurationMethods = KeyValuePairSettings
                .FindSinkConfigurationMethods(new[] { typeof(RollingFileSink).Assembly })
                .Select(m => m.Name)
                .Distinct()
                .ToList();

            Assert.AreEqual(6, configurationMethods.Count);

            Assert.That(configurationMethods.Contains("ColoredConsole"));
            Assert.That(configurationMethods.Contains("Console"));
            Assert.That(configurationMethods.Contains("DumpFile"));
            Assert.That(configurationMethods.Contains("File"));
            Assert.That(configurationMethods.Contains("RollingFile"));
            Assert.That(configurationMethods.Contains("Trace"));
        }

        [Test]
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

            Assert.IsNotNull(evt);
            Assert.AreEqual("Test", evt.Properties["App"].LiteralValue());
        }
    }
}
