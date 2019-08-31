using System;
using System.Linq;
using BenchmarkDotNet.Attributes;
using Serilog.Context;
using Serilog.Core;
using Serilog.Events;
using Serilog.PerformanceTests.Support;

namespace Serilog.PerformanceTests
{
    [MemoryDiagnoser]
    [MinColumn, MaxColumn]
    [ClrJob(baseline: true), CoreJob]
    [ShortRunJob]
    public class AlmostRealWorldBenchmark
    {
        static readonly LoggingLevelSwitch LoggingLevelSwitch = new LoggingLevelSwitch(LogEventLevel.Verbose);
        static readonly Random Rnd = new Random(42);

        ILogger CreateLog()
        {
            return Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Environment", "BenchmarkTester")
                .Enrich.WithProperty("Application", "Serilog.PerformanceTests.AlmostRealWorldBenchmark")
                .Enrich.WithProperty("ExecutionId", Guid.NewGuid())
                .Enrich.WithProperty("ExecutionStartDate", DateTimeOffset.UtcNow)
                .MinimumLevel.ControlledBy(LoggingLevelSwitch)
                .WriteTo.Logger(l => l
                    .Filter.ByExcluding(ev => ev.Properties.ContainsKey("ProgressEntry"))
                    .WriteTo.Sink(new NullSink())) //To Simulate a FileSink that don't 'records' some LogEvents
                .WriteTo.Sink(new NullSink()) //To Simulate a Console Sink that show all LogEvents
                .CreateLogger();
        }

        [Benchmark(Baseline = true)]
        public void LogLikeAApp()
        {
            var log = CreateLog(); //Always create a new instance of the logger each test to Benchmark the creation with the test.
            try
            {
                log.Debug("App - Start...");

                log.Information("Arguments: {@Args}", new[] { "test", "performance", "-q" });
                log.Debug("Parsing args.");
                var execType = ExecutionType.Test;

                var log2 = log.ForContext("Type", execType);
                log2.Information("Running in {Type} mode", execType);

                RunTest(log2);

                log.Debug("App - Ending...");
            }
            finally
            {
                (log as IDisposable)?.Dispose();
            }
        }

        static void RunTest(ILogger log)
        {
            var start = DateTimeOffset.UtcNow;
            log.Information("Starting Exec of the Test Begin at {StartDateTime}", start);

            using (LogContext.PushProperty("Operation", "Testing"))
            {
                var todo = Enumerable.Range(0, 10_000).ToList();
                var passed = 0;
                var fail = 0;

                foreach (var i in todo.LogProgress(log))
                {
                    using (LogContext.PushProperty("Item", i))
                    {
                        log.Verbose("Testing... {ItemNumber}", i);

                        var result = (Rnd.Next(0, 1) == 1);
                        if (result)
                        {
                            passed++;
                            log.Information("Test Passed.");
                        }
                        else
                        {
                            fail++;
                            log.Warning("Test not Passed.");
                        }

                        log.Verbose("Item Test End");
                    }
                }
                log.Debug("Test End");

                log.Information("Qnt of tests: {QntOfItems}", todo.Count);
                log.Information("Test Statistics: {@Statistics}", new { Qnt = todo.Count, Passed = passed, Fail = fail });
            }

            var end = DateTimeOffset.UtcNow;
            if (log.IsEnabled(LogEventLevel.Information))
            {
                log.Information($"Exec ended - Elapsed {(end - start)}");
            }
        }

        enum ExecutionType
        {
            Test,
            Build,
            Run,
        }
    }
}

