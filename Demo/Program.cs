using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Demo
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
                .WriteTo.ColoredConsole()
                .WriteTo.Trace()
                .WriteTo.RollingFile("serilog_file_logging.txt")
                .CreateLogger();

            Log.Verbose("This app, {ExeName}, demonstrates the basics of using Serilog", "Demo.exe");

            var collection = new[] { "Item1", "Item2", "Item3" };
            Log.Information("I have a {Collection}", collection);

            var dict = new Dictionary<string, int> { { "Item1", 1 }, { "Item2", 5 } };
            Log.Information("I have a {Dict}", dict);

            ProcessInput(new Position { Lat = 24.7, Long = 132.2 });
            ProcessInput(new Position { Lat = 24.71, Long = 132.15 });
            ProcessInput(new Position { Lat = 24.72, Long = 132.2 });

            const int failureCount = 3;
            Log.Warning("Exception coming up because of {FailureCount} failures...", failureCount);

            try
            {
                DoBad();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "There's those {FailureCount} failures", failureCount);
            }

            Log.Fatal("That's all folks - and there was a fatal issue at {0}....", Environment.TickCount);
            Console.ReadLine();
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
            Log.Debug("Processing some input on {ThreadId}...", Environment.CurrentManagedThreadId);
            Thread.Sleep(Rng.Next(0, 100));
            sw.Stop();

            Log.Information("Processed {@SensorInput} in {Time:000} ms", sensorInput, sw.ElapsedMilliseconds);
        }
    }
}