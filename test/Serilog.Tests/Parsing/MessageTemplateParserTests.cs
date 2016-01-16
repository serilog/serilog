using System.Linq;
using Xunit;
using Serilog.Parsing;

namespace Serilog.Tests.Parsing
{
    public class MessageTemplateParserTests
    {
        static MessageTemplateToken[] Parse(string messsageTemplate)
        {
            return new MessageTemplateParser().Parse(messsageTemplate).Tokens.ToArray();
        }

        static void AssertParsedAs(string message, params MessageTemplateToken[] messageTemplateTokens)
        {
            var parsed = Parse(message);
            Assert.Equal(
                parsed,
                messageTemplateTokens);
        }

        [Fact]
        public void AnEmptyMessageIsASingleTextToken()
        {
            var t = Parse("");
            Assert.Equal(1, t.Length);
            Assert.IsType<TextToken>(t.Single());
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
        }

        [Fact]
        public void DoubledRightBracketsAreParsedAsASingleBracket()
        {
            AssertParsedAs("Nice }}-: mo",
                new TextToken("Nice }-: mo"));
        }

        [Fact]
        public void AMalformedPropertyTagIsParsedAsText()
        {
            AssertParsedAs("{0 space}",
                new TextToken("{0 space}"));
        }

        [Fact]
        public void AnIntegerPropertyNameIsParsedAsPositionalProperty()
        {
            var parsed = (PropertyToken) Parse("{0}").Single();
            Assert.Equal("0", parsed.PropertyName);
            Assert.True(parsed.IsPositional);
        }

        [Fact]
        public void FormatsCanContainColons()
        {
            var parsed = (PropertyToken) Parse("{Time:hh:mm}").Single();
            Assert.Equal("hh:mm", parsed.Format);
        }

        [Fact]
        public void ZeroValuesAlignmentIsParsedAsText()
        {
            AssertParsedAs("{Hello,-0}",
                new TextToken("{Hello,-0}"));

            AssertParsedAs("{Hello,0}",
                new TextToken("{Hello,0}"));
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
                new TextToken(", ", 10),
                new PropertyToken("Name", "{Name}", startIndex: 12),
                new TextToken("!", 18));
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
        public void DestructuringWithEmptyPropertyNameIsParsedAsText()
        {
            AssertParsedAs("{@}",
                new TextToken("{@}"));
        }

        [Fact]
        public void UnderscoresAreValidInPropertyNames()
        {
            AssertParsedAs("{_123_Hello}", new PropertyToken("_123_Hello", "{_123_Hello}")); 
        }

    }
}
