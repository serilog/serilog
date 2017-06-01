using BenchmarkDotNet.Attributes;
using System.Globalization;
using System.IO;
using Serilog.Events;
using Serilog.Formatting.Display;
using Serilog.PerformanceTests.Support;

namespace Serilog.PerformanceTests
{
    /// <summary>
    /// Determines the cost of rendering an event out to one of the typical text targets,
    /// like the console or a text file.
    /// </summary>
    public class OutputTemplateRenderingBenchmark
    {
        const string DefaultFileOutputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}";
        static readonly LogEvent HelloWorldEvent = Some.InformationEvent("Hello, {Name}", "World");
        static readonly MessageTemplateTextFormatter Formatter = new MessageTemplateTextFormatter(DefaultFileOutputTemplate, CultureInfo.InvariantCulture);

        readonly StringWriter _output = new StringWriter();

        [Setup]
        public void Setup()
        {
            _output.GetStringBuilder().Length = 0;
            _output.GetStringBuilder().Capacity = 1024; // Only a few dozen chars actually needed here.
        }

        [Benchmark]
        public void FormatToOutput()
        {
            Formatter.Format(HelloWorldEvent, _output);
        }
    }
}
