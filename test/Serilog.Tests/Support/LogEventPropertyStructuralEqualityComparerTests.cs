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
            LogEventPropertyStructuralEqualityComparer sut = new();
            Assert.False(sut.Equals(null, new("a", new ScalarValue(null))));
            Assert.False(sut.Equals(new("a", new ScalarValue(null)), null));
            Assert.False(sut.Equals(null, null));
        }

        [Fact]
        public void LogEventPropertyStructuralEqualityComparerWorksForSequences()
        {
            ScalarValue[] intStringDoubleScalarArray =
                { new(1), new("2"), new(3.0), };

            ScalarValue[] intStringFloatScalarArray =
                { new("1"), new(2), new(3.0f), };

            LogEventProperty sequenceOfScalarsIntStringDoubleA = new("a", new SequenceValue(intStringDoubleScalarArray));

            LogEventProperty sequenceOfScalarsIntStringDoubleAStructurallyEqual = new("a",
                new SequenceValue(new ScalarValue[] { new(1), new("2"), new(3.0), }));

            LogEventProperty sequenceOfScalarsIntStringDoubleAStructurallyNotEqual = new("a",
                new SequenceValue(new ScalarValue[] { new(1), new("2"), new(3.1), }));

            LogEventProperty sequenceOfScalarsIntStringFloatA = new("a", new ScalarValue(intStringFloatScalarArray));

            LogEventProperty sequenceOfScalarsIntStringDoubleB = new("b", new SequenceValue(intStringDoubleScalarArray));

            LogEventPropertyStructuralEqualityComparer sut = new();

            // Structurally equal
            Assert.True(sut.Equals(sequenceOfScalarsIntStringDoubleA, sequenceOfScalarsIntStringDoubleAStructurallyEqual));

            // Not equal due to having a different property name (but otherwise structurally equal)
            Assert.False(sut.Equals(sequenceOfScalarsIntStringDoubleA, sequenceOfScalarsIntStringDoubleB));

            // Structurally not equal because element 3 has a different value
            Assert.False(sut.Equals(sequenceOfScalarsIntStringDoubleA, sequenceOfScalarsIntStringDoubleAStructurallyNotEqual));

            // Structurally not equal because element 3 has a different underlying value and type
            Assert.False(sut.Equals(sequenceOfScalarsIntStringDoubleA, sequenceOfScalarsIntStringFloatA));
        }

        [Fact]
        public void LogEventPropertyStructuralEqualityComparerWorksForScalars()
        {
            LogEventProperty scalarStringA = new("a", new ScalarValue("a"));
            LogEventProperty scalarStringAStructurallyEqual = new("a", new ScalarValue("a"));

            LogEventProperty scalarStringB = new("b", new ScalarValue("b"));
            LogEventProperty scalarStringBStructurallyNotEqual = new("b", new ScalarValue("notEqual"));

            LogEventProperty scalarIntA1 = new("a", new ScalarValue(1));
            LogEventProperty scalarIntA1StructurallyEqual = new("a", new ScalarValue(1));
            LogEventProperty scalarIntA1DiffValueSameType = new("a", new ScalarValue(0));
            LogEventProperty scalarIntB1 = new("b", new ScalarValue(1));

            var guid1 = Guid.NewGuid();
            var guid2 = Guid.NewGuid();
            LogEventProperty scalarGuid1 = new("1", new ScalarValue(guid1));
            LogEventProperty scalarGuid1StructurallyEqual = new("1", new ScalarValue(guid1));
            LogEventProperty scalarGuid1StructurallyNotEqual = new("1", new ScalarValue("notEqual"));
            LogEventProperty scalarGuid2 = new("2", new ScalarValue(guid2));

            LogEventPropertyStructuralEqualityComparer sut = new();

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
