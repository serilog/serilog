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
    [MemoryDiagnoser]
    public class OutputTemplateRenderingBenchmark : BaseBenchmark
    {
        const string DefaultFileOutputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}";
        static readonly LogEvent HelloWorldEvent = Some.InformationEvent("Hello, {Name}", "World");
        static readonly MessageTemplateTextFormatter Formatter = new MessageTemplateTextFormatter(DefaultFileOutputTemplate, CultureInfo.InvariantCulture);

        readonly TextWriter _output = new NullTextWriter();

        [Benchmark]
        public void FormatToOutput()
        {
            Formatter.Format(HelloWorldEvent, _output);
        }
    }
}
