using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Serilog.Core;
using Serilog.Parsing;

namespace Serilog.Tests.Core
{
    [TestFixture]
    public class MessageTemplateTests
    {
        class Chair
        {
            // ReSharper disable UnusedMember.Local
            public string Back { get { return "straight"; } }
            public int[] Legs { get { return new[] { 1, 2, 3, 4 }; } }
            // ReSharper restore UnusedMember.Local
            public override string ToString()
            {
                return "a chair";
            }
        }

        [Test]
        public void AnObjectIsRenderedInSimpleNotation()
        {
            var m = Render("I sat at {@Chair}", new Chair());
            Assert.AreEqual("I sat at Chair { Back: \"straight\", Legs: [1, 2, 3, 4] }", m);
        }

        [Test]
        public void AnAnonymousObjectIsRenderedInSimpleNotationWithoutType()
        {
            var m = Render("I sat at {@Chair}", new { Back = "straight", Legs = new[] { 1, 2, 3, 4 } });
            Assert.AreEqual("I sat at { Back: \"straight\", Legs: [1, 2, 3, 4] }", m);
        }

        [Test]
        public void AnObjectWithDefaultDestructuringIsRenderedAsALiteral()
        {
            var m = Render("I sat at {Chair}", new Chair());
            Assert.AreEqual("I sat at a chair", m);
        }

        [Test]
        public void AnObjectWithStringifyDestructuringIsRenderedAsAString()
        {
            var m = Render("I sat at {$Chair}", new Chair());
            Assert.AreEqual("I sat at \"a chair\"", m);
        }

        [Test]
        public void MultiplePropertiesAreRenderedInOrder()
        {
            var m = Render("Just biting {Fruit} number {Count}", "Apple", 12);
            Assert.AreEqual("Just biting \"Apple\" number 12", m);
        }

        [Test]
        public void FormatStringsArePropagated()
        {
            var m = Render("Welcome, customer {CustomerId:0000}", 12);
            Assert.AreEqual("Welcome, customer 0012", m);
        }

        static string Render(string messageTemplate, params object[] properties)
        {
            var mt = new MessageTemplate(MessageTemplateParser.Parse(messageTemplate));
            var props = mt.ConstructPositionalProperties(properties);
            var output = new StringBuilder();
            var writer = new StringWriter(output);
            mt.Render(props.ToDictionary(p => p.Name), writer);
            writer.Flush();
            return output.ToString();
        }
    }
}
