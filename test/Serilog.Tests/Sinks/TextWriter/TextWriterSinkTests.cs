using System;
using System.Globalization;
using System.IO;
using Xunit;
using Serilog.Tests.Support;

namespace Serilog.Tests.Sinks.TextWriter
{
    public class TextWriterSinkTests
    {
        [Fact]
        public void EventsAreWrittenToTheTextWriter()
        {
            var sw = new StringWriter();

            var log = new LoggerConfiguration()
                .WriteTo.TextWriter(sw)
                .CreateLogger();

            var mt = Some.String();
            log.Information(mt);

            var s = sw.ToString();
            Assert.True(s.Contains(mt));
        }

        [Fact]
        public void EventsAreWrittenToTheTextWriterUsingFormatProvider()
        {
            var sw = new StringWriter();

            var french = new CultureInfo("fr-FR");
            var log = new LoggerConfiguration()
                .WriteTo.TextWriter(sw, formatProvider: french)
                .CreateLogger();

            var mt = String.Format(french, "{0}", 12.345);
            log.Information("{0}", 12.345);

            var s = sw.ToString();
            Assert.True(s.Contains(mt));
        }
    }
}
