using BenchmarkDotNet.Attributes;
using Serilog.Context;
using Serilog.Events;
using Serilog.PerformanceTests.Support;

namespace Serilog.PerformanceTests
{
    public class LogContextEnrichmentBenchmark
    {
        ILogger _bare, _enriched;
        readonly LogEvent _event = Some.InformationEvent();

        public LogContextEnrichmentBenchmark()
        {
            _bare = new LoggerConfiguration()
                .WriteTo.Sink(new NullSink())
                .CreateLogger();

            _enriched = new LoggerConfiguration()
                .WriteTo.Sink(new NullSink())
                .Enrich.FromLogContext()
                .CreateLogger();
        }

        [Benchmark(Baseline = true)]
        public void Bare()
        {
            _bare.Write(_event);
        }

        [Benchmark]
        public void PushProperty()
        {
            using (LogContext.PushProperty("A", "B"))
            {
            }
        }

        [Benchmark]
        public void PushPropertyNested()
        {
            using (LogContext.PushProperty("A", "B"))
            using (LogContext.PushProperty("C", "D"))
            {
            }
        }

        [Benchmark]
        public void PushPropertyEnriched()
        {
            using (LogContext.PushProperty("A", "B"))
            {
                _enriched.Write(_event);
            }
        }
    }
}
