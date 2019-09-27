using BenchmarkDotNet.Attributes;
using Serilog.Parsing;

namespace Serilog.PerformanceTests
{
    /// <summary>
    /// Tests the cost of parsing various message templates.
    /// </summary>
    [MemoryDiagnoser]
    public class MessageTemplateParsingBenchmark
    {
        readonly MessageTemplateParser _parser;

        const string _SimpleTextTemplate = "Hello, world!";
        const string _SinglePropertyTokenTemplate = "{Name}";
        const string _ManyPropertyTokenTemplate = "{Greeting}, {Name}!";
        const string _MultipleTokensTemplate = "Hello, world! {Greeting}, {Name} - {{Escaped}} - {@Hello} {$World}";
        const string _DefaultConsoleOutputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {Message}{NewLine}{Exception}";

        public MessageTemplateParsingBenchmark()
        {
            _parser = new MessageTemplateParser();
        }

        [Benchmark(Baseline = true)]
        public void EmptyTemplate()
        {
            _parser.Parse("");
        }
        
        [Benchmark]
        public void SimpleTextTemplate()
        {
            _parser.Parse(_SimpleTextTemplate);
        }

        [Benchmark]
        public void SinglePropertyTokenTemplate()
        {
            _parser.Parse(_SinglePropertyTokenTemplate);
        }
        
        [Benchmark]
        public void ManyPropertyTokenTemplate()
        {
            _parser.Parse(_ManyPropertyTokenTemplate);
        }

        [Benchmark]
        public void MultipleTokensTemplate()
        {
            _parser.Parse(_MultipleTokensTemplate);
        }

        [Benchmark]
        public void DefaultConsoleOutputTemplate()
        {
            _parser.Parse(_DefaultConsoleOutputTemplate);
        }
    }
}
