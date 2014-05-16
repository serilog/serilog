using System;
using System.Diagnostics;
using System.Threading;
using Serilog;
using Serilog.Enrichers;

namespace ElasticSearchSample
{
    class Position
    {
        public double Lat { get; set; }
        public double Long { get; set; }
    }

    class Program
    {
        static void Main()
        {
       
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.ColoredConsole()
                .WriteTo.ElasticSearch()
                .Enrich.With(new ThreadIdEnricher(),
                             new MachineNameEnricher())
                .CreateLogger();

            Log.Verbose("This app, {ExeName}, demonstrates the basics of using Serilog", "Demo.exe");

            ProcessInput(new Position { Lat = 24.7, Long = 132.2 });
            ProcessInput(new Position { Lat = 24.71, Long = 132.15 });
            ProcessInput(new Position { Lat = 24.72, Long = 132.2 });

            const int failureCount = 3;
            Log.Warning("Exception coming up because of {FailureCount} failures...", failureCount);

            Log.ForContext<Program>().Information("Just biting {Fruit} number {Count:0000}", "Apple", 12);

            // ReSharper disable CoVariantArrayConversion
            Log.Information("I've eaten {Dinner}", new[] { "potatoes", "peas" });
            // ReSharper restore CoVariantArrayConversion

          //  Log.Information("I sat at {@Chair}", new { Back = "straight", Legs = new[] { 1, 2, 3, 4 } });
            Log.Information("I sat at {Chair} in {TimeSpan}", new { Back = "straight", Legs = new[] { 1, 2, 3, 4 } }, TimeSpan.FromHours(3));

            var context = Log.Logger.ForContext("MessageId", 567);
            try
            {
                context.Information("Processing a message");
                throw new NotImplementedException("Nothing doing.");
            }
            catch (Exception ex)
            {
                context.Error(ex, "Rolling back transaction!");
            }

            try
            {
                DoBad();
            }
            catch (Exception ex)
            {
                var ex2 = new InvalidOperationException("Something went wrong", ex);
                Log.Error(ex2, "There's those {FailureCount} failures", failureCount);
            }

            Log.Fatal("That's all folks - and all done using {WorkingSet} bytes of RAM", Environment.WorkingSet);
            Console.ReadKey(true);
        }

        static void DoBad()
        {
            throw new InvalidOperationException("Everything's broken!");
        }

        static readonly Random Rng = new Random();

        static void ProcessInput(Position sensorInput)
        {
            var sw = new Stopwatch();
            sw.Start();
            Log.Debug("Processing some input on {MachineName}...", Environment.MachineName);
            Thread.Sleep(Rng.Next(0, 100));
            sw.Stop();

            Log.Information("Processed {@SensorInput} in {Time:000} ms", sensorInput, sw.ElapsedMilliseconds);
        }
    }
}
