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
        Some.InformationEvent("Template for string escape properties {ValueToEscape}",
            $"This is simple {new string('"', 128)} string with \"quotes\"");

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
