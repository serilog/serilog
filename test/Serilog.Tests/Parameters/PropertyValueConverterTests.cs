#if INTERNAL_TESTS

using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Serilog.Core;
using Serilog.Events;
using Serilog.Parameters;
using Serilog.Parsing;
using Serilog.Tests.Support;

namespace Serilog.Tests.Parameters
{
    public class PropertyValueConverterTests
    {
        readonly PropertyValueConverter _converter = new PropertyValueConverter(10, Enumerable.Empty<Type>(), Enumerable.Empty<IDestructuringPolicy>());

        [Fact]
        public void UnderDestructuringAByteArrayIsAScalarValue()
        {
            var pv = _converter.CreatePropertyValue(new byte[0], Destructuring.Destructure);
            Assert.IsType<ScalarValue>(pv);
            Assert.IsType<byte[]>(((ScalarValue)pv).Value);
        }

        [Fact]
        public void UnderDestructuringABooleanIsAScalarValue()
        {
            var pv = _converter.CreatePropertyValue(true, Destructuring.Destructure);
            Assert.IsType<ScalarValue>(pv);
            Assert.IsType<bool>(((ScalarValue)pv).Value);
        }

        [Fact]
        public void UnderDestructuringAnIntegerArrayIsASequenceValue()
        {
            var pv = _converter.CreatePropertyValue(new int[0], Destructuring.Destructure);
            Assert.IsType<SequenceValue>(pv);
        }

        [Fact]
        public void ByDefaultADestructuredNullNullableIsAScalarNull()
        {
            var pv = _converter.CreatePropertyValue(new int?(), Destructuring.Destructure);
            Assert.Null(((ScalarValue)pv).Value);
        }

