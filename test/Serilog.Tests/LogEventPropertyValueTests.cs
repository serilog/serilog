using NUnit.Framework;
using Serilog.Events;
using Serilog.Parsing;

namespace Serilog.Tests
{
    [TestFixture]
    public class LogEventPropertyValueTests
    {
        [Test]
        public void AnEnumIsConvertedToALiteralValue()
        {
            var value = LogEventPropertyValue.For(LogEventLevel.Debug, Destructuring.Default);
            Assert.IsInstanceOf<LogEventPropertyLiteralValue>(value);
        }

    }
}
