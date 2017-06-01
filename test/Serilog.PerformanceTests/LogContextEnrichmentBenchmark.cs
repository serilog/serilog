using BenchmarkDotNet.Attributes;
using Serilog.Context;
using Serilog.PerformanceTests.Support;
using Serilog.Events;

namespace Serilog.PerformanceTests
{ 
    public class LogContextEnrichmentBenchmark
    {
        ILogger _bare, _enriched;
        readonly LogEvent _event = Some.InformationEvent();
        
        [Setup]
        public void Setup()
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
  