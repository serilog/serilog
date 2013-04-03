using System;
using System.Globalization;
using System.Linq;
using NUnit.Framework;
using Serilog.Events;
using Serilog.Parameters;
using Serilog.Parsing;
using Serilog.Tests.Support;

namespace Serilog.Tests.Events
{
    [TestFixture]
    public class LogEventPropertyValueTests
    {
        readonly PropertyValueConverter _converter = new PropertyValueConverter(Enumerable.Empty<Type>());

        [Test]
        public void AnEnumIsConvertedToANonStringScalarValue()
        {
            var value = _converter.CreatePropertyValue(LogEventLevel.Debug, Destructuring.Default);
            Assert.IsInstanceOf<ScalarValue>(value);
            var sv = (ScalarValue)value;
            Assert.IsNotNull(sv.Value);
            Assert.IsInstanceOf<LogEventLevel>(sv.Value);
        }

        [Test]
        public void AScalarValueToStringRendersTheValue()
        {
            var num = Some.Int();
            var value = _converter.CreatePropertyValue(num, Destructuring.Default);
            var str = value.ToString();
            Assert.AreEqual(num.ToString(CultureInfo.InvariantCulture), str);
        }

        [Test]
        public void WhenDestructuringAKnownLiteralTypeIsScalar()
        {
            var guid = Guid.NewGuid();
            var value = _converter.CreatePropertyValue(guid, Destructuring.Destructure);
            var str = value.ToString();
            Assert.AreEqual(guid.ToString(), str);
        }
    }
}
