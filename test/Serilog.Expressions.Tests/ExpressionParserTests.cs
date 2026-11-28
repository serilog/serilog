using Serilog.Expressions.Parsing;
using Xunit;

namespace Serilog.Expressions.Tests;

public class ExpressionParserTests
{
    [Theory]
    [InlineData("contains(@Message, 'some text')", "contains(@Message, 'some text')")]
    [InlineData("AProperty", null)]
    [InlineData("AProperty = 'some text'", "_Internal_Equal(AProperty, 'some text')")]
    [InlineData("AProperty = 42", "_Internal_Equal(AProperty, 42)")]
    [InlineData("@Properties['0'] = 42", "_Internal_Equal(@Properties['0'], 42)")]
    [InlineData("AProperty = null", "_Internal_Equal(AProperty, null)")]
    [InlineData("AProperty <> 42", "_Internal_NotEqual(AProperty, 42)")]
    [InlineData("has(AProperty)", null)]
    [InlineData("not A", "_Internal_Not(A)")]
    [InlineData("not (1 = 2)", "_Internal_Not(_Internal_Equal(1, 2))")]
    [InlineData("not(1 = 2)", "_Internal_Not(_Internal_Equal(1, 2))")]
    [InlineData("AProperty = 3 and Another < 12", "_Internal_And(_Internal_Equal(AProperty, 3), _Internal_LessThan(Another, 12))")]
    [InlineData("@Timestamp", null)]
    [InlineData("1 + (2 + 3) * 4", "_Internal_Add(1, _Internal_Multiply(_Internal_Add(2, 3), 4))")]
    [InlineData("AProperty.Another = 3", "_Internal_Equal(AProperty.Another, 3)")]
    [InlineData("AProperty /2/3 = 7", "_Internal_Equal(_Internal_Divide(_Internal_Divide(AProperty, 2), 3), 7)")]
    [InlineData("AProperty[0] = 1", "_Internal_Equal(AProperty[0], 1)")]
    [InlineData("AProperty[0] = note", "_Internal_Equal(AProperty[0], note)")] // Ensure correct tokenization of 'not'
    [InlineData("equal(AProperty[0].Description, 1)", null)]
    [InlineData("equal(AProperty[?].Description, 1)", null)]
    [InlineData("equal(AProperty[*].Description, 1)", null)]
    [InlineData("equal(AProperty[ * ].Description, 1)", "equal(AProperty[*].Description, 1)")]
    [InlineData("AProperty like '%foo'", "_Internal_Like(AProperty, '%foo')")]
    [InlineData("AProperty not like '%foo'", "_Internal_NotLike(AProperty, '%foo')")]
    [InlineData("A is null", "_Internal_IsNull(A)")]
    [InlineData("A IS NOT NULL", "_Internal_IsNotNull(A)")]
    [InlineData("A is not null or B", "_Internal_Or(_Internal_IsNotNull(A), B)")]
    [InlineData("@EventType = 0xC0ffee", "_Internal_Equal(@EventType, 12648430)")]
    [InlineData("@Level in ['Error', 'Warning']", "_Internal_In(@Level, ['Error', 'Warning'])")]
    [InlineData("5 not in [1, 2]", "_Internal_NotIn(5, [1, 2])")]
    [InlineData("1+1", "_Internal_Add(1, 1)")]
    [InlineData("'te\nst'", null)]
    [InlineData("A.B is null", "_Internal_IsNull(A.B)")]
    public void ValidSyntaxIsAccepted(string input, string? expected = null)
    {
        var expressionParser = new ExpressionParser();
        var roundTrip = expressionParser.Parse(input).ToString();
        Assert.Equal(expected ?? input, roundTrip);
    }

    [Theory]
    [InlineData(" ")]
    [InlineData("\"Hello!\"")]
    [InlineData("@\"Hello!\"")]
    [InlineData("$FE6789E6")]
    [InlineData("AProperty == 'some text'")]
    [InlineData("AProperty != 42")]
    [InlineData("!(1 == 2)")]
    [InlineData("AProperty = 3 && Another < 12")]
    [InlineData("A = 99999999999999999999999999999999999999999999")]
    [InlineData("A = 0x99999999999999999999999999999999999999999999")]
    public void InvalidSyntaxIsRejected(string input)
    {
        var expressionParser = new ExpressionParser();
        Assert.Throws<ArgumentException>(() => expressionParser.Parse(input));
    }

    [Theory]
    [InlineData("A = 'b", "Syntax error: unexpected end of input, expected `'`.")]
    [InlineData("A or B) and C", "Syntax error (line 1, column 7): unexpected `)`.")]
    [InlineData("A lik3 C", "Syntax error (line 1, column 3): unexpected identifier `lik3`.")]
    [InlineData("A > 1234f", "Syntax error (line 1, column 9): unexpected `f`, expected digit.")]
    public void PreciseErrorsAreReported(string input, string expectedMessage)
    {
        var expressionParser = new ExpressionParser();
        var ex = Assert.Throws<ArgumentException>(() => expressionParser.Parse(input));
        Assert.Equal(expectedMessage, ex.Message);
    }
}