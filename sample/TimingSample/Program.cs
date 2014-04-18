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
                .WriteTo.ColoredConsole()
                .WriteTo.Trace()
                .CreateLogger();
             
            // Meter
            var meter = logger.MeterOperation("meter");
            meter.Mark();

            using (logger.BeginTimedOperation("Time a thread sleep for 2 seconds."))
            {
                Thread.Sleep(1000);
                using (logger.BeginTimedOperation("And inside we try a Task.Delay for 2 seconds."))
                {
                    meter.Mark();
                    Task.Delay(2000).Wait();
                }
                meter.Mark();
                Thread.Sleep(1000);
            }

            meter.Mark();

            using (logger.BeginTimedOperation("Using a passed in identifier", "test-loop"))
            {
                var a = "";
                for (int i = 0; i < 1000; i++)

                {
                    a += "b";
                }
            }

            // Exceed a limit
            using (logger.BeginTimedOperation("This should execute within 1 second.", null, LogEventLevel.Debug, TimeSpan.FromSeconds(1)))
            {
                Thread.Sleep(1100);
                meter.Mark();
            }

            // Gauge
            var queue = new Queue<int>();
            var gauge = logger.GaugeOperation("queue", "item(s)", () => queue.Count());

            gauge.Write();

            queue.Enqueue(20);

            gauge.Write();

            queue.Dequeue();

            meter.Mark();

            gauge.Write();

            // Counter
            var counter = logger.CountOperation("counter", "operation(s)", true, LogEventLevel.Debug);
            counter.Increment();
            counter.Increment();
            counter.Increment();
            counter.Decrement();

            meter.Write();


            const int count = 100000;
            var block = new ManualResetEvent(false);
   
            var j = 0;
            ThreadPool.QueueUserWorkItem(s =>
            {
                while (j < count)
                {
                    meter.Mark();
                    j++;
                }
                Thread.Sleep(5000); // Wait for at least one EWMA rate tick
                block.Set();
            });
            block.WaitOne();

            meter.Write();

            Console.ReadKey(true);
        }
    }
}
