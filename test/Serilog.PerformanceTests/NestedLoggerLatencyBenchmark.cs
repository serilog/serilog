using BenchmarkDotNet.Attributes;
using Serilog.PerformanceTests.Support;
using Serilog.Events;

namespace Serilog.PerformanceTests
{
    /// <summary>
    /// Tests the overhead of writing through a nested logger.
    /// </summary>
    public class NestedLoggerLatencyBenchmark
    {
        ILogger _log, _nested;
        readonly LogEvent _event = Some.InformationEvent();

        [Setup]
        public void Setup()
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
  