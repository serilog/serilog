using PropertyToken = Serilog.Parsing.PropertyToken;

namespace Serilog.Tests.Parsing;

public class MessageTemplateParserTests
{
    static MessageTemplateToken[] Parse(string messageTemplate)
    {
        return new MessageTemplateParser().Parse(messageTemplate).Tokens.ToArray();
    }

    // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
    static void AssertParsedAs(string message, params MessageTemplateToken[] messageTemplateTokens)
    {
        var parsed = Parse(message);
        Assert.Equal(
            messageTemplateTokens,
            parsed);
    }

    [Fact]
    public void MessageTemplateIsRequired()
    {
        Assert.Throws<ArgumentNullException>(() => Parse(null!));
    }

    [Fact]
    public void AnEmptyMessageIsASingleTextToken()
    {
        var ts = Parse("");
        var mt = Assert.Single(ts);
        var tt = Assert.IsType<TextToken>(mt);
        Assert.Equal("", tt.Text);
        Assert.Equal("", tt.ToString());
        Assert.Equal(0, tt.Length);
    }

    [Fact]
    public void AnEmptyPropertyIsIsParsedAsText()
    {
        var ts = Parse("{}");
        var mt = Assert.Single(ts);
        var tt = Assert.IsType<TextToken>(mt);
        Assert.Equal("{}", tt.Text);
        Assert.Equal("{}", tt.ToString());
        Assert.Equal(2, tt.Length);
    }

    [Fact]
    public void AMessageWithoutPropertiesIsASingleTextToken()
    {
        AssertParsedAs("Hello, world!",
            new TextToken("Hello, world!"));
    }

    [Fact]
    public void AMessageWithPropertyOnlyIsASinglePropertyToken()
    {
        AssertParsedAs("{Hello}",
            new PropertyToken("Hello", "{Hello}"));
    }

    [Fact]
    public void DoubledLeftBracketsAreParsedAsASingleBracket()
    {
        AssertParsedAs("{{ Hi! }",
            new TextToken("{ Hi! }"));
    }

    [Fact]
    public void DoubledLeftBracketsAreParsedAsASingleBracketInsideText()
    {
        AssertParsedAs("Well, {{ Hi!",
            new TextToken("Well, { Hi!"));

        AssertParsedAs("Hello, {{worl@d}!",
            new TextToken("Hello, {worl@d}!"));
    }

    [Fact]
    public void DoubledRightBracketsAreParsedAsASingleBracket()
    {
        AssertParsedAs("Nice }}-: mo",
            new TextToken("Nice }-: mo"));
    }

    [Fact]
    public void DoubledRightBracketsAfterOneLeftIsParsedAPropertyTokenAndATextToken()
    {
        AssertParsedAs("{World}}!",
            new PropertyToken("World", "{World}"), new TextToken("}!"));

        AssertParsedAs("Hello, {World}}!",
            new TextToken("Hello, "), new PropertyToken("World", "{World}"), new TextToken("}!"));
    }

    [Fact]
    public void DoubledBracketsAreParsedAsASingleBracket()
    {
        AssertParsedAs("{{Hi}}",
            new TextToken("{Hi}"));

        AssertParsedAs("Hello, {{worl@d}}!",
            new TextToken("Hello, {worl@d}!"));
    }

    [Theory]
    [InlineData("{test.name}")]
    [InlineData("{te.st.na.me}")]
    public void DashedAndDottedNamesAreAccepted(string template)
    {
        var parser = new MessageTemplateParser();
        var token = Assert.Single(parser.Parse(template).Tokens);
        var propertyToken = Assert.IsType<PropertyToken>(token);
        var expected = template.Trim('{', '}');
        Assert.Equal(expected, propertyToken.PropertyName);
        Assert.Null(propertyToken.Alignment);
        Assert.Null(propertyToken.Format);
        Assert.False(propertyToken.IsPositional);
    }

    [Theory]
    [InlineData("{0 space}")]
    [InlineData("{0 space")]
    [InlineData("{0_space")]
    [InlineData("{0_{{space}")]
    [InlineData("{0_{{space")]
    [InlineData("{.testname}")]
    [InlineData("{testname.}")]
    [InlineData("{.}")]
    [InlineData("{..}")]
    [InlineData("{test..name}")]
    [InlineData("{10.name}")]
    [InlineData("{0_}")]
    [InlineData("{0a}")]
    public void AMalformedPropertyTagIsParsedAsText(string template)
    {
        AssertParsedAs(template, new TextToken(template));
    }

