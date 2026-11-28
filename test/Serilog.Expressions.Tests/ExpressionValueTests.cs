using Serilog.Events;
using Xunit;

namespace Serilog.Expressions.Tests;

public class ExpressionValueTests
{
    [Fact]
    public void UndefinedResultsAreFalse()
    {
        Assert.False(ExpressionResult.IsTrue(null));
    }

    [Fact]
    public void NonBooleanResultsAreFalse()
    {
        Assert.False(ExpressionResult.IsTrue(new ScalarValue(10)));
    }

    [Fact]
    public void TrueIsTrue()
    {
        Assert.True(ExpressionResult.IsTrue(new ScalarValue(true)));
    }

    [Fact]
    public void FalseIsNotTrue()
    {
        Assert.False(ExpressionResult.IsTrue(new ScalarValue(false)));
    }
}