using NUnit.Framework;
using Serilog.Parsing;
using Serilog.Values;

namespace Serilog.Tests
{
    [TestFixture]
    public class LogEventPropertyValueTests
    {
        [Test]
        public void AnEnumIsConvertedToALiteralValue()
        {
            var value = LogEventPropertyValue.For(LogEventLevel.Debug, DestructuringHint.Default);
            Assert.IsInstanceOf<LogEventPropertyTokenValue>(value);
        }

    }
}
