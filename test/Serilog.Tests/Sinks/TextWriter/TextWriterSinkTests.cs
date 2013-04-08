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
    }
}
