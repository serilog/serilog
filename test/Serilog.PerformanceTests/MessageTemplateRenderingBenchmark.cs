using BenchmarkDotNet.Attributes;
using System.IO;
using Serilog.Events;
using Serilog.PerformanceTests.Support;

namespace Serilog.PerformanceTests
{
    /// <summary>
    /// Determines the cost of rendering a message template.
    /// </summary>
    public class MessageTemplateRenderingBenchmark
    {
        static readonly LogEvent NoProperties =
            Some.InformationEvent("This template has no properties");

        static readonly LogEvent VariedProperties =
            Some.InformationEvent("Processed {@Position} for {Task} in {Elapsed:000} ms",
                new { Latitude = 25, Longitude = 134 }, "Benchmark", 34);

        readonly StringWriter _output = new StringWriter();

        [Setup]
        public void Setup()
        {
            _output.GetStringBuilder().Length = 0;
            _output.GetStringBuilder().Capacity = 1024; // Only a few dozen chars actually needed here.
        }

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
