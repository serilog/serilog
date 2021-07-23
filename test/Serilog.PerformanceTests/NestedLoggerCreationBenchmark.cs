using BenchmarkDotNet.Attributes;
using Serilog.PerformanceTests.Support;

namespace Serilog.PerformanceTests
{
    /// <summary>
    /// Tests the cost creating a nested logger.
    /// </summary>
    [MemoryDiagnoser]
    public class NestedLoggerCreationBenchmark
    {
        ILogger log = null!;

        [GlobalSetup]
        public void Setup()
        {
            log = new LoggerConfiguration()
                .WriteTo.Sink(new NullSink())
                .CreateLogger();
        }

        [Benchmark]
        public void ForContextInt()
        {
            log.ForContext("Number", 1);
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
