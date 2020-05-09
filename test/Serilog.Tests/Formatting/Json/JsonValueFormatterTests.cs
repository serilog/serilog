using System;
using System.Collections.Generic;
using System.IO;
using Serilog.Events;
using Serilog.Formatting.Json;
using Serilog.Tests.Support;
using Xunit;

namespace Serilog.Tests.Formatting.Json
{
    public class JsonValueFormatterTests
    {
        class JsonLiteralValueFormatter : JsonValueFormatter
        {
            public string Format(object literal)
            {
                var output = new StringWriter();
                FormatLiteralValue(literal, output);
                return output.ToString();
            }
        }

        [Theory]
        [InlineData(0, "0")]
        [InlineData(123, "123")]
        [InlineData(-123, "-123")]
        [InlineData(123L, "123")]
        [InlineData(-123L, "-123")]
        [InlineData('c', "\"c\"")]
        [InlineData('é', "\"é\"")]
        [InlineData('\t', "\"\\t\"")]
        [InlineData('\n', "\"\\n\"")]
        [InlineData('\0', "\"\\u0000\"")]
        [InlineData("Hello, world!", "\"Hello, world!\"")]
        [InlineData(true, "true")]
        [InlineData(false, "false")]
        [InlineData("\\\"\t\r\n\f", "\"\\\\\\\"\\t\\r\\n\\f\"")]
        [InlineData("🤷‍", "\"🤷‍\"")]
        [InlineData("\u0001", "\"\\u0001\"")]
        [InlineData("a\nb", "\"a\\nb\"")]
        [InlineData(null, "null")]
        public void JsonLiteralTypesAreFormatted(object value, string expectedJson)
        {
            var formatter = new JsonLiteralValueFormatter();
            Assert.Equal(expectedJson, formatter.Format(value));
        }

        [Fact]
        public void DateTimesFormatAsIso8601()
        {
            JsonLiteralTypesAreFormatted(new DateTime(2016, 01, 01, 13, 13, 13, DateTimeKind.Utc), "\"2016-01-01T13:13:13.0000000Z\"");
        }

        [Fact]
        public void DoubleFormatsAsNumber()
        {
            JsonLiteralTypesAreFormatted(0d, "0");
            JsonLiteralTypesAreFormatted(123.0d, "123");
            JsonLiteralTypesAreFormatted(-123.0d, "-123");
            JsonLiteralTypesAreFormatted(123.45, "123.45");
            JsonLiteralTypesAreFormatted(-123.45, "-123.45");
        }

        [Fact]
        public void DoubleSpecialsFormatAsString()
        {
            JsonLiteralTypesAreFormatted(double.NaN, "\"NaN\"");
            JsonLiteralTypesAreFormatted(double.PositiveInfinity, "\"Infinity\"");
            JsonLiteralTypesAreFormatted(double.NegativeInfinity, "\"-Infinity\"");
        }

        [Fact]
        public void FloatFormatsAsNumber()
        {
            JsonLiteralTypesAreFormatted(0f, "0");
            JsonLiteralTypesAreFormatted(123.0f, "123");
            JsonLiteralTypesAreFormatted(-123.0f, "-123");
            JsonLiteralTypesAreFormatted(123.45f, "123.45");
            JsonLiteralTypesAreFormatted(-123.45f, "-123.45");
        }

        [Fact]
        public void FloatSpecialsFormatAsString()
        {
            JsonLiteralTypesAreFormatted(float.NaN, "\"NaN\"");
            JsonLiteralTypesAreFormatted(float.PositiveInfinity, "\"Infinity\"");
            JsonLiteralTypesAreFormatted(float.NegativeInfinity, "\"-Infinity\"");
        }

        [Fact]
        public void DecimalFormatsAsNumber()
        {
            JsonLiteralTypesAreFormatted(0m, "0");
            JsonLiteralTypesAreFormatted(123.45m, "123.45");
            JsonLiteralTypesAreFormatted(-123.45m, "-123.45");
            JsonLiteralTypesAreFormatted(123.0m, "123.0");
            JsonLiteralTypesAreFormatted(-123.0m, "-123.0");
        }

        static string Format(LogEventPropertyValue value)
        {
            var formatter = new JsonValueFormatter();
            var output = new StringWriter();
            formatter.Format(value, output);
            return output.ToString();
        }

        [Fact]
        public void ScalarPropertiesFormatAsLiteralValues()
        {
            var f = Format(new ScalarValue(123));
            Assert.Equal("123", f);
        }

        [Fact]
        public void SequencePropertiesFormatAsArrayValue()
        {
            var f = Format(new SequenceValue(new[] { new ScalarValue(123), new ScalarValue(456) }));
            Assert.Equal("[123,456]", f);
        }

        [Fact]
        public void StructuresFormatAsAnObject()
        {
            var structure = new StructureValue(new[] { new LogEventProperty("A", new ScalarValue(123)) }, "T");
            var f = Format(structure);
            Assert.Equal("{\"A\":123,\"_typeTag\":\"T\"}", f);
        }

        [Fact]
        public void DictionaryWithScalarKeyFormatsAsAnObject()
        {
            var dict = new DictionaryValue(new Dictionary<ScalarValue, LogEventPropertyValue> {
                { new ScalarValue(12), new ScalarValue(345) }
            });

            var f = Format(dict);
            Assert.Equal("{\"12\":345}", f);
        }

        [Fact]
        public void SequencesOfSequencesAreFormatted()
        {
            var s = new SequenceValue(new[] { new SequenceValue(new[] { new ScalarValue("Hello") }) });

            var f = Format(s);
            Assert.Equal("[[\"Hello\"]]", f);
        }

        [Fact]
        public void TypeTagCanBeOverridden()
        {
            var structure = new StructureValue(new LogEventProperty[0], "T");
            var formatter = new JsonValueFormatter("$type");
            var output = new StringWriter();
            formatter.Format(structure, output);
            var f = output.ToString();
            Assert.Equal("{\"$type\":\"T\"}", f);
        }

        [Fact]
        public void WhenNullNoTypeTagIsWritten()
        {
            var structure = new StructureValue(new LogEventProperty[0], "T");
            var formatter = new JsonValueFormatter(null);
            var output = new StringWriter();
            formatter.Format(structure, output);
            var f = output.ToString();
            Assert.Equal("{}", f);
        }
    }
}
