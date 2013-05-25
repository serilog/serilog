using System;
using System.Globalization;
using System.IO;
using NUnit.Framework;
using Serilog.Tests.Support;

namespace Serilog.Tests.Sinks.TextWriter
{
    [TestFixture]
    public class TextWriterSinkTests
    {
        [Test]
        public void EventsAreWrittenToTheTextWriter()
        {
            var sw = new StringWriter();

            var log = new LoggerConfiguration()
                .WriteTo.TextWriter(sw)
                .CreateLogger();

            var mt = Some.String();
            log.Information(mt);

            var s = sw.ToString();
            Assert.That(s.Contains(mt));
        }

        [Test]
        public void EventsAreWrittenToTheTextWriterUsingFormatProvider()
        {
            var sw = new StringWriter();

            var french = CultureInfo.GetCultureInfo("fr-FR");
            var log = new LoggerConfiguration()
                .WriteTo.TextWriter(sw, formatProvider: french)
                .CreateLogger();

            var mt = String.Format(french, "{0}", 12.345);
            log.Information("{0}", 12.345);

            var s = sw.ToString();
            Assert.That(s.Contains(mt));
        }
    }
}
