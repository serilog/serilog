using System.Globalization;
using NUnit.Framework;
using Serilog.Events;
using Serilog.Parsing;
using Serilog.Tests.Support;

namespace Serilog.Tests.Events
{
    [TestFixture]
    public class LogEventPropertyValueTests
    {
        [Test]
        public void AnEnumIsConvertedToALiteralValue()
        {
            var value = LogEventPropertyValue.For(LogEventLevel.Debug, Destructuring.Default);
            Assert.IsInstanceOf<ScalarValue>(value);
        }

        [Test]
        public void AScalarValueToStringRendersTheValue()
        {
            var num = Some.Int();
            var value = LogEventPropertyValue.For(num, Destructuring.Default);
            var str = value.ToString();
            Assert.AreEqual(num.ToString(CultureInfo.InvariantCulture), str);
        }
    }
}
