using System;
using Serilog.Events;
using Xunit;

namespace Serilog.Tests.Support
{
    public class LogEventPropertyStructuralEqualityComparerTests
    {
        [Fact]
        public void HandlesNullAsNotEqual()
        {
            var sut = new LogEventPropertyStructuralEqualityComparer();
            Assert.False(sut.Equals(null, new LogEventProperty("a", new ScalarValue(null))));
            Assert.False(sut.Equals(new LogEventProperty("a", new ScalarValue(null)), null));
            Assert.False(sut.Equals(null, null));
        }

        [Fact]
        public void LogEventPropertyStructuralEqualityComparerWorksForScalars()
        {
            var scalarStringA = new LogEventProperty("a", new ScalarValue("a"));
            var scalarStringAStructurallyEqual = new LogEventProperty("a", new ScalarValue("a"));

            var scalarStringB = new LogEventProperty("b", new ScalarValue("b"));
            var scalarStringBStructurallyNotEqual = new LogEventProperty("b", new ScalarValue("notEqual"));

            var scalarIntA1 = new LogEventProperty("a", new ScalarValue(1));
            var scalarIntA1StructurallyEqual = new LogEventProperty("a", new ScalarValue(1));
            var scalarIntA1DiffValueSameType = new LogEventProperty("a", new ScalarValue(0));
            var scalarIntB1 = new LogEventProperty("b", new ScalarValue(1));

            var guid1 = Guid.NewGuid();
            var guid2 = Guid.NewGuid();
            var scalarGuid1 = new LogEventProperty("1", new ScalarValue(guid1));
            var scalarGuid1StructurallyEqual = new LogEventProperty("1", new ScalarValue(guid1));
            var scalarGuid1StructurallyNotEqual = new LogEventProperty("1", new ScalarValue("notEqual"));
            var scalarGuid2 = new LogEventProperty("2", new ScalarValue(guid2));

            var sut = new LogEventPropertyStructuralEqualityComparer();

            Assert.True(sut.Equals(scalarStringA, scalarStringAStructurallyEqual));
            Assert.True(sut.Equals(scalarIntA1, scalarIntA1StructurallyEqual));
            Assert.True(sut.Equals(scalarGuid1, scalarGuid1StructurallyEqual));

            Assert.False(sut.Equals(scalarStringB, scalarStringBStructurallyNotEqual));
            Assert.False(sut.Equals(scalarIntA1, scalarIntB1));
            Assert.False(sut.Equals(scalarIntA1, scalarIntA1DiffValueSameType));

            Assert.False(sut.Equals(scalarGuid1, scalarGuid2));
            Assert.False(sut.Equals(scalarGuid1, scalarGuid1StructurallyNotEqual));
        }
    }
}
