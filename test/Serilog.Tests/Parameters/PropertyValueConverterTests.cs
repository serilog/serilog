using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Serilog.Core;
using Serilog.Events;
using Serilog.Parameters;
using Serilog.Parsing;
using Serilog.Tests.Support;

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

        class A
        {
            public B B { get; set; }
        }

        class B
        {
// ReSharper disable UnusedAutoPropertyAccessor.Local
            public A A { get; set; }
// ReSharper restore UnusedAutoPropertyAccessor.Local
        }

        [Test]
        public void DestructuringACyclicStructureDoesNotStackOverflow()
        {
            var ab = new A { B = new B() };
            ab.B.A = ab;

            var pv = _converter.CreatePropertyValue(ab, true);
            Assert.IsInstanceOf<StructureValue>(pv);
        }

        struct C
        {
            public D D { get; set; }
        }

        class D
        {
            // ReSharper disable once MemberHidesStaticFromOuterClass
            // ReSharper disable once UnusedAutoPropertyAccessor.Local
            public IList<C?> C { get; set; } 
        }

        [Test]
        public void CollectionsAndCustomPoliciesInCyclesDoNotStackOverflow()
        {
            var cd = new C { D = new D() };
            cd.D.C = new List<C?> { cd };

            var pv = _converter.CreatePropertyValue(cd, true);
            Assert.IsInstanceOf<StructureValue>(pv);
        }

        [Test]
        public void ByDefaultAScalarDictionaryIsADictionaryValue()
        {
            var pv = _converter.CreatePropertyValue(new Dictionary<int, string> { { 1, "hello" } }, Destructuring.Default);
            Assert.IsInstanceOf<DictionaryValue>(pv);
            var dv = (DictionaryValue)pv;
            Assert.AreEqual(1, dv.Elements.Count);
        }

        [Test]
        public void ByDefaultANonScalarDictionaryIsASequenceValue()
        {
            var pv = _converter.CreatePropertyValue(new Dictionary<A, string> { { new A(), "hello" } }, Destructuring.Default);
            Assert.IsInstanceOf<SequenceValue>(pv);
            var sv = (SequenceValue)pv;
            Assert.AreEqual(1, sv.Elements.Count);
        }

        [Test]
        public void DelegatesAreConvertedToScalarStringsWhenDestructuring()
        {
            Action del = DelegatesAreConvertedToScalarStringsWhenDestructuring;
            var pv = _converter.CreatePropertyValue(del, Destructuring.Destructure);
            Assert.IsInstanceOf<ScalarValue>(pv);
            Assert.IsInstanceOf<string>(pv.LiteralValue());
        }

        [Test]
        public void WhenByteArraysAreConvertedTheyAreCopiedToArrayScalars()
        {
            var bytes = Enumerable.Range(0, 10).Select(b => (byte)b).ToArray();
            var pv = _converter.CreatePropertyValue(bytes);
            var lv = (byte[])pv.LiteralValue();
            Assert.AreEqual(bytes.Length, lv.Length);
            Assert.AreNotSame(bytes, lv);
        }

        [Test]
        public void ByteArraysLargerThan1kAreConvertedToStrings()
        {
            var bytes = Enumerable.Range(0, 1025).Select(b => (byte)b).ToArray();
            var pv = _converter.CreatePropertyValue(bytes);
            var lv = (string)pv.LiteralValue();
            Assert.That(lv.EndsWith("(1025 bytes)"));
        }

        public class Thrower
        {
            public string Throws { get { throw new NotSupportedException(); } }
            public string Doesnt { get { return "Hello"; } }
        }

        [Test]
        public void FailsGracefullyWhenGettersThrow()
        {
            var pv = _converter.CreatePropertyValue(new Thrower(), Destructuring.Destructure);
            var sv = (StructureValue)pv;
            Assert.AreEqual(2, sv.Properties.Count);
            var t = sv.Properties.Single(m => m.Name == "Throws");
            Assert.AreEqual("The property accessor threw an exception: NotSupportedException", t.Value.LiteralValue());
            var d = sv.Properties.Single(m => m.Name == "Doesnt");
            Assert.AreEqual("Hello", d.Value.LiteralValue());
        }

        [Test]
        public void SurvivesDestructuringASystemType()
        {
            var pv = _converter.CreatePropertyValue(typeof(string), Destructuring.Destructure);
            Assert.AreEqual(typeof(string), pv.LiteralValue()); 
        }
    }
}
