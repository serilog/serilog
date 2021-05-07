﻿using Serilog.Data;
using Serilog.Events;
using System.Linq;
using Xunit;

namespace Serilog.Tests.Data
{
    class LimitingRewriter : LogEventPropertyValueRewriter<int>
    {
        public LogEventPropertyValue LimitStringLength(LogEventPropertyValue value, int maximumLength)
        {
            return Visit(maximumLength, value);
        }

        protected override LogEventPropertyValue VisitScalarValue(int state, ScalarValue scalar)
        {
            var str = scalar.Value as string;
            if (str == null || str.Length <= state)
                return scalar;

            return new ScalarValue(str.Substring(0, state));
        }
    }

    public class LogEventPropertyValueRewriterTests
    {
        [Fact]
        public void StatePropagatesAndNestedStructuresAreRewritten()
        {
            SequenceValue value = new(new[]
            {
                new StructureValue(new[]
                {
                    new LogEventProperty("S", new ScalarValue("abcde"))
                })
            });

            LimitingRewriter limiter = new();
            var limited = limiter.LimitStringLength(value, 3);

            var seq = limited as SequenceValue;
            Assert.NotNull(seq);

            var str = seq.Elements.Single() as StructureValue;
            Assert.NotNull(str);

            var prop = str.Properties.Single();
            Assert.Equal("S", prop.Name);

            var sca = prop.Value as ScalarValue;
            Assert.NotNull(sca);

            Assert.Equal("abc", sca.Value);
        }

        [Fact]
        public void WhenNoRewritingTakesPlaceAllElementsAreUnchanged()
        {
            SequenceValue value = new(new[]
            {
                new StructureValue(new[]
                {
                    new LogEventProperty("S", new ScalarValue("abcde"))
                })
            });
            LimitingRewriter limiter = new();
            var unchanged = limiter.LimitStringLength(value, 10);
            Assert.Same(value, unchanged);
        }
    }
}
