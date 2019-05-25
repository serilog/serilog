using BenchmarkDotNet.Attributes;
using Serilog.PerformanceTests.Support;

namespace Serilog.PerformanceTests
{
    /// <summary>
    /// Tests the cost creating a nested logger.
    /// </summary>
    public class NestedLoggerCreationBenchmark
    {
        ILogger log;

        public NestedLoggerCreationBenchmark()
        {
            log = new LoggerConfiguration()
                .WriteTo.Sink(new NullSink())
                .CreateLogger();
        }

        [Benchmark]
        public void ForContextString()
        {
            log.ForContext("SourceContext", "Serilog.PerformanceTests");
        }

        [Benchmark]
        public void ForContextType()
        {
            log.ForContext<NestedLoggerCreationBenchmark>();
        }
    }
}
