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
            return MessageTemplateParser.Parse(messsageTemplate).ToArray();
        }

        private static void AssertParsedAs(string message, params MessageTemplateToken[] messageTemplateTokens)
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
                new LogEventPropertyToken("Hello", "{Hello}"));
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
            var parsed = (LogEventPropertyToken) Parse("{0}").Single();
            Assert.AreEqual("0", parsed.PropertyName);
            Assert.IsTrue(parsed.IsPositional);
        }

        [Test]
        public void FormatsCanContainColons()
        {
            var parsed = (LogEventPropertyToken) Parse("{Time:hh:mm}").Single();
            Assert.AreEqual("hh:mm", parsed.Format);
        }
    }
}