    [Fact]
    public void ATrailingUnmatchedBracketIsParsedAsText()
    {
        AssertParsedAs("{0}}space}", new PropertyToken("0", "{0}"), new TextToken("}space}"));
    }

    [Fact]
    public void AMessageWithAMalformedPropertyTagIsParsedAsManyTextTokens()
    {
        AssertParsedAs("Hello, {w@rld}",
            new TextToken("Hello, "), new TextToken("{w@rld}"));

        AssertParsedAs("Hello, {w@rld",
            new TextToken("Hello, "), new TextToken("{w@rld"));

        AssertParsedAs("Hello, {w{{rld",
            new TextToken("Hello, "), new TextToken("{w{{rld"));

        AssertParsedAs("Hello{{, {w{{rld",
            new TextToken("Hello{, "), new TextToken("{w{{rld"));

        AssertParsedAs("Hello, {w@rld}, HI!",
            new TextToken("Hello, "), new TextToken("{w@rld}"), new TextToken(", HI!"));

        AssertParsedAs("{w@rld} Hi!",
            new TextToken("{w@rld}"), new TextToken(" Hi!"));

        AssertParsedAs("{H&llo}, {w@rld}",
            new TextToken("{H&llo}"), new TextToken(", "), new TextToken("{w@rld}"));
    }

    [Fact]
    public void ASingleIntegerPropertyNameIsParsedAsPositionalProperty()
    {
        var parsed = (PropertyToken)Parse("{0}").Single();
        Assert.Equal("0", parsed.PropertyName);
        Assert.Equal("{0}", parsed.RawText);
        Assert.Equal(parsed.RawText, parsed.ToString());
        Assert.True(parsed.IsPositional);
    }

    [Fact]
    public void ManyIntegerPropertyNameIsParsedAsPositionalProperty()
    {
        var parsed = Parse("{0}, {1}, {2}");

        var prop1 = (PropertyToken)parsed[0];
        Assert.Equal("0", prop1.PropertyName);
        Assert.Equal("{0}", prop1.RawText);
        Assert.True(prop1.IsPositional);
        Assert.Equal(3, prop1.Length);

        var prop2 = (TextToken)parsed[1];
        Assert.Equal(", ", prop2.Text);
        Assert.Equal(2, prop2.Length);

        var prop3 = (PropertyToken)parsed[2];
        Assert.Equal("1", prop3.PropertyName);
        Assert.Equal("{1}", prop3.RawText);
        Assert.True(prop3.IsPositional);
        Assert.Equal(3, prop3.Length);

        var prop4 = (TextToken)parsed[3];
        Assert.Equal(", ", prop4.Text);
        Assert.Equal(2, prop4.Length);

        var prop5 = (PropertyToken)parsed[4];
        Assert.Equal("2", prop5.PropertyName);
        Assert.Equal("{2}", prop5.RawText);
        Assert.True(prop5.IsPositional);
        Assert.Equal(3, prop5.Length);
    }

    [Fact]
    public void InvalidIntegerPropertyNameIsParsedAsText()
    {
        var parsed = Parse("{-1}{-0}{0}{1}{3.1415}");

        var prop1 = (TextToken)parsed[0];
        Assert.Equal("{-1}", prop1.Text);

        var prop2 = (TextToken)parsed[1];
        Assert.Equal("{-0}", prop2.Text);

        var prop3 = (PropertyToken)parsed[2];
        Assert.Equal("0", prop3.PropertyName);
        Assert.Equal("{0}", prop3.RawText);
        Assert.True(prop3.IsPositional);

        var prop4 = (PropertyToken)parsed[3];
        Assert.Equal("1", prop4.PropertyName);
        Assert.Equal("{1}", prop4.RawText);
        Assert.True(prop4.IsPositional);

        var prop5 = (TextToken)parsed[4];
        Assert.Equal("{3.1415}", prop5.Text);
    }

    [Fact]
    public void FormatsCanContainColons()
    {
        var parsed = (PropertyToken)Parse("{Time:hh:mm}").Single();
        Assert.Equal("hh:mm", parsed.Format);
    }

    [Fact]
    public void FormatsCanContainSymbolsAndOperators()
    {
        AssertParsedAs("{Hello:HH$MM}",
            new PropertyToken("Hello", "{Hello:HH$MM}", "HH$MM"));

        AssertParsedAs("{Hello:HH<MM}",
            new PropertyToken("Hello", "{Hello:HH<MM}", "HH<MM"));

        AssertParsedAs("{Hello:HH#MM}",
            new PropertyToken("Hello", "{Hello:HH#MM}", "HH#MM"));

        AssertParsedAs("{Hello:HH+MM}",
            new PropertyToken("Hello", "{Hello:HH+MM}", "HH+MM"));

        AssertParsedAs("{Hello:HH`MM}",
            new PropertyToken("Hello", "{Hello:HH`MM}", "HH`MM"));
    }

    [Fact]
    public void PropertiesCanHaveLeftAlignment()
    {
        var prop1 = (PropertyToken)Parse("{Hello,-5}").Single();
        Assert.Equal("Hello", prop1.PropertyName);
        Assert.Equal("{Hello,-5}", prop1.RawText);
        Assert.Equal(new Alignment(AlignmentDirection.Left, 5), prop1.Alignment);

        var prop2 = (PropertyToken)Parse("{Hello,-50}").Single();
        Assert.Equal("Hello", prop2.PropertyName);
        Assert.Equal("{Hello,-50}", prop2.RawText);
        Assert.Equal(new Alignment(AlignmentDirection.Left, 50), prop2.Alignment);
    }

    [Fact]
    public void PropertiesCanHaveRightAlignment()
    {
        var prop1 = (PropertyToken)Parse("{Hello,5}").Single();
        Assert.Equal("Hello", prop1.PropertyName);
        Assert.Equal("{Hello,5}", prop1.RawText);
        Assert.Equal(new Alignment(AlignmentDirection.Right, 5), prop1.Alignment);

        var prop2 = (PropertyToken)Parse("{Hello,50}").Single();
        Assert.Equal("Hello", prop2.PropertyName);
        Assert.Equal("{Hello,50}", prop2.RawText);
        Assert.Equal(new Alignment(AlignmentDirection.Right, 50), prop2.Alignment);
    }

    [Fact]
    public void PropertiesCanHaveAlignmentAndFormat()
    {
        var prop1 = (PropertyToken)Parse("{Hello,-5:000}").Single();
        Assert.Equal("Hello", prop1.PropertyName);
        Assert.Equal("{Hello,-5:000}", prop1.RawText);
        Assert.Equal("000", prop1.Format);
        Assert.Equal(new Alignment(AlignmentDirection.Left, 5), prop1.Alignment);

        var prop2 = (PropertyToken)Parse("{Hello,-50:000}").Single();
        Assert.Equal("Hello", prop2.PropertyName);
        Assert.Equal("{Hello,-50:000}", prop2.RawText);
        Assert.Equal("000", prop2.Format);
        Assert.Equal(new Alignment(AlignmentDirection.Left, 50), prop2.Alignment);

        var prop3 = (PropertyToken)Parse("{Hello,5:000}").Single();
        Assert.Equal("Hello", prop3.PropertyName);
        Assert.Equal("{Hello,5:000}", prop3.RawText);
        Assert.Equal("000", prop3.Format);
        Assert.Equal(new Alignment(AlignmentDirection.Right, 5), prop3.Alignment);

        var prop4 = (PropertyToken)Parse("{Hello,50:000}").Single();
        Assert.Equal("Hello", prop4.PropertyName);
        Assert.Equal("{Hello,50:000}", prop4.RawText);
        Assert.Equal("000", prop4.Format);
        Assert.Equal(new Alignment(AlignmentDirection.Right, 50), prop4.Alignment);
    }

    [Fact]
    public void FormatInFrontOfAlignmentWillHaveTheAlignmentBeConsideredPartOfTheFormat()
    {
        var prop1 = (PropertyToken)Parse("{Hello:000,-5}").Single();
        Assert.Equal("Hello", prop1.PropertyName);
        Assert.Equal("{Hello:000,-5}", prop1.RawText);
        Assert.Equal("000,-5", prop1.Format);
        Assert.Null(prop1.Alignment);

        var prop2 = (PropertyToken)Parse("{Hello:000,-50}").Single();
        Assert.Equal("Hello", prop2.PropertyName);
        Assert.Equal("{Hello:000,-50}", prop2.RawText);
        Assert.Equal("000,-50", prop2.Format);
        Assert.Null(prop2.Alignment);

        var prop3 = (PropertyToken)Parse("{Hello:000,5}").Single();
        Assert.Equal("Hello", prop3.PropertyName);
        Assert.Equal("{Hello:000,5}", prop3.RawText);
        Assert.Equal("000,5", prop3.Format);
        Assert.Null(prop3.Alignment);

        var prop4 = (PropertyToken)Parse("{Hello:000,50}").Single();
        Assert.Equal("Hello", prop4.PropertyName);
        Assert.Equal("{Hello:000,50}", prop4.RawText);
        Assert.Equal("000,50", prop4.Format);
        Assert.Null(prop4.Alignment);
    }

