using BenchmarkDotNet.Attributes;
using System.IO;
using Serilog.Events;
using Serilog.PerformanceTests.Support;

namespace Serilog.PerformanceTests
{
    /// <summary>
    /// Determines the cost of rendering a message template.
    /// </summary>
    [MemoryDiagnoser]
    public class MessageTemplateRenderingBenchmark : BaseBenchmark
    {
        static readonly LogEvent NoProperties =
            Some.InformationEvent("This template has no properties");

        static readonly LogEvent VariedProperties =
            Some.InformationEvent("Processed {@Position} for {Task} in {Elapsed:000} ms",
                new { Latitude = 25, Longitude = 134 }, "Benchmark", 34);

        readonly TextWriter _output = new NullTextWriter();

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
    }
}
