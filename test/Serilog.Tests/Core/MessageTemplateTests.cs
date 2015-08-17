using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;
using Serilog.Core;
using Serilog.Parameters;
using MessageTemplateParser = Serilog.Parsing.MessageTemplateParser;

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

        class Receipt
        {
            // ReSharper disable UnusedMember.Local
            public decimal Sum { get { return 12.345m; } }
            public DateTime When { get { return new DateTime(2013, 5, 20, 16, 39, 0); } }
            // ReSharper restore UnusedMember.Local
            public override string ToString()
            {
                return "a receipt";
            }
        }

        [Test]
        public void AnObjectIsRenderedInSimpleNotation()
        {
            var m = Render("I sat at {@Chair}", new Chair());
            Assert.AreEqual("I sat at Chair { Back: \"straight\", Legs: [1, 2, 3, 4] }", m);
        }

        [Test]
        public void AnObjectIsRenderedInSimpleNotationUsingFormatProvider()
        {
            var m = Render(CultureInfo.GetCultureInfo("fr-FR"), "I received {@Receipt}", new Receipt());
            Assert.AreEqual("I received Receipt { Sum: 12,345, When: 20/05/2013 16:39:00 }", m);
        }

        [Test]
        public void AnAnonymousObjectIsRenderedInSimpleNotationWithoutType()
        {
            var m = Render("I sat at {@Chair}", new { Back = "straight", Legs = new[] { 1, 2, 3, 4 } });
            Assert.AreEqual("I sat at { Back: \"straight\", Legs: [1, 2, 3, 4] }", m);
        }

        [Test]
        public void AnAnonymousObjectIsRenderedInSimpleNotationWithoutTypeUsingFormatProvider()
        {
            var m = Render(CultureInfo.GetCultureInfo("fr-FR"), "I received {@Receipt}", new { Sum = 12.345, When = new DateTime(2013, 5, 20, 16, 39, 0) });
            Assert.AreEqual("I received { Sum: 12,345, When: 20/05/2013 16:39:00 }", m);
        }

        [Test]
        public void AnObjectWithDefaultDestructuringIsRenderedAsAStringLiteral()
        {
            var m = Render("I sat at {Chair}", new Chair());
            Assert.AreEqual("I sat at \"a chair\"", m);
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
        public void MultiplePropertiesUseFormatProvider()
        {
            var m = Render(CultureInfo.GetCultureInfo("fr-FR"), "Income was {Income} at {Date:d}", 1234.567, new DateTime(2013, 5, 20));
            Assert.AreEqual("Income was 1234,567 at 20/05/2013", m);
        }

        [Test]
        public void FormatStringsArePropagated()
        {
            var m = Render("Welcome, customer {CustomerId:0000}", 12);
            Assert.AreEqual("Welcome, customer 0012", m);
        }

        [TestCase("Welcome, customer #{CustomerId,-10}, pleasure to see you", ExpectedResult = "Welcome, customer #1234      , pleasure to see you")]
        [TestCase("Welcome, customer #{CustomerId,-10:000000}, pleasure to see you", ExpectedResult = "Welcome, customer #001234    , pleasure to see you")]
        [TestCase("Welcome, customer #{CustomerId,10}, pleasure to see you", ExpectedResult = "Welcome, customer #      1234, pleasure to see you")]
        [TestCase("Welcome, customer #{CustomerId,10:000000}, pleasure to see you", ExpectedResult = "Welcome, customer #    001234, pleasure to see you")]
        [TestCase("Welcome, customer #{CustomerId,10:0,0}, pleasure to see you", ExpectedResult = "Welcome, customer #     1,234, pleasure to see you")]
        [TestCase("Welcome, customer #{CustomerId:0,0}, pleasure to see you", ExpectedResult = "Welcome, customer #1,234, pleasure to see you")]
        public string AlignmentStringsArePropagated(string value)
        {
            return Render(value, 1234);
        }

        [Test]
        public void FormatProviderIsUsed()
        {
            var m = Render(CultureInfo.GetCultureInfo("fr-FR"), "Please pay {Sum}", 12.345);
            Assert.AreEqual("Please pay 12,345", m);
        }

        static string Render(string messageTemplate, params object[] properties)
        {
            return Render(null, messageTemplate, properties);
        }

        static string Render(IFormatProvider formatProvider, string messageTemplate, params object[] properties)
        {
            var mt = new MessageTemplateParser().Parse(messageTemplate);
            var binder = new PropertyBinder(new PropertyValueConverter(10, Enumerable.Empty<Type>(), Enumerable.Empty<IDestructuringPolicy>()));
            var props = binder.ConstructProperties(mt, properties);
            var output = new StringBuilder();
            var writer = new StringWriter(output);
            mt.Render(props.ToDictionary(p => p.Name, p => p.Value), writer, formatProvider);
            writer.Flush();
            return output.ToString();
        }

        [Test]
        public void ATemplateWithOnlyPositionalPropertiesIsAnalyzedAndRenderedPositionally()
        {
            var m = Render("{1}, {0}", "world", "Hello");
            Assert.AreEqual("\"Hello\", \"world\"", m);
        }

        [Test]
        public void ATemplateWithOnlyPositionalPropertiesUsesFormatProvider()
        {
            var m = Render(CultureInfo.GetCultureInfo("fr-FR"), "{1}, {0}", 12.345, "Hello");
            Assert.AreEqual("\"Hello\", 12,345", m);
        }

        // Debatable what the behavior should be, here.
        [Test]
        public void ATemplateWithNamesAndPositionalsUsesNamesForAllValues()
        {
            var m = Render("{1}, {Place}", "world", "Hello");
            Assert.AreEqual("\"world\", \"Hello\"", m);
        }

        [Test]
        public void MissingPositionalParametersRenderAsTextLikeStandardFormats()
        {
            var m = Render("{1}, {0}", "world");
            Assert.AreEqual("{1}, \"world\"", m);
        }
    }
}
