using Serilog.Templates;
using Serilog.Templates.Ast;
using Serilog.Templates.Parsing;
using Xunit;

namespace Serilog.Expressions.Tests;

public class TemplateParserTests
{
    [Theory]
    [InlineData("Trailing {", "Syntax error: unexpected end of input, expected expression.")]
    [InlineData("Lonely } bracket", "Syntax error (line 1, column 9): unexpected space, expected escaped `}`.")]
    [InlineData("Trailing }", "Syntax error: unexpected end of input, expected escaped `}`.")]
    [InlineData("Unclosed {hole", "Syntax error: unexpected end of input, expected `}`.")]
    [InlineData("Syntax {+Err}or", "Syntax error (line 1, column 9): unexpected operator `+`, expected expression.")]
    [InlineData("Syntax {1 + 2 and}or", "Syntax error (line 1, column 18): unexpected `}`, expected expression.")]
    [InlineData("Missing {Align,-} digits", "Syntax error (line 1, column 17): unexpected `}`, expected number.")]
    [InlineData("Non-digit {Align,x} specifier", "Syntax error (line 1, column 18): unexpected identifier `x`, expected alignment and width.")]
    [InlineData("Empty {Align,} digits", "Syntax error (line 1, column 14): unexpected `}`, expected alignment and width.")]
    public void ErrorsAreReported(string input, string error)
    {
        Assert.False(ExpressionTemplate.TryParse(input, null, null, null, false, null, out _, out var actual));
        Assert.Equal(error, actual);
    }

    [Fact]
    public void DefaultAlignmentIsNull()
    {
        var parser = new TemplateParser();
        Assert.True(parser.TryParse("{x}", out var template, out _));
        var avt = Assert.IsType<FormattedExpression>(template);
        Assert.Null(avt.Alignment);
    }
}