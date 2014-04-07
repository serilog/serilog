using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Serilog;
using Serilog.Enrichers;
using Serilog.Events;

namespace TimingSample
{
    internal class Program
    {
        private static void Main()
        {
            var logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.ColoredConsole(
                    outputTemplate: "{Timestamp:HH:mm:ss} ({ThreadId}) [{Level}] {Message:l}{NewLine:l}{Exception:l}")
                .WriteTo.Trace()
                .Enrich.WithProperty("App", "Test Harness")
                .Enrich.With(new ThreadIdEnricher(), new MachineNameEnricher())
                .CreateLogger();

            logger.Information("Just biting {Fruit} number {Count}", "Apple", 12);
            logger.ForContext<Program>().Information("Just biting {Fruit} number {Count:0000}", "Apple", 12);

            using (logger.BeginTimedOperation("Time a thread sleep for 2 seconds."))
            {
                Thread.Sleep(1000);
                using (logger.BeginTimedOperation("And inside we try a Task.Delay for 2 seconds."))
                {
                    Task.Delay(2000).Wait();
                }
                Thread.Sleep(1000);
            }

            using (logger.BeginTimedOperation("Using a passed in identifier", "test-loop"))
            {
                var a = "";
                for (int i = 0; i < 1000; i++)
                {
                    a += "b";
                }
            }

            // Exceed a limit
            using (logger.BeginTimedOperation("This should execute within 1 second.",null, LogEventLevel.Debug, TimeSpan.FromSeconds(1)))
            {
                Thread.Sleep(1100);
            }

             // Gauge
            var queue = new Queue<int>();
            var gauge = logger.GaugedOperation("queue", () => queue.Count());

            gauge.Measure();

            queue.Enqueue(2);

            gauge.Measure();

            queue.Dequeue();

            gauge.Measure();

            // Counter
            var counter = logger.CountOperation("counter", LogEventLevel.Debug);
            counter.Increment();
            counter.Increment();
            counter.Increment();
            counter.Decrement();

            // Meter
            var meter = logger.MeterOperation("meter");
            meter.Mark();


            Console.ReadKey(true);
        }
    }
}
