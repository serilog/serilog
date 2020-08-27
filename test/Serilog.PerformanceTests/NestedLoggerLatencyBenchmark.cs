using BenchmarkDotNet.Attributes;
using Serilog.Events;
using Serilog.PerformanceTests.Support;

namespace Serilog.PerformanceTests
{
    /// <summary>
    /// Tests the overhead of writing through a nested logger.
    /// </summary>
    public class NestedLoggerLatencyBenchmark : BaseBenchmark
    {
        ILogger _log, _nested;
        readonly LogEvent _event = Some.InformationEvent();

        public NestedLoggerLatencyBenchmark()
        {
            _log = new LoggerConfiguration()
                .WriteTo.Sink(new NullSink())
                .CreateLogger();

            _nested = _log.ForContext<NestedLoggerLatencyBenchmark>();
        }

        [Benchmark(Baseline = true)]
        public void RootLogger()
        {
            _log.Write(_event);
        }

        [Benchmark]
        public void NestedLogger()
        {
            _nested.Write(_event);
        }
    }
}
