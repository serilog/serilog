using System.Globalization;
using System.IO;
using Xunit;
using Serilog.Tests.Support;
using Serilog.Formatting.Display;

namespace Serilog.Tests.Formatting.Display
{
    public class MessageTemplateTextFormatterTests
    {
        [Fact]
        public void UsesFormatProvider()
        {
            var french = CultureInfo.GetCultureInfo("fr-FR");
            var formatter = new MessageTemplateTextFormatter("{Message}", french);
            var evt = DelegatingSink.GetLogEvent(l => l.Information("{0}", 12.345));
            var sw = new StringWriter();
            formatter.Format(evt, sw);
            Assert.AreEqual("12,345", sw.ToString());
        }

        [Fact]
        public void MessageTemplatesContainingFormatStringPropertiesRenderCorrectly()
        {
            var formatter = new MessageTemplateTextFormatter("{Message}", CultureInfo.InvariantCulture);
            var evt = DelegatingSink.GetLogEvent(l => l.Information("{Message}", "Hello, world!"));
            var sw = new StringWriter();
            formatter.Format(evt, sw);
            Assert.AreEqual("\"Hello, world!\"", sw.ToString());
        }

        [Fact]
        public void UppercaseFormatSpecifierIsSupportedForStrings()
        {
            var formatter = new MessageTemplateTextFormatter("{Name:u}", CultureInfo.InvariantCulture);
            var evt = DelegatingSink.GetLogEvent(l => l.Information("{Name}", "Nick"));
            var sw = new StringWriter();
            formatter.Format(evt, sw);
            Assert.AreEqual("NICK", sw.ToString());
        }

        [Fact]
        public void LowercaseFormatSpecifierIsSupportedForStrings()
        {
            var formatter = new MessageTemplateTextFormatter("{Name:w}", CultureInfo.InvariantCulture);
            var evt = DelegatingSink.GetLogEvent(l => l.Information("{Name}", "Nick"));
            var sw = new StringWriter();
            formatter.Format(evt, sw);
            Assert.AreEqual("nick", sw.ToString());
        }
    }
}
