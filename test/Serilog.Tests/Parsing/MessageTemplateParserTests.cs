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
            CollectionAssert.AreEqual(
                parsed,
                messageTemplateTokens);
        }

        [Test]
        public void AnEmptyMessageIsASingleTextToken()
        {
            var t = Parse("");
            Assert.AreEqual(1, t.Length);
            Assert.IsInstanceOf<TextToken>(t.Single());
        }

        [Test]
        public void AMessageWithoutPropertiesIsASingleTextToken()
        {
            AssertParsedAs("Hello, world!",
                new TextToken("Hello, world!"));
        }

        [Test]
        public void AMessageWithPropertyOnlyIsASinglePropertyToken()
        {
            AssertParsedAs("{Hello}",
                new PropertyToken("Hello", "{Hello}"));
        }

        [Test]
        public void DoubledLeftBracketsAreParsedAsASingleBracket()
        {
            AssertParsedAs("{{ Hi! }",
                new TextToken("{ Hi! }"));
        }

        [Test]
        public void DoubledLeftBracketsAreParsedAsASingleBracketInsideText()
        {
            AssertParsedAs("Well, {{ Hi!",
                new TextToken("Well, { Hi!"));
        }

        [Test]
        public void DoubledRightBracketsAreParsedAsASingleBracket()
        {
            AssertParsedAs("Nice }}-: mo",
                new TextToken("Nice }-: mo"));
        }

        [Test]
        public void AMalformedPropertyTagIsParsedAsText()
        {
            AssertParsedAs("{0 space}",
                new TextToken("{0 space}"));
        }

        [Test]
        public void AnIntegerPropertyNameIsParsedAsPositionalProperty()
        {
            var parsed = (PropertyToken) Parse("{0}").Single();
            Assert.AreEqual("0", parsed.PropertyName);
            Assert.IsTrue(parsed.IsPositional);
        }

        [Test]
        public void FormatsCanContainColons()
        {
            var parsed = (PropertyToken) Parse("{Time:hh:mm}").Single();
            Assert.AreEqual("hh:mm", parsed.Format);
        }

        [Test]
        public void ZeroValuesAlignmentIsParsedAsText()
        {
            AssertParsedAs("{Hello,-0}",
                new TextToken("{Hello,-0}"));

            AssertParsedAs("{Hello,0}",
                new TextToken("{Hello,0}"));
        }

        [Test]
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

        [Test]
        public void EmptyAlignmentIsParsedAsText()
        {
            AssertParsedAs("{Hello,}",
                new TextToken("{Hello,}"));

            AssertParsedAs("{Hello,:format}",
                new TextToken("{Hello,:format}"));
        }

        [Test]
        public void MultipleTokensHasCorrectIndexes()
        {
            AssertParsedAs("{Greeting}, {Name}!",
                new PropertyToken("Greeting", "{Greeting}"),
                new TextToken(", ", 10),
                new PropertyToken("Name", "{Name}", startIndex: 12),
                new TextToken("!", 18));
        }

        [Test]
        public void MissingRightBracketIsParsedAsText()
        {
            AssertParsedAs("{Hello",
                new TextToken("{Hello"));
        }

        [Test]
        public void DestructureHintIsParsedCorrectly()
        {
            var parsed = (PropertyToken)Parse("{@Hello}").Single();
            Assert.AreEqual(Destructuring.Destructure, parsed.Destructuring);
        }

        [Test]
        public void StringifyHintIsParsedCorrectly()
        {
            var parsed = (PropertyToken)Parse("{$Hello}").Single();
            Assert.AreEqual(Destructuring.Stringify, parsed.Destructuring);
        }

        [Test]
        public void DestructuringWithEmptyPropertyNameIsParsedAsText()
        {
            AssertParsedAs("{@}",
                new TextToken("{@}"));
        }

        [Test]
        public void UnderscoresAreValidInPropertyNames()
        {
            AssertParsedAs("{_123_Hello}", new PropertyToken("_123_Hello", "{_123_Hello}")); 
        }

    }
}