    [Fact]
    public void ZeroValuesAlignmentIsParsedAsProperty()
    {
       AssertParsedAs("{Hello,-0}",
           new PropertyToken("Hello", "{Hello,-0}", alignment: new Alignment(AlignmentDirection.Right, 0)));

       AssertParsedAs("{Hello,0}",
           new PropertyToken("Hello", "{Hello,0}", alignment: new Alignment(AlignmentDirection.Left, 0)));
    }

    [Fact]
    public void AlignmentWithPositiveSignParsedAsText()
    {
        AssertParsedAs("{Hello,+10}",
            new TextToken("{Hello,+10}"));
    }

    [Fact]
    public void NonNumberAlignmentIsParsedAsText()
    {
        AssertParsedAs("{Hello,-aa}",
            new TextToken("{Hello,-aa}"));

        AssertParsedAs("{Hello,aa}",
            new TextToken("{Hello,aa}"));

        AssertParsedAs("{Hello,-10-1}",
            new TextToken("{Hello,-10-1}"));

        AssertParsedAs("{Hello,10-1}",
            new TextToken("{Hello,10-1}"));
    }

    [Fact]
    public void EmptyAlignmentIsParsedAsText()
    {
        AssertParsedAs("{Hello,}",
            new TextToken("{Hello,}"));

        AssertParsedAs("{Hello,:format}",
            new TextToken("{Hello,:format}"));
    }

    [Fact]
    public void MultipleTokensHasCorrectIndexes()
    {
        AssertParsedAs("{Greeting}, {Name}!",
            new PropertyToken("Greeting", "{Greeting}"),
            new TextToken(", "),
            new PropertyToken("Name", "{Name}"),
            new TextToken("!"));
    }

    [Fact]
    public void MissingRightBracketIsParsedAsText()
    {
        AssertParsedAs("{Hello",
            new TextToken("{Hello"));
    }

    [Fact]
    public void DestructureHintIsParsedCorrectly()
    {
        var parsed = (PropertyToken)Parse("{@Hello}").Single();
        Assert.Equal(Destructuring.Destructure, parsed.Destructuring);
    }

    [Fact]
    public void StringifyHintIsParsedCorrectly()
    {
        var parsed = (PropertyToken)Parse("{$Hello}").Single();
        Assert.Equal(Destructuring.Stringify, parsed.Destructuring);
    }

    [Fact]
    public void DestructureWithInvalidHintsIsParsedAsText()
    {
        AssertParsedAs("{@$}",
            new TextToken("{@$}"));

        AssertParsedAs("{$@}",
            new TextToken("{$@}"));
    }

    [Fact]
    public void DestructuringWithEmptyPropertyNameIsParsedAsText()
    {
        AssertParsedAs("{@}",
            new TextToken("{@}"));

        AssertParsedAs("{$}",
            new TextToken("{$}"));
    }

    [Fact]
    public void UnderscoresAreValidInPropertyNames()
    {
        AssertParsedAs("{_123_Hello}", new PropertyToken("_123_Hello", "{_123_Hello}"));
    }

    [Fact]
    public void IndexOutOfRangeExceptionBugHasNotRegressed()
    {
        var parser = new MessageTemplateParser();
        parser.Parse("{,,}");
    }

    [Fact]
    public void FormatCanContainMultipleSections()
    {
        var parsed = (PropertyToken)Parse("{Number:##.0;-##.0;zero}").Single();
        Assert.Equal("##.0;-##.0;zero", parsed.Format);
    }

    [Fact]
    public void FormatCanContainPlusSign()
    {
        var parsed = (PropertyToken)Parse("{Number:+##.0}").Single();
        Assert.Equal("+##.0", parsed.Format);
    }
}
