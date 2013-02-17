using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Opi.Parsing;

namespace Opi.Tests.Parsing
{
    [TestClass]
    public class MessageTemplateParserTests
    {
        static MessageTemplateToken[] Parse(string messsageTemplate)
        {
            return MessageTemplateTokenParser.Parse(messsageTemplate).ToArray();
        }

        private static void AssertParsedAs(string message, params MessageTemplateToken[] messageTemplateTokens)
        {
            var parsed = Parse(message);
            CollectionAssert.AreEqual(
                parsed,
                messageTemplateTokens);
        }

        [TestMethod]
        public void AnEmptyMessageIsASingleTextToken()
        {
            var t = Parse("");
            Assert.AreEqual(1, t.Length);
            Assert.IsInstanceOfType(t.Single(), typeof(TextToken));
        }

        [TestMethod]
        public void AMessageWithoutPropertiesIsASingleTextToken()
        {
            AssertParsedAs("Hello, world!",
                new TextToken("Hello, world!"));
        }

        [TestMethod]
        public void AMessageWithPropertyOnlyIsASinglePropertyToken()
        {
            AssertParsedAs("{Hello}",
                new LogEventPropertyToken("Hello", "{Hello}"));
        }

        [TestMethod]
        public void DoubledLeftBracketsAreParsedAsASingleBracket()
        {
            AssertParsedAs("{{ Hi! }",
                new TextToken("{ Hi! }"));
        }

        [TestMethod]
        public void DoubledLeftBracketsAreParsedAsASingleBracketInsideText()
        {
            AssertParsedAs("Well, {{ Hi!",
                new TextToken("Well, { Hi!"));
        }

        [TestMethod]
        public void DoubledRightBracketsAreParsedAsASingleBracket()
        {
            AssertParsedAs("Nice }}-: mo",
                new TextToken("Nice }-: mo"));
        }

        // Internal details of the parser leak out here, ideally
        // we'd combine these into a single text token.
        [TestMethod]
        public void AMalformedPropertyTagIsParsedAsText()
        {
            AssertParsedAs("{0 space}",
                new TextToken("{0"),
                new TextToken(" space}"));
        }
    }
}
