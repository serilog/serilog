using System.Linq;
using NUnit.Framework;
using Serilog.Parsing;

namespace Serilog.Tests.Parsing
{
    [TestFixture]
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
                new TextToken("Hello, world!", 0, 13));
        }

        [Test]
        public void AMessageWithPropertyOnlyIsASinglePropertyToken()
        {
            AssertParsedAs("{Hello}",
                new PropertyToken("Hello", "{Hello}", 0, 7));
        }

        [Test]
        public void DoubledLeftBracketsAreParsedAsASingleBracket()
        {
            AssertParsedAs("{{ Hi! }",
                new TextToken("{ Hi! }", 0, 8));
        }

        [Test]
        public void DoubledLeftBracketsAreParsedAsASingleBracketInsideText()
        {
            AssertParsedAs("Well, {{ Hi!",
                new TextToken("Well, { Hi!", 0, 12));
        }

        [Test]
        public void DoubledRightBracketsAreParsedAsASingleBracket()
        {
            AssertParsedAs("Nice }}-: mo",
                new TextToken("Nice }-: mo", 0, 12));
        }

        [Test]
        public void AMalformedPropertyTagIsParsedAsText()
        {
            AssertParsedAs("{0 space}",
                new TextToken("{0 space}", 0, 9));
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
                new TextToken("{Hello,-0}", 0, 10));

            AssertParsedAs("{Hello,0}",
                new TextToken("{Hello,0}", 0, 9));
        }

        [Test]
        public void NonNumberAlignmentIsParsedAsText()
        {
            AssertParsedAs("{Hello,-aa}",
                new TextToken("{Hello,-aa}", 0, 11));

            AssertParsedAs("{Hello,aa}",
                new TextToken("{Hello,aa}", 0, 10));

            AssertParsedAs("{Hello,-10-1}",
                new TextToken("{Hello,-10-1}", 0, 13));

            AssertParsedAs("{Hello,10-1}",
                new TextToken("{Hello,10-1}", 0, 12));
        }

        [Test]
        public void EmptyAlignmentIsParsedAsText()
        {
            AssertParsedAs("{Hello,}",
                new TextToken("{Hello,}", 0, 8));

            AssertParsedAs("{Hello,:format}",
                new TextToken("{Hello,:format}", 0, 15));
        }

        [Test]
        public void MultipleTokensHasCorrectIndexes()
        {
            AssertParsedAs("{Greeting}, {Name}!",
                new PropertyToken("Greeting", "{Greeting}", 0, 10),
                new TextToken(", ", 10, 12),
                new PropertyToken("Name", "{Name}", 12, 18),
                new TextToken("!", 18, 19));
        }

        [Test]
        public void MissingRightBracketIsParsedAsText()
        {
            AssertParsedAs("{Hello",
                new TextToken("{Hello", 0, 6));
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
                new TextToken("{@}", 0, 3));
        }
    }
}