        [Fact]
        public void ByDefaultADestructuredNonNullNullableIsItsValue()
        {
            // ReSharper disable RedundantExplicitNullableCreation
            var pv = _converter.CreatePropertyValue(new int?(2), Destructuring.Destructure);
            // ReSharper restore RedundantExplicitNullableCreation
            Assert.Equal(2, ((ScalarValue)pv).Value);
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

        [Fact]
        public void DestructuringACyclicStructureDoesNotStackOverflow()
        {
            var ab = new A { B = new B() };
            ab.B.A = ab;

            var pv = _converter.CreatePropertyValue(ab, true);
            Assert.IsType<StructureValue>(pv);
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

        [Fact]
        public void CollectionsAndCustomPoliciesInCyclesDoNotStackOverflow()
        {
            var cd = new C { D = new D() };
            cd.D.C = new List<C?> { cd };

            var pv = _converter.CreatePropertyValue(cd, true);
            Assert.IsType<StructureValue>(pv);
        }

        [Fact]
        public void ByDefaultAScalarDictionaryIsADictionaryValue()
        {
            var pv = _converter.CreatePropertyValue(new Dictionary<int, string> { { 1, "hello" } }, Destructuring.Default);
            Assert.IsType<DictionaryValue>(pv);
            var dv = (DictionaryValue)pv;
            Assert.Equal(1, dv.Elements.Count);
        }

        [Fact]
        public void ByDefaultANonScalarDictionaryIsASequenceValue()
        {
            var pv = _converter.CreatePropertyValue(new Dictionary<A, string> { { new A(), "hello" } }, Destructuring.Default);
            Assert.IsType<SequenceValue>(pv);
            var sv = (SequenceValue)pv;
            Assert.Equal(1, sv.Elements.Count);
        }

        [Fact]
        public void DelegatesAreConvertedToScalarStringsWhenDestructuring()
        {
            Action del = DelegatesAreConvertedToScalarStringsWhenDestructuring;
            var pv = _converter.CreatePropertyValue(del, Destructuring.Destructure);
            Assert.IsType<ScalarValue>(pv);
            Assert.IsType<string>(pv.LiteralValue());
        }

        [Fact]
        public void WhenByteArraysAreConvertedTheyAreCopiedToArrayScalars()
        {
            var bytes = Enumerable.Range(0, 10).Select(b => (byte)b).ToArray();
            var pv = _converter.CreatePropertyValue(bytes);
            var lv = (byte[])pv.LiteralValue();
            Assert.Equal(bytes.Length, lv.Length);
            Assert.NotSame(bytes, lv);
        }

        [Fact]
        public void ByteArraysLargerThan1kAreConvertedToStrings()
        {
            var bytes = Enumerable.Range(0, 1025).Select(b => (byte)b).ToArray();
            var pv = _converter.CreatePropertyValue(bytes);
            var lv = (string)pv.LiteralValue();
            Assert.True(lv.EndsWith("(1025 bytes)"));
        }

        public class Thrower
        {
            public string Throws { get { throw new NotSupportedException(); } }
            public string Doesnt { get { return "Hello"; } }
        }

        [Fact]
        public void FailsGracefullyWhenGettersThrow()
        {
            var pv = _converter.CreatePropertyValue(new Thrower(), Destructuring.Destructure);
            var sv = (StructureValue)pv;
            Assert.Equal(2, sv.Properties.Count);
            var t = sv.Properties.Single(m => m.Name == "Throws");
            Assert.Equal("The property accessor threw an exception: NotSupportedException", t.Value.LiteralValue());
            var d = sv.Properties.Single(m => m.Name == "Doesnt");
            Assert.Equal("Hello", d.Value.LiteralValue());
        }

        [Fact]
        public void SurvivesDestructuringASystemType()
        {
            var pv = _converter.CreatePropertyValue(typeof(string), Destructuring.Destructure);
            Assert.Equal(typeof(string), pv.LiteralValue());
        }

        [Fact]
        public void SurvivesDestructuringMethodBase()
        {
            var theMethod = System.Reflection.MethodBase.GetCurrentMethod();
            var pv = _converter.CreatePropertyValue(theMethod, Destructuring.Destructure);
            Assert.Equal(theMethod, pv.LiteralValue());
        }

        public class BaseWithProps
        {
            public string PropA { get; set; }
            public virtual string PropB { get; set; }
            public string PropC { get; set; }
        }

        public class DerivedWithOverrides : BaseWithProps
        {
            new public string PropA { get; set; }
            public override string PropB { get; set; }
            public string PropD { get; set; }
        }

        [Fact]
        public void NewAndInheritedPropertiesAppearOnlyOnce()
        {
            var valAsDerived = new DerivedWithOverrides
            {
                PropA = "A",
                PropB = "B",
                PropC = "C",
                PropD = "D"
            };

            var valAsBase = (BaseWithProps)valAsDerived;
            valAsBase.PropA = "BA";

            var pv = (StructureValue) _converter.CreatePropertyValue(valAsDerived, true);

            Assert.Equal(4, pv.Properties.Count);
            Assert.Equal("A", pv.Properties.Single(pp => pp.Name == "PropA").Value.LiteralValue());
            Assert.Equal("B", pv.Properties.Single(pp => pp.Name == "PropB").Value.LiteralValue());
        }

        class HasIndexer
        {
            public string this[int index] { get { return "Indexer"; } }
        }

        [Fact]
        public void IndexerPropertiesAreIgnoredWhenDestructuring()
        {
            var indexed = new HasIndexer();
            var pv = (StructureValue)_converter.CreatePropertyValue(indexed, true);
            Assert.Equal(0, pv.Properties.Count);
        }

        // Important because we use "Item" to short cut indexer checking
        // (reducing garbage).
        class HasItem
        {
            public string Item { get { return "Item"; } }
        }

        [Fact]
        public void ItemPropertiesNotAreIgnoredWhenDestructuring()
        {
            var indexed = new HasItem();
            var pv = (StructureValue)_converter.CreatePropertyValue(indexed, true);
            Assert.Equal(1, pv.Properties.Count);
            var item = pv.Properties.Single();
            Assert.Equal("Item", item.Name);
        }

        [Fact]
        public void CSharpAnonymousTypesAreRecognizedWhenDestructuring()
        {
            var o = new { Foo = "Bar" };
            var result = _converter.CreatePropertyValue(o, true);
            Assert.Equal(typeof(StructureValue), result.GetType());
            var structuredValue = (StructureValue)result;
            Assert.Equal(null, structuredValue.TypeTag);
        }
    }
}

#endif
