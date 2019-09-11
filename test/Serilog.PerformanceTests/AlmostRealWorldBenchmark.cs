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
    [ShortRunJob]
    public class AlmostRealWorldBenchmark
    {
        const int TodoMainLoopCount = 10_000;
        static readonly LoggingLevelSwitch LoggingLevelSwitch = new LoggingLevelSwitch(LogEventLevel.Verbose);
        static readonly Random Rnd = new Random(42);

        [Benchmark(Baseline = true)]
        public void SimulateAAppWithoutSerilog()
        {
            var execType = ExecutionType.Test;
            RunTestWithoutLog(execType);
        }

        [Benchmark()]
        public void SimulateAAppWithSerilogOff()
        {
            LoggingLevelSwitch.MinimumLevel = LogEventLevel.Fatal; //Just Log Fatal and other Log levels OFF
            SimulateAAppWithSerilog();
        }

        [Benchmark()]
        public void SimulateAAppWithSerilogOn()
        {
            LoggingLevelSwitch.MinimumLevel = LogEventLevel.Verbose; //All Log levels ON
            SimulateAAppWithSerilog();
        }

        static ILogger CreateLog()
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

        static void SimulateAAppWithSerilog()
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

                RunTestWithLog(execType, log2);

                log.Debug("App - Ending...");
            }
            finally
            {
                (log as IDisposable)?.Dispose();
            }
        }

        static int RunTestWithoutLog(ExecutionType execType)
        {
            using (LogContext.PushProperty("Operation", "Testing"))
            {
                var todo = Enumerable.Range(0, TodoMainLoopCount).ToList();
                var passed = 0;
                var fail = 0;

                foreach (var i in todo)
                {
                    using (LogContext.PushProperty("Item", i))
                    {
                        var result = (Rnd.Next(0, 1) == 1);
                        if (result)
                        {
                            passed++;
                        }
                        else
                        {
                            fail++;
                        }
                    }
                }

                return passed - fail;
            }
        }

        static int RunTestWithLog(ExecutionType execType, ILogger log)
        {
            var start = DateTimeOffset.UtcNow;
            log.Information("Starting Exec of the Test Begin at {StartDateTime}", start);
            try
            {
                using (LogContext.PushProperty("Operation", "Testing"))
                {
                    var todo = Enumerable.Range(0, TodoMainLoopCount).ToList();
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

                    return passed - fail;
                }
            }
            finally
            {
                var end = DateTimeOffset.UtcNow;
                if (log.IsEnabled(LogEventLevel.Information))
                {
                    log.Information($"Exec ended - Elapsed {(end - start)}");
                }
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

