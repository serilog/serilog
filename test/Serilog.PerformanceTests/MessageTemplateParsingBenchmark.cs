using BenchmarkDotNet.Attributes;
using Serilog.Parsing;

namespace Serilog.PerformanceTests
{
    /// <summary>
    /// Tests the cost of parsing various message templates.
    /// </summary>
    public class MessageTemplateParsingBenchmark
    {  
        MessageTemplateParser _parser; 
        const string _DefaultConsoleOutputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {Message}{NewLine}{Exception}";
        
        [Setup]
        public void Setup()
        { 
            _parser = new MessageTemplateParser();
        }

        [Benchmark(Baseline = true)]
        public void EmptyTemplate()
        {
            _parser.Parse("");
        }  

        [Benchmark]
        public void DefaultConsoleOutputTemplate()
        {
            _parser.Parse(_DefaultConsoleOutputTemplate);
        }  
    }
}
  