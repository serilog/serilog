using BenchmarkDotNet.Attributes;
using Serilog.Events;
using Serilog.Expressions.PerformanceTests.Support;
using Xunit;

namespace Serilog.Expressions.PerformanceTests;

/// <summary>
/// Tests the performance of various filtering mechanisms.
/// </summary>
public class ComparisonBenchmark
{
    readonly Func<LogEvent, bool> _trivialFilter, _handwrittenFilter, _expressionFilter;
    readonly LogEvent _event = Some.InformationEvent("{A}", 3);

    public ComparisonBenchmark()
    {
        // Just the delegate invocation overhead
        _trivialFilter = _ => true;

        // `A == 3`, the old way
        _handwrittenFilter = evt =>
        {
            if (evt.Properties.TryGetValue("A", out var a) && (a as ScalarValue)?.Value is int)
            {
                return (int)((ScalarValue)a).Value! == 3;
            }

            return false;
        };

        // The code we're interested in; the `true.Equals()` overhead is normally added when
        // this is used with Serilog
        var compiled = SerilogExpression.Compile("A = 3");
        _expressionFilter = evt => ExpressionResult.IsTrue(compiled(evt));

        Assert.True(_trivialFilter(_event) && _handwrittenFilter(_event) && _expressionFilter(_event));
    }

    [Benchmark]
    public void TrivialFilter()
    {
        _trivialFilter(_event);
    }

    [Benchmark(Baseline = true)]
    public void HandwrittenFilter()
    {
        _handwrittenFilter(_event);
    }

    [Benchmark]
    public void ExpressionFilter()
    {
        _expressionFilter(_event);
    }
}