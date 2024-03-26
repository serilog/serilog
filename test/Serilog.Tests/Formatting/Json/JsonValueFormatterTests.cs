namespace Serilog.Tests.Formatting.Json;

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
    [InlineData("", "\"\"")]
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

#if FEATURE_DATE_AND_TIME_ONLY

    [Fact]
    public void DateOnly()
    {
        JsonLiteralTypesAreFormatted(new DateOnly(2016, 01, 01), "\"2016-01-01\"");
    }

    [Fact]
    public void TimeOnly()
    {
        JsonLiteralTypesAreFormatted(new TimeOnly(13, 01, 01, 999), "\"13:01:01.9990000\"");
    }

#endif

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

    [Fact]
    public void TimeSpanFormatsAsString()
    {
        JsonLiteralTypesAreFormatted(TimeSpan.FromHours(1), "\"01:00:00\"");
        JsonLiteralTypesAreFormatted(TimeSpan.FromHours(-1), "\"-01:00:00\"");
        JsonLiteralTypesAreFormatted(TimeSpan.Zero, "\"00:00:00\"");
        JsonLiteralTypesAreFormatted(TimeSpan.FromDays(1), "\"1.00:00:00\"");
        JsonLiteralTypesAreFormatted(TimeSpan.FromDays(-1), "\"-1.00:00:00\"");
        JsonLiteralTypesAreFormatted(TimeSpan.MinValue, "\"-10675199.02:48:05.4775808\"");
        JsonLiteralTypesAreFormatted(TimeSpan.MaxValue, "\"10675199.02:48:05.4775807\"");
    }

    [Fact]
    public void GuidFormatsAsString()
    {
        JsonLiteralTypesAreFormatted(Guid.Parse("88c117ae-616c-4bf7-ab58-9c729b15c562"), "\"88c117ae-616c-4bf7-ab58-9c729b15c562\"");
        JsonLiteralTypesAreFormatted(Guid.Empty, "\"00000000-0000-0000-0000-000000000000\"");
    }

    [Fact]
    public void ObjectsAreFormattedAsJsonStringViaToString()
    {
        JsonLiteralTypesAreFormatted(new Exception("This e a Exception"), "\"System.Exception: This e a Exception\"");
        JsonLiteralTypesAreFormatted(new AChair(), "\"a chair\"");
        JsonLiteralTypesAreFormatted(new ToStringReturnsNull(), "\"\"");
    }

    [Fact]
    public void ObjectsAreFormattedAsExceptionStringsInJsonWhenToStringThrows()
    {
        Assert.Throws<ArgumentNullException>(() => JsonLiteralTypesAreFormatted(new ToStringThrows(), "will Throws a error before comparing with this string"));
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
        var structure = new StructureValue(Array.Empty<LogEventProperty>(), "T");
        var formatter = new JsonValueFormatter("$type");
        var output = new StringWriter();
        formatter.Format(structure, output);
        var f = output.ToString();
        Assert.Equal("{\"$type\":\"T\"}", f);
    }

    [Fact]
    public void WhenNullNoTypeTagIsWritten()
    {
        var structure = new StructureValue(Array.Empty<LogEventProperty>(), "T");
        var formatter = new JsonValueFormatter(null);
        var output = new StringWriter();
        formatter.Format(structure, output);
        var f = output.ToString();
        Assert.Equal("{}", f);
    }

    class AChair
    {
        public string Back => "";
        public int[]? Legs => null;
        public override string ToString() => "a chair";
    }

    class ToStringReturnsNull
    {
        public string AProp => "";
        public override string? ToString() => null;
    }

    class ToStringThrows
    {
        public string AProp => "";
        public override string ToString() => throw new ArgumentNullException("", "A possible a Bug in a class");
    }
}
