using System.Collections.Specialized;
using NUnit.Framework;
using Serilog.Events;
using Serilog.Tests.Support;

namespace Serilog.Extras.AppSettings.Tests
{
    [TestFixture]
    public class PrefixedAppSettingsReaderTests
    {
        [Test]
        public void ConvertibleValuesConvertToTIfTargetIsNullable()
        {
            var result = (int?)PrefixedAppSettingsReader.ConvertToType("3", typeof(int?));
            Assert.That(result == 3);
        }

        [Test]
        public void NullValuesConvertToNullIfTargetIsNullable()
        {
            var result = (int?)PrefixedAppSettingsReader.ConvertToType(null, typeof(int?));
            Assert.That(result == null);
        }

        [Test]
        public void EmptyStringValuesConvertToNullIfTargetIsNullable()
        {
            var result = (int?)PrefixedAppSettingsReader.ConvertToType("", typeof(int?));
            Assert.That(result == null);
        }

        [Test]
        public void ValuesConvertToEnumMembers()
        {
            var result = (LogEventLevel)PrefixedAppSettingsReader.ConvertToType("Information", typeof(LogEventLevel));
            Assert.AreEqual(LogEventLevel.Information, result);
        }

        [Test]
        public void PropertyEnrichmentIsApplied()
        {
            var configuration = new LoggerConfiguration();
            var settings = new NameValueCollection
            {
                { "serilog:enrich:with-property:App", "Test" }
            };

            PrefixedAppSettingsReader.ConfigureLogger(configuration, settings);

            LogEvent evt = null;
            var log = configuration.WriteTo.Sink(new DelegatingSink(e => evt = e)).CreateLogger();

            log.Information("Has a test property");

            Assert.IsNotNull(evt);
            Assert.AreEqual("Test", evt.Properties["App"].LiteralValue());
        }
    }
}
