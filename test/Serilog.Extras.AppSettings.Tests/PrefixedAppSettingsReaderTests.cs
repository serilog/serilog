using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
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

        [Test]
        public void EnvironmentVariableExpansionIsApplied()
        {
            var configuration = new LoggerConfiguration();
            var settings = new NameValueCollection
            {
                { "serilog:enrich:with-property:Path", "%PATH%" }
            };

            PrefixedAppSettingsReader.ConfigureLogger(configuration, settings);

            LogEvent evt = null;
            var log = configuration.WriteTo.Sink(new DelegatingSink(e => evt = e)).CreateLogger();

            log.Information("Has a Path property with value expanded from the environment variable");

            Assert.IsNotNull(evt);
            Assert.AreNotEqual("%PATH%", evt.Properties["Path"].LiteralValue());
        }

		[Test]
	    public void SingleUriConvertsToUri()
	    {
			var result = (Uri)PrefixedAppSettingsReader.ConvertToType("http://localhost:9200/", typeof(Uri));
			Assert.AreEqual(result.AbsoluteUri, "http://localhost:9200/");
	    }

		[Test]
		public void MultipleUriConvertsToEnumerableUri()
		{
			var resultEnumerable = (IEnumerable<Uri>)PrefixedAppSettingsReader.ConvertToType("http://localhost:9200/ http://localhost:9201/", typeof(IEnumerable<Uri>));
			
			Assert.IsNotNull(resultEnumerable);
			Assert.IsInstanceOf(typeof(IEnumerable<Uri>), resultEnumerable);
			
			var result = resultEnumerable as IList<Uri> ?? resultEnumerable.ToList();
			
			Assert.AreEqual(result.Count(), 2);

			Assert.IsInstanceOf(typeof(Uri), result[0]);
			Assert.AreEqual(result[0].AbsoluteUri, "http://localhost:9200/");
			
			Assert.IsInstanceOf(typeof(Uri), result[1]);
			Assert.AreEqual(result[1].AbsoluteUri, "http://localhost:9201/");
		}
	}
}
