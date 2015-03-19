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

        [Test]
        public void UppercaseFormatSpecifierIsSupportedForStrings()
        {
            var formatter = new MessageTemplateTextFormatter("{Name:u}", CultureInfo.InvariantCulture);
            var evt = DelegatingSink.GetLogEvent(l => l.Information("{Name}", "Nick"));
            var sw = new StringWriter();
            formatter.Format(evt, sw);
            Assert.AreEqual("NICK", sw.ToString());
        }

        [Test]
        public void LowercaseFormatSpecifierIsSupportedForStrings()
        {
            var formatter = new MessageTemplateTextFormatter("{Name:w}", CultureInfo.InvariantCulture);
            var evt = DelegatingSink.GetLogEvent(l => l.Information("{Name}", "Nick"));
            var sw = new StringWriter();
            formatter.Format(evt, sw);
            Assert.AreEqual("nick", sw.ToString());
        }

        [Test]
        public void UppercaseFormatSpecifierIsSupportedForLevelUppercased()
        {
            var formatter = new MessageTemplateTextFormatter("{Level:u} {Message}", CultureInfo.InvariantCulture);
            var evt = DelegatingSink.GetLogEvent(l => l.Information("Nick"));
            var sw = new StringWriter();
            formatter.Format(evt, sw);
            Assert.AreEqual("INFORMATION Nick", sw.ToString());
        }

        [Test]
        public void UppercaseFormatSpecifierIsSupportedForLevelLowercased()
        {
            var formatter = new MessageTemplateTextFormatter("{Level:w} {Message}", CultureInfo.InvariantCulture);
            var evt = DelegatingSink.GetLogEvent(l => l.Information("Nick"));
            var sw = new StringWriter();
            formatter.Format(evt, sw);
            Assert.AreEqual("information Nick", sw.ToString());
        }

        [Test]
        public void UppercaseFormatSpecifierIsSupportedForLevelNoCase()
        {
            var formatter = new MessageTemplateTextFormatter("{Level} {Message}", CultureInfo.InvariantCulture);
            var evt = DelegatingSink.GetLogEvent(l => l.Information("Nick"));
            var sw = new StringWriter();
            formatter.Format(evt, sw);
            Assert.AreEqual("Information Nick", sw.ToString());
        }
    }
}
