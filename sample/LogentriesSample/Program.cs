using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Serilog;

namespace LogentriesSample
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
                .Enrich.WithProperty("UserName", Environment.UserName)
                .WriteTo.ColoredConsole()
                .WriteTo.Logentries("<token>")       // Enter the input key here
                .CreateLogger();

            Log.Verbose("This app, {ExeName}, demonstrates the basics of using Serilog", "Demo.exe");

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

            Log.Fatal("That's all folks - and all done using {WorkingSet} bytes of RAM", Environment.WorkingSet);

            // Loop
            for (int i = 0; i < 100; i++)
            {
                Log.Information("Counter {Counter} to {Max}", i, 100);
                System.Threading.Thread.Sleep(500);
            }

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
