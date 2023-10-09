namespace Serilog.PerformanceTests;

/// <summary>
/// Determines the cost of rendering a message template.
/// </summary>
[MemoryDiagnoser]
public class MessageTemplateRenderingBenchmark
{
    static readonly LogEvent NoProperties =
        Some.InformationEvent("This template has no properties");

    static readonly LogEvent VariedProperties =
        Some.InformationEvent("Processed {@Position} for {Task} in {Elapsed:000} ms",
            new { Latitude = 25, Longitude = 134 }, "Benchmark", 34);

    static readonly LogEvent EscapeString =
        Some.InformationEvent("Template for string escape properties {ValueToEscape} Json: {JsonToEscape}",
            $"This is simple string with \"quotes\"", Json);

    const string Json =
        "{\"StringField\":\"FieldsValues\",\"Amount\":666,\"Tax\":123.21,\"Id\":\"fae98759-d8a6-4b78-9bc8-60e4a2c33c7e\"," +
        "\"StringField1\":\"FieldsValues\",\"Amount1\":666,\"Tax\":123.21,\"Id1\":\"437ac564-1c4c-4fdd-98b0-aecc7bf50a6b\"," +
        "\"Data\":{\"Id\":21213,\"Name\":\"InnerName\",\"Value\":456.84},\"Data1\":{\"Id\":21213,\"Name\":\"InnerName\",\"Value\":456.84}}";

    readonly NullTextWriter _output = new();

    [Benchmark]
    public void TemplateWithNoProperties()
    {
        NoProperties.MessageTemplate.Render(NoProperties.Properties, _output);
    }

    [Benchmark]
    public void TemplateWithVariedProperties()
    {
        VariedProperties.MessageTemplate.Render(VariedProperties.Properties, _output);
    }

    [Benchmark]
    public void TemplateWithQuotesEscapeString()
    {
        EscapeString.MessageTemplate.Render(EscapeString.Properties, _output);
    }
}
