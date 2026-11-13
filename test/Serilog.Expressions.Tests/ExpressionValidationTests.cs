using Xunit;

namespace Serilog.Expressions.Tests;

public class ExpressionValidationTests
{
    [Theory]
    [InlineData("IsMatch(Name, '[invalid')", "Invalid regular expression")]
    [InlineData("IndexOfMatch(Text, '(?<')", "Invalid regular expression")]
    [InlineData("IsMatch(Name, '(?P<name>)')", "Invalid regular expression")]
    [InlineData("IsMatch(Name, '(unclosed')", "Invalid regular expression")]
    [InlineData("IndexOfMatch(Text, '*invalid')", "Invalid regular expression")]
    public void InvalidRegularExpressionsAreReportedGracefully(string expression, string expectedErrorFragment)
    {
        var result = SerilogExpression.TryCompile(expression, out var compiled, out var error);
        Assert.False(result);
        Assert.Contains(expectedErrorFragment, error);
        Assert.Null(compiled);
    }

    [Theory]
    [InlineData("UnknownFunction()", "The function name `UnknownFunction` was not recognized.")]
    [InlineData("foo(1, 2, 3)", "The function name `foo` was not recognized.")]
    [InlineData("MyCustomFunc(Name)", "The function name `MyCustomFunc` was not recognized.")]
    [InlineData("notAFunction()", "The function name `notAFunction` was not recognized.")]
    public void UnknownFunctionNamesAreReportedGracefully(string expression, string expectedError)
    {
        var result = SerilogExpression.TryCompile(expression, out var compiled, out var error);
        Assert.False(result);
        Assert.Equal(expectedError, error);
        Assert.Null(compiled);
    }

    [Theory]
    [InlineData("Length(Name) ci")]
    [InlineData("Round(Value, 2) ci")]
    [InlineData("Now() ci")]
    [InlineData("TypeOf(Value) ci")]
    [InlineData("IsDefined(Prop) ci")]
    public void InvalidCiModifierUsageCompilesWithWarning(string expression)
    {
        var result = SerilogExpression.TryCompile(expression, out var compiled, out var error);
        Assert.True(result, $"Failed to compile: {error}");
        Assert.NotNull(compiled);
        Assert.Null(error);
    }

    [Theory]
    [InlineData("Contains(Name, 'test') ci")]
    [InlineData("StartsWith(Path, '/api') ci")]
    [InlineData("EndsWith(File, '.txt') ci")]
    [InlineData("IsMatch(Email, '@example') ci")]
    [InlineData("IndexOfMatch(Text, 'pattern') ci")]
    [InlineData("IndexOf(Name, 'x') ci")]
    [InlineData("Name = 'test' ci")]
    [InlineData("Name <> 'test' ci")]
    [InlineData("Name like '%test%' ci")]
    public void ValidCiModifierUsageCompiles(string expression)
    {
        var result = SerilogExpression.TryCompile(expression, out var compiled, out var error);
        Assert.True(result, $"Failed to compile: {error}");
        Assert.NotNull(compiled);
        Assert.Null(error);
    }

    [Fact]
    public void FirstErrorIsReportedInComplexExpressions()
    {
        var expression = "UnknownFunc() and Length(Value) > 5";
        var result = SerilogExpression.TryCompile(expression, out var compiled, out var error);
        
        Assert.False(result);
        Assert.Null(compiled);
        
        // Should report the first error encountered
        Assert.Contains("UnknownFunc", error);
        Assert.NotNull(error);
    }

    [Fact]
    public void ValidExpressionsStillCompileWithoutErrors()
    {
        var validExpressions = new[]
        {
            "IsMatch(Name, '^[A-Z]')",
            "IndexOfMatch(Text, '\\d+')",
            "Contains(Name, 'test') ci",
            "Length(Items) > 5",
            "Round(Value, 2)",
            "TypeOf(Data) = 'String'",
            "Name like '%test%'",
            "StartsWith(Path, '/') and EndsWith(Path, '.json')"
        };
        
        foreach (var expr in validExpressions)
        {
            var result = SerilogExpression.TryCompile(expr, out var compiled, out var error);
            Assert.True(result, $"Failed to compile: {expr}. Error: {error}");
            Assert.NotNull(compiled);
            Assert.Null(error);
        }
    }

    [Fact]
    public void CompileMethodStillThrowsForInvalidExpressions()
    {
        // Ensure Compile() method (not TryCompile) maintains throwing behavior for invalid expressions
        Assert.Throws<ArgumentException>(() => 
            SerilogExpression.Compile("UnknownFunction()"));
        
        Assert.Throws<ArgumentException>(() => 
            SerilogExpression.Compile("IsMatch(Name, '[invalid')"));
        
        // CI modifier on non-supporting functions compiles with warning
        var compiledWithCi = SerilogExpression.Compile("Length(Name) ci");
        Assert.NotNull(compiledWithCi);
        
        Assert.Throws<ArgumentException>(() => 
            SerilogExpression.Compile("IndexOfMatch(Text, '(?<')"));
    }

    [Theory]
    [InlineData("IsMatch(Name, Name)")] // Non-constant pattern
    [InlineData("IndexOfMatch(Text, Value)")] // Non-constant pattern
    public void NonConstantRegexPatternsHandledGracefully(string expression)
    {
        // These should compile but may log warnings (not errors)
        var result = SerilogExpression.TryCompile(expression, out var compiled, out var error);
        
        // These compile successfully but return undefined at runtime
        Assert.True(result);
        Assert.NotNull(compiled);
        Assert.Null(error);
    }

    [Fact]
    public void RegexTimeoutIsRespected()
    {
        // This regex should compile fine - timeout only matters at runtime
        var expression = @"IsMatch(Text, '(a+)+b')";
        
        var result = SerilogExpression.TryCompile(expression, out var compiled, out var error);
        
        Assert.True(result);
        Assert.NotNull(compiled);
        Assert.Null(error);
    }

    [Fact]
    public void ComplexExpressionsReportFirstError()
    {
        var expression = "UnknownFunc1() or Length(Value) > 5";
        var result = SerilogExpression.TryCompile(expression, out var compiled, out var error);
        
        Assert.False(result);
        Assert.Null(compiled);
        Assert.NotNull(error);
        
        // Should report the first error encountered during compilation
        Assert.Contains("UnknownFunc1", error);
    }

    [Fact]
    public void BackwardCompatibilityPreservedForInvalidCiUsage()
    {
        // These previously compiled (CI was silently ignored)
        // They should still compile with the new changes
        var expressions = new[]
        {
            "undefined() ci",
            "null = undefined() ci",
            "Length(Name) ci",
            "Round(Value, 2) ci"
        };

        foreach (var expr in expressions)
        {
            var result = SerilogExpression.TryCompile(expr, out var compiled, out var error);
            Assert.True(result, $"Breaking change detected: {expr} no longer compiles. Error: {error}");
            Assert.NotNull(compiled);
            Assert.Null(error);
        }
    }
}