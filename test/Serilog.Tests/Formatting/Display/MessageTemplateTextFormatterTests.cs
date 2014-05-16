using System.Globalization;
using System.IO;
using NUnit.Framework;
using Serilog.Tests.Support;
using Serilog.Formatting.Display;

namespace Serilog.Tests.Formatting.Display
{
    [TestFixture]
    public class MessageTemplateTextFormatterTests
    {
        [Test]
        public void UsesFormatProvider()
        {
            var french = CultureInfo.GetCultureInfo("fr-FR");
            var formatter = new MessageTemplateTextFormatter("{Message}", french);
            var evt = DelegatingSink.GetLogEvent(l => l.Information("{0}", 12.345));
            var sw = new StringWriter();
            formatter.Format(evt, sw);
            Assert.AreEqual("12,345", sw.ToString());
        }

        [Test]
        public void MessageTemplatesContainingFormatStringPropertiesRenderCorrectly()
        {
            var formatter = new MessageTemplateTextFormatter("{Message}", CultureInfo.InvariantCulture);
            var evt = DelegatingSink.GetLogEvent(l => l.Information("{Message}", "Hello, world!"));
            var sw = new StringWriter();
            formatter.Format(evt, sw);
            Assert.AreEqual("\"Hello, world!\"", sw.ToString());
        }
    }
}
