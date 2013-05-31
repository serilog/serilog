using System.Collections.Generic;
using NUnit.Framework;
using Serilog.Events;

namespace Serilog.Sinks.RavenDB.Tests
{
    [TestFixture]
    public class RavenPropertyFormatterTests
    {
        readonly ScalarValue _scalarComplex = new ScalarValue(new { A = 1, B = 2 });

        [Test]
        public void ASimpleNullIsNull()
        {
            Assert.IsNull(RavenPropertyFormatter.Simplify(null));
        }

        [Test]
        public void WhenAComplexTypeIsPassedAsAScalarTheResultIsAString()
        {
            var simplified = RavenPropertyFormatter.Simplify(_scalarComplex);
            Assert.IsInstanceOf<string>(simplified);
        }

        [Test]
        public void WhenASequenceIsSimplifiedTheResultIsAnArray()
        {
            var simplified = RavenPropertyFormatter.Simplify(new SequenceValue(new[] { _scalarComplex }));
            Assert.IsInstanceOf<object[]>(simplified);
            Assert.IsInstanceOf<string>(((object[])simplified)[0]);
        }

        [Test]
        public void ASimplifiedStructureIsADictionary()
        {
            var simplified = RavenPropertyFormatter.Simplify(new StructureValue(new[] { new LogEventProperty("C", _scalarComplex) }));
            Assert.IsInstanceOf<Dictionary<string,object>>(simplified);
            Assert.IsInstanceOf<string>(((Dictionary<string, object>)simplified)["C"]);
        }
    }
}
