using System;
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
        static readonly LogEvent _noMessageEvent = Some.InformationEvent(string.Empty);
        static readonly LogEvent _noPropertiesEvent = Some.InformationEvent("This template has no properties");
        static readonly LogEvent _oneSimplePropertiesEvent = Some.InformationEvent("This template has {One} properties", 1);
        static readonly LogEvent _variedPropertiesEvent = Some.InformationEvent("Processed {@Position} for {Task} in {Elapsed:000} ms", new { Latitude = 25, Longitude = 134 }, "Benchmark", 34);
        static readonly LogEvent _complexPropertiesEvent = Some.InformationEvent("Hello, world! {Greeting}, {Name} - {{Escaped}} - {@Hello} {$World} {Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {Msg}{NewLine}{Ex} Hello, world! {Test,-15} {Prop,50} {Qnt,5:000}",
                                                                                 "Greeting", 1, new { Latitude = 25, Longitude = 134 }, 3.14F, new System.DateTimeOffset(2020, 02, 20, 20, 02, 22, TimeSpan.Zero), "Info", "Text", "\n", new Exception(), 12345, 123, 1);

        readonly TextWriter _output = new NullTextWriter();


        [Benchmark(Baseline = true)]
        public void NoMessage()
        {
            _noMessageEvent.MessageTemplate.Render(_noMessageEvent.Properties, _output);
        }

        [Benchmark]
        public void NoProperties()
        {
            _noPropertiesEvent.MessageTemplate.Render(_noPropertiesEvent.Properties, _output);
        }

        [Benchmark]
        public void OneSimpleProperties()
        {
            _oneSimplePropertiesEvent.MessageTemplate.Render(_oneSimplePropertiesEvent.Properties, _output);
        }

        [Benchmark]
        public void VariedProperties()
        {
            _variedPropertiesEvent.MessageTemplate.Render(_variedPropertiesEvent.Properties, _output);
        }

        [Benchmark]
        public void ComplexProperties()
        {
            _complexPropertiesEvent.MessageTemplate.Render(_complexPropertiesEvent.Properties, _output);
        }
    }
}
