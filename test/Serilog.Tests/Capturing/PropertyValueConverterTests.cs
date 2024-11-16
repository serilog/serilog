// ReSharper disable UnusedAutoPropertyAccessor.Global, UnusedParameter.Local, ParameterOnlyUsedForPreconditionCheck.Local, RedundantExplicitNullableCreation, MemberHidesStaticFromOuterClass
// ReSharper disable UnusedAutoPropertyAccessor.Local, UnusedMemberInSuper.Global, UnusedMember.Local, UseObjectOrCollectionInitializer, UnusedAutoPropertyAccessor.Local

// ReSharper disable PropertyCanBeMadeInitOnly.Local
namespace Serilog.Tests.Capturing;

public class PropertyValueConverterTests
{
    readonly PropertyValueConverter _converter =
        new(10, 1000, 1000, [], [], [], false);

    [Fact]
    public async Task MaximumDepthIsEffectiveAndThreadSafe()
    {
        var converter = new PropertyValueConverter(3, 1000, 1000, [], [], [], false);

        var barrier = new Barrier(participantCount: 3);

        var t1 =
            Task.Run(() => DoThreadTest(new { Root = new { B = new { C = new { D = new { E = "F" } } } } },
                result =>
                {
                    Assert.Contains("B", result);
                    Assert.Contains("C", result);
                    Assert.DoesNotContain("D", result);
                    Assert.DoesNotContain("E", result);
                }));

        var t2 =
            Task.Run(() => DoThreadTest(new { Root = new { Y = new { Z = "5" } } },
                result =>
                {
                    Assert.Contains("Y", result);
                    Assert.Contains("Z", result);
                }));

        var t3 =
            Task.Run(() => DoThreadTest(new { Root = new { M = new { N = new { V = 8 } } } },
                result =>
                {
                    Assert.Contains("M", result);
                    Assert.Contains("N", result);
                    Assert.DoesNotContain("V", result);
                }));

        await Task.WhenAll(t1, t2, t3);

        void DoThreadTest(object logObject, Action<string?> assertAction)
        {
            for (var i = 0; i < 100; ++i)
            {
                barrier.SignalAndWait();

                var propValue = converter.CreatePropertyValue(logObject, true);

                Assert.IsType<StructureValue>(propValue);

                var result = ((StructureValue)propValue).Properties.SingleOrDefault(p => p.Name == "Root")?.Value.ToString();

                assertAction.Invoke(result);
            }
        }
    }

    [Fact]
    public void UnderDestructuringAByteArrayIsAScalarValue()
    {
        var pv = _converter.CreatePropertyValue(Array.Empty<byte>(), Destructuring.Destructure);
        Assert.IsType<ScalarValue>(pv);
        Assert.IsType<string>(((ScalarValue)pv).Value);
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
        var pv = _converter.CreatePropertyValue(Array.Empty<int>(), Destructuring.Destructure);
        Assert.IsType<SequenceValue>(pv);
    }

    [Fact]
    public void ByDefaultADestructuredNullNullableIsAScalarNull()
    {
        var pv = _converter.CreatePropertyValue(default(int?), Destructuring.Destructure);
        Assert.Null(((ScalarValue)pv).Value);
    }

    [Fact]
    public void ByDefaultADestructuredNonNullNullableIsItsValue()
    {
        var pv = _converter.CreatePropertyValue(new int?(2), Destructuring.Destructure);
        Assert.Equal(2, ((ScalarValue)pv).Value);
    }

    class A
    {
        public B? B { get; set; }
    }

    class B
    {
        public A? A { get; set; }
    }

    [Fact]
    public void DestructuringACyclicStructureDoesNotStackOverflow()
    {
        var ab = new A { B = new() };
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
        public IList<C?>? C { get; set; }
    }

    [Fact]
    public void CollectionsAndCustomPoliciesInCyclesDoNotStackOverflow()
    {
        var cd = new C { D = new() };
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
        Assert.Single(dv.Elements);
    }

    [Fact]
    public void ByDefaultANonScalarDictionaryIsASequenceValue()
    {
        var pv = _converter.CreatePropertyValue(new Dictionary<A, string> { { new A(), "hello" } }, Destructuring.Default);
        Assert.IsType<SequenceValue>(pv);
        var sv = (SequenceValue)pv;
        Assert.Single(sv.Elements);
    }

    [Fact]
    public void DelegatesAreConvertedToScalarStringsWhenDestructuring()
    {
        var del = DelegatesAreConvertedToScalarStringsWhenDestructuring;
        var pv = _converter.CreatePropertyValue(del, Destructuring.Destructure);
        Assert.IsType<ScalarValue>(pv);
        Assert.IsType<string>(pv.LiteralValue());
    }

    [Fact]
    public void ByteArraysAreConvertedToStrings()
    {
        var bytes = Enumerable.Range(0, 10).Select(b => (byte)b).ToArray();
        var pv = _converter.CreatePropertyValue(bytes);
        var lv = (string?)pv.LiteralValue();
        Assert.Equal("00010203040506070809", lv);
    }

    [Fact]
    public void ByteArraysLargerThan1kAreLimitedAndConvertedToStrings()
    {
        var bytes = Enumerable.Range(0, 1025).Select(b => (byte)b).ToArray();
        var pv = _converter.CreatePropertyValue(bytes);
        var lv = (string?)pv.LiteralValue();
        Assert.Equal("000102030405060708090A0B0C0D0E0F... (1025 bytes)", lv);
    }

#if FEATURE_SPAN
        [Fact]
        public void ByteSpansAreConvertedToStrings()
        {
            var bytes = Enumerable.Range(0, 10).Select(b => (byte)b).ToArray().AsMemory();
            var pv = _converter.CreatePropertyValue(bytes);
            var lv = (string?)pv.LiteralValue();
            Assert.Equal("00010203040506070809", lv);
        }

        [Fact]
        public void ByteSpansLargerThan1kAreLimitedAndConvertedToStrings()
        {
            var bytes = Enumerable.Range(0, 1025).Select(b => (byte)b).ToArray().AsMemory();
            var pv = _converter.CreatePropertyValue(bytes);
            var lv = (string?)pv.LiteralValue();
            Assert.EndsWith("(1025 bytes)", lv);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(1000)]
        [InlineData(10000)]
        public void ByteSpansAreConvertedToTheSameStringsAsArrays(int length)
        {
            var bytes = Enumerable.Range(0, length).Select(b => (byte)b).ToArray();
            var bytesSpan = bytes.AsMemory();

            var bytesResult = _converter.CreatePropertyValue(bytes).LiteralValue();
            var bytesSpanResult = _converter.CreatePropertyValue(bytesSpan).LiteralValue();

            Assert.Equal(bytesResult, bytesSpanResult);
        }

        [Fact]
        public void FailsGracefullyWhenAccessingPropertiesViaReflectionThrows()
        {
            var thrower = new[] { 1, 2, 3 }.AsMemory();

            var pv = _converter.CreatePropertyValue(thrower, Destructuring.Destructure);
            var sv = (StructureValue)pv;
            Assert.Equal(3, sv.Properties.Count);
            var t = sv.Properties.Single(m => m.Name == "Span");
            Assert.Equal("Accessing this property is not supported via Reflection API", t.Value.LiteralValue());
            var l = sv.Properties.Single(m => m.Name == "Length");
            Assert.Equal(3, l.Value.LiteralValue());
            var k = sv.Properties.Single(m => m.Name == "IsEmpty");
            Assert.False((bool?)k.Value.LiteralValue());
        }
#endif

    public class Thrower
    {
        public string Throws => throw new NotSupportedException();
        public string Doesnt => "Hello";
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
        var theMethod = MethodBase.GetCurrentMethod();
        var pv = _converter.CreatePropertyValue(theMethod, Destructuring.Destructure);
        Assert.Equal(theMethod, pv.LiteralValue());
    }

    public class BaseWithProps
    {
        public string? PropA { get; set; }
        public virtual string? PropB { get; set; }
        public string? PropC { get; set; }
    }

    public class DerivedWithOverrides : BaseWithProps
    {
        public new string? PropA { get; set; }
        public override string? PropB { get; set; }
        public string? PropD { get; set; }
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

        var pv = (StructureValue)_converter.CreatePropertyValue(valAsDerived, true);

        Assert.Equal(4, pv.Properties.Count);
        Assert.Equal("A", pv.Properties.Single(pp => pp.Name == "PropA").Value.LiteralValue());
        Assert.Equal("B", pv.Properties.Single(pp => pp.Name == "PropB").Value.LiteralValue());
    }

    class HasIndexer
    {
        public string this[int index] => "Indexer";
    }

    [Fact]
    public void IndexerPropertiesAreIgnoredWhenDestructuring()
    {
        var indexed = new HasIndexer();
        var pv = (StructureValue)_converter.CreatePropertyValue(indexed, true);
        Assert.Empty(pv.Properties);
    }

    // Important because we use "Item" to short cut indexer checking
    // (reducing garbage).
    class HasItem
    {
        public string Item => "Item";
    }

    [Fact]
    public void ItemPropertiesNotAreIgnoredWhenDestructuring()
    {
        var indexed = new HasItem();
        var pv = (StructureValue)_converter.CreatePropertyValue(indexed, true);
        Assert.Single(pv.Properties);
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
        Assert.Null(structuredValue.TypeTag);
    }

    [Fact]
    public void ValueTuplesAreRecognizedWhenDestructuring()
    {
        var o = (1, "A", new[] { "B" });
        var result = _converter.CreatePropertyValue(o);

        var sequenceValue = Assert.IsType<SequenceValue>(result);

        Assert.Equal(3, sequenceValue.Elements.Count);
        Assert.Equal(new ScalarValue(1), sequenceValue.Elements[0]);
        Assert.Equal(new ScalarValue("A"), sequenceValue.Elements[1]);
        var nested = Assert.IsType<SequenceValue>(sequenceValue.Elements[2]);
        Assert.Single(nested.Elements);
        Assert.Equal(new ScalarValue("B"), nested.Elements[0]);
    }

