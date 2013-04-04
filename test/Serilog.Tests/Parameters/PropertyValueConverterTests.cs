using System;
using System.Linq;
using NUnit.Framework;
using Serilog.Core;
using Serilog.Events;
using Serilog.Parameters;
using Serilog.Parsing;

namespace Serilog.Tests.Parameters
{
    [TestFixture]
    public class PropertyValueConverterTests
    {
        readonly PropertyValueConverter _converter = new PropertyValueConverter(Enumerable.Empty<Type>(), Enumerable.Empty<IDestructuringPolicy>());

        [Test]
        public void UnderDestructuringAByteArrayIsAScalarValue()
        {
            var pv = _converter.CreatePropertyValue(new byte[0], Destructuring.Destructure);
            Assert.IsInstanceOf<ScalarValue>(pv);
            Assert.IsInstanceOf<byte[]>(((ScalarValue)pv).Value);
        }

        [Test]
        public void UnderDestructuringAnIntegerArrayIsASequenceValue()
        {
            var pv = _converter.CreatePropertyValue(new int[0], Destructuring.Destructure);
            Assert.IsInstanceOf<SequenceValue>(pv);
        }

        [Test]
        public void ByDefaultADestructuredNullNullableIsAScalarNull()
        {
            var pv = _converter.CreatePropertyValue(new int?(), Destructuring.Destructure);
            Assert.IsNull(((ScalarValue)pv).Value);
        }

        [Test]
        public void ByDefaultADestructuredNonNullNullableIsItsValue()
        {
            // ReSharper disable RedundantExplicitNullableCreation
            var pv = _converter.CreatePropertyValue(new int?(2), Destructuring.Destructure);
            // ReSharper restore RedundantExplicitNullableCreation
            Assert.AreEqual(2, ((ScalarValue)pv).Value);
        }
    }
}
