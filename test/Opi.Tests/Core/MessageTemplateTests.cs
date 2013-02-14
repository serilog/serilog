using System.IO;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Opi.Core;
using Opi.Parsing;

namespace Opi.Tests.Core
{
    [TestClass]
    public class MessageTemplateTests
    {
        class Chair
        {
            public string Back { get { return "straight"; } }
            public int[] Legs { get { return new[] { 1, 2, 3, 4 }; } }
            public override string ToString()
            {
                return "a chair";
            }
        }

        [TestMethod]
        public void AnObjectIsRenderedInSimpleNotation()
        {
            var m = Render("I sat at {Chair}", new Chair());
            Assert.AreEqual("I sat at Chair { Back: \"straight\", Legs: [1, 2, 3, 4] }", m);
        }

        [TestMethod]
        public void AnAnonymousObjectIsRenderedInSimpleNotationWithoutType()
        {
            var m = Render("I sat at {Chair}", new { Back = "straight", Legs = new[] { 1, 2, 3, 4 } });
            Assert.AreEqual("I sat at { Back: \"straight\", Legs: [1, 2, 3, 4] }", m);
        }

        [TestMethod]
        public void AnObjectWithStringifyDestructuringIsRenderedAsAString()
        {
            var m = Render("I sat at {Chair:s}", new Chair());
            Assert.AreEqual("I sat at \"a chair\"", m);
        }


        [TestMethod]
        public void MultiplePropertiesAreRenderedInOrder()
        {
            var m = Render("Just biting {Fruit} number {Count}", "Apple", 12);
            Assert.AreEqual("Just biting \"Apple\" number 12", m);
        }

        [TestMethod]
        public void FormatStringsArePropagated()
        {
            var m = Render("Welcome, customer {CustomerId:0000}", 12);
            Assert.AreEqual("Welcome, customer 0012", m);
        }

        static string Render(string messageTemplate, params object[] properties)
        {
            var mt = new MessageTemplate(MessageTemplateTokenParser.Parse(messageTemplate));
            var props = mt.ConstructPositionalProperties(properties);
            var output = new StringBuilder();
            var writer = new StringWriter(output);
            mt.Render(props.ToDictionary(p => p.Name), writer);
            writer.Flush();
            return output.ToString();
        }
    }
}
