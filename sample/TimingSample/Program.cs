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
            Console.WriteLine("Press A for threshold sample, B for timers sample");
            var key = Console.ReadKey(true);
            switch (key.Key)
            {
                case ConsoleKey.A:

                    ThresholdSample();
                    break;

                case ConsoleKey.B:

                    TimersSample();
                    break;

            }
        }

        private static void ThresholdSample()
        {
            var logger = new LoggerConfiguration()
              .MinimumLevel.Debug()
              .WriteTo.ThresholdLogger(
                  bufferSize:3, 
                  threshHoldLevel:LogEventLevel.Error,
                  restrictedToMinimumLevel: LogEventLevel.Debug,
                  configureLogger: lc => lc
                      .WriteTo            
                      .ColoredConsole(LogEventLevel.Information, outputTemplate:"ColoredConsole sink: {Message:l}{NewLine:l}"))
              .WriteTo.Console()
              .Enrich.With(new ThreadIdEnricher(), new MachineNameEnricher())
              .CreateLogger();

            logger.Information("Starting logging system");
            logger.Information("Another message for the logs");
            logger.Information("We keep logging information events");
            logger.Information("The console logger will display them");
            logger.Information("When we generate an error, the ColoredConsole will receive the last 3 events, but can filter those based on level and filters.");
            logger.Debug("This is a debug message, the colored console will not show this one as it will only display Information and up.");
           
            logger.Error("This is the error, you should see this one in both the console and in the colored console and the colored console should also list the previous events.");

            Console.ReadKey(true);

        }

   
        private static void TimersSample()
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

          

            Console.ReadKey(true);
        }
    }
}
