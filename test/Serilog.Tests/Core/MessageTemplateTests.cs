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

        [Fact]
        public void AnObjectIsRenderedInSimpleNotation()
        {
            var m = Render("I sat at {@Chair}", new Chair());
            Assert.Equal("I sat at Chair { Back: \"straight\", Legs: [1, 2, 3, 4] }", m);
        }

        [Fact]
        public void AnObjectIsRenderedInSimpleNotationUsingFormatProvider()
        {
            var m = Render(new CultureInfo("fr-FR"), "I received {@Receipt}", new Receipt());
            Assert.Equal("I received Receipt { Sum: 12,345, When: 20/05/2013 16:39:00 }", m);
        }

        [Fact]
        public void AnAnonymousObjectIsRenderedInSimpleNotationWithoutType()
        {
            var m = Render("I sat at {@Chair}", new { Back = "straight", Legs = new[] { 1, 2, 3, 4 } });
            Assert.Equal("I sat at { Back: \"straight\", Legs: [1, 2, 3, 4] }", m);
        }

        [Fact]
        public void AnAnonymousObjectIsRenderedInSimpleNotationWithoutTypeUsingFormatProvider()
        {
            var m = Render(new CultureInfo("fr-FR"), "I received {@Receipt}", new { Sum = 12.345, When = new DateTime(2013, 5, 20, 16, 39, 0) });
            Assert.Equal("I received { Sum: 12,345, When: 20/05/2013 16:39:00 }", m);
        }

        [Fact]
        public void AnObjectWithDefaultDestructuringIsRenderedAsAStringLiteral()
        {
            var m = Render("I sat at {Chair}", new Chair());
            Assert.Equal("I sat at \"a chair\"", m);
        }

        [Fact]
        public void AnObjectWithStringifyDestructuringIsRenderedAsAString()
        {
            var m = Render("I sat at {$Chair}", new Chair());
            Assert.Equal("I sat at \"a chair\"", m);
        }

        [Fact]
        public void MultiplePropertiesAreRenderedInOrder()
        {
            var m = Render("Just biting {Fruit} number {Count}", "Apple", 12);
            Assert.Equal("Just biting \"Apple\" number 12", m);
        }

        [Fact]
        public void MultiplePropertiesUseFormatProvider()
        {
            var m = Render(new CultureInfo("fr-FR"), "Income was {Income} at {Date:d}", 1234.567, new DateTime(2013, 5, 20));
            Assert.Equal("Income was 1234,567 at 20/05/2013", m);
        }

        [Fact]
        public void FormatStringsArePropagated()
        {
            var m = Render("Welcome, customer {CustomerId:0000}", 12);
            Assert.Equal("Welcome, customer 0012", m);
        }

        [Theory]
        [InlineData("Welcome, customer #{CustomerId,-10}, pleasure to see you", "Welcome, customer #1234      , pleasure to see you")]
        [InlineData("Welcome, customer #{CustomerId,-10:000000}, pleasure to see you", "Welcome, customer #001234    , pleasure to see you")]
        [InlineData("Welcome, customer #{CustomerId,10}, pleasure to see you", "Welcome, customer #      1234, pleasure to see you")]
        [InlineData("Welcome, customer #{CustomerId,10:000000}, pleasure to see you", "Welcome, customer #    001234, pleasure to see you")]
        [InlineData("Welcome, customer #{CustomerId,10:0,0}, pleasure to see you", "Welcome, customer #     1,234, pleasure to see you")]
        [InlineData("Welcome, customer #{CustomerId:0,0}, pleasure to see you", "Welcome, customer #1,234, pleasure to see you")]
        public void AlignmentStringsArePropagated(string value, string expected)
        {
            Assert.Equal(expected, Render(value, 1234));
        }

        [Fact]
        public void FormatProviderIsUsed()
        {
            var m = Render(new CultureInfo("fr-FR"), "Please pay {Sum}", 12.345);
            Assert.Equal("Please pay 12,345", m);
        }

        static string Render(string messageTemplate, params object[] properties)
        {
            return Render(null, messageTemplate, properties);
        }

        static string Render(IFormatProvider formatProvider, string messageTemplate, params object[] properties)
        {
            var mt = new MessageTemplateParser().Parse(messageTemplate);
            var binder = new PropertyBinder(new PropertyValueConverter(10, Enumerable.Empty<Type>(), Enumerable.Empty<IDestructuringPolicy>(), false));
            var props = binder.ConstructProperties(mt, properties);
            var output = new StringBuilder();
            var writer = new StringWriter(output);
            mt.Render(props.ToDictionary(p => p.Name, p => p.Value), writer, formatProvider);
            writer.Flush();
            return output.ToString();
        }

        [Fact]
        public void ATemplateWithOnlyPositionalPropertiesIsAnalyzedAndRenderedPositionally()
        {
            var m = Render("{1}, {0}", "world", "Hello");
            Assert.Equal("\"Hello\", \"world\"", m);
        }

        [Fact]
        public void ATemplateWithOnlyPositionalPropertiesUsesFormatProvider()
        {
            var m = Render(new CultureInfo("fr-FR"), "{1}, {0}", 12.345, "Hello");
            Assert.Equal("\"Hello\", 12,345", m);
        }

        // Debatable what the behavior should be, here.
        [Fact]
        public void ATemplateWithNamesAndPositionalsUsesNamesForAllValues()
        {
            var m = Render("{1}, {Place}", "world", "Hello");
            Assert.Equal("\"world\", \"Hello\"", m);
        }

        [Fact]
        public void MissingPositionalParametersRenderAsTextLikeStandardFormats()
        {
            var m = Render("{1}, {0}", "world");
            Assert.Equal("{1}, \"world\"", m);
        }

        [Fact]
        public void AnonymousTypeShouldBeRendered()
        {
            var anonymous = new { Test = 3M };
            var m = Render("Anonymous type {value}", anonymous);
            Assert.Equal("Anonymous type \"{ Test = 3 }\"", m);
        }

        [Fact]
        public void EnumerableOfAnonymousTypeShouldBeRendered()
        {
            var anonymous = new { Foo = 4M, Bar = "Baz" };
            var enumerable = Enumerable.Repeat("MyKey", 1).Select(v => anonymous);
            var m = Render("Enumerable with anonymous type {enumerable}", enumerable);
            Assert.Equal("Enumerable with anonymous type [\"{ Foo = 4, Bar = Baz }\"]", m);
        }

        [Fact]
        public void DictionaryOfAnonymousTypeAsValueShouldBeRendered()
        {
            var anonymous = new { Test = 5M };
            var dictionary = Enumerable.Repeat("MyKey", 1).ToDictionary(v => v, v => anonymous);
            var m = Render("Dictionary with anonymous type value {dictionary}", dictionary);
            Assert.Equal("Dictionary with anonymous type value [(\"MyKey\": \"{ Test = 5 }\")]", m);
        }

        [Fact]
        public void DictionaryOfAnonymousTypeAsKeyShouldBeRendered()
        {
            var anonymous = new { Bar = 6M, Baz = 4M };
            var dictionary = Enumerable.Repeat("MyValue", 1).ToDictionary(v => anonymous, v => v);
            var m = Render("Dictionary with anonymous type key {dictionary}", dictionary);
            Assert.Equal("Dictionary with anonymous type key [\"[{ Bar = 6, Baz = 4 }, MyValue]\"]", m);
        }
    }
}