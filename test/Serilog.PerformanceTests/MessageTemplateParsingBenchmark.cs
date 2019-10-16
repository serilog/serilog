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
        const string _SingleTextWithPropertyTemplate = "This is a new Log entry with some external {Data}";
        const string _ManyPropertyTokenTemplate = "{Greeting}, {Name}!";
        const string _MultipleTokensTemplate = "Hello, world! {Greeting}, {Name} - {{Escaped}} - {@Hello} {$World}";
        const string _DefaultConsoleOutputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {Message}{NewLine}{Exception}";
        const string _BigTemplate = "Hello, world! {Greeting}, {Name} - {{Escaped}} - {@Hello} {$World} {Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {Message}{NewLine}{Exception} Hello, world! {0} {-1} {2} {Test,-15} {Prop,50} {Qnt,5:000}";

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
        public void SingleTextWithPropertyTemplate()
        {
            _parser.Parse(_SingleTextWithPropertyTemplate);
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

        [Benchmark]
        public void BigTemplate()
        {
            _parser.Parse(_BigTemplate);
        }
    }
}