    [Fact]
    public void AllTupleLengthsUpToSevenAreSupportedForCapturing()
    {
        var tuples = new object[]
        {
            ValueTuple.Create(1),
            (1, 2),
            (1, 2, 3),
            (1, 2, 3, 4),
            (1, 2, 3, 4, 5),
            (1, 2, 3, 4, 5, 6),
            (1, 2, 3, 4, 5, 6, 7)
        };

        foreach (var t in tuples)
            Assert.IsType<SequenceValue>(_converter.CreatePropertyValue(t));
    }

#if FEATURE_ITUPLE

    [Fact]
    public void EightPlusValueTupleElementsAreSupportedForCapturing()
    {
        var scalar = _converter.CreatePropertyValue((1, 2, 3, 4, 5, 6, 7, 8));
        Assert.IsType<SequenceValue>(scalar);
    }

#else

    [Fact]
    public void EightPlusValueTupleElementsAreIgnoredByCapturing()
    {
        var scalar = _converter.CreatePropertyValue((1, 2, 3, 4, 5, 6, 7, 8));
        Assert.IsType<ScalarValue>(scalar);
    }

#endif

    [Fact]
    public void ValueTupleDestructuringIsTransitivelyApplied()
    {
        var tuple = _converter.CreatePropertyValue(ValueTuple.Create(new { A = 1 }), true);
        var sequence = Assert.IsType<SequenceValue>(tuple);
        Assert.IsType<StructureValue>(sequence.Elements[0]);
    }

    [Fact]
    public void StructureConversionYieldsEmptyStructureOnNoProperties()
    {
        var structure = _converter.CreateStructureValue(new Empty(), typeof(Empty), false);
        Assert.Empty(structure.Properties);
    }

    [Fact]
    public void TypeTagIsComputed()
    {
        var structure = _converter.CreateStructureValue(new Empty(), typeof(Empty), false);
        Assert.Equal("Empty", structure.TypeTag);
    }

    [Fact]
    public void TypeTagIsNotComputedForCompilerGeneratedTypes()
    {
        var structure = _converter.CreateStructureValue(new Empty(), typeof(Empty), true);
        Assert.Null(structure.TypeTag);
    }

    [Fact]
    public void RegularTypesAreNotCompilerGenerated()
    {
        Assert.False(PropertyValueConverter.IsCompilerGeneratedType(typeof(Empty)));
    }

    [Fact]
    public void CompilerGeneratedTypesAreIdentified()
    {
        Assert.True(PropertyValueConverter.IsCompilerGeneratedType(new { A = 1 }.GetType()));
    }

#if TEST_FEATURE_WCF
    [Fact]
    // https://github.com/serilog/serilog/issues/1235
    public void StructureConversionDoesNotThrowOnWcfProxyTypes()
    {
        var remoteAddress = new System.ServiceModel.EndpointAddress("http://localhost");
        var binding = new System.ServiceModel.BasicHttpBinding();

        var myFactory = new System.ServiceModel.ChannelFactory<IMyChannel>(binding, remoteAddress);
        var channel = myFactory.CreateChannel();

        _converter.CreateStructureValue(channel, channel.GetType(), false);
    }

    [System.ServiceModel.ServiceContract]
    interface IMyChannel
    {
        [System.ServiceModel.OperationContract]
        string Get();
    }
#endif

    [Fact]
    public void StructureConversionPrefersMostDerivedDefinition()
    {
        var structure = _converter.CreateStructureValue(new SubclassWithRedefinition(), typeof(SubclassWithRedefinition), false);
        Assert.Equal("new", ((ScalarValue)structure.Properties.Single(p => p.Name == "Name").Value).Value);
    }

    [Fact]
    public void StructureConversionPrefersMostDerivedOverride()
    {
        var structure = _converter.CreateStructureValue(new SubclassWithOverride(), typeof(SubclassWithOverride), false);
        Assert.Equal("override", ((ScalarValue)structure.Properties.Single(p => p.Name == "Name").Value).Value);
    }

    class Empty;

    public class BaseClass
    {
        public virtual string Name => "base";
    }

    public class SubclassWithRedefinition : BaseClass
    {
        public new string Name => "new";
    }

    public class SubclassWithOverride : BaseClass
    {
        public override string Name => "override";
    }

    [Fact]
    public void PrivateGettersAreIgnoredWhenCapturing()
    {
        var structure = _converter.CreateStructureValue(new HasPropertyWithPrivateGetter(), typeof(HasPropertyWithPrivateGetter), false);
        Assert.Contains(structure.Properties, p => p.Name == "A");
        Assert.DoesNotContain(structure.Properties, p => p.Name == "B");
    }

    public class HasPropertyWithPrivateGetter
    {
        public string A { get; set; } = "A";
        public string B { private get; set; } = "B";
    }
}
