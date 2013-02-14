using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Opi.Tests.Parsing
{
    [TestClass]
    public class MessageTemplateParserTests
    {
        [TestMethod]
        public void AnEmptyMessageIsASingleTextToken()
        {
        }

        [TestMethod]
        public void AMessageWithoutPropertiesIsASingleTextToken()
        {
        }

        [TestMethod]
        public void AMessageWithPropertyOnlyIsASinglePropertyToken()
        {
        }

        [TestMethod]
        public void DoubledLeftBracketsAreParsedAsASingleBracket()
        {
        }

        [TestMethod]
        public void DoubledRightBracketsAreParsedAsASingleBracket()
        {
        }

        [TestMethod]
        public void AMalformedPropertyTagIsParsedAsText()
        {
        }
    }
}
