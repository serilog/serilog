using System;
using System.Diagnostics;
using System.Threading;
using Serilog;

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
                .WriteTo.Console()
                .WriteTo.CouchDB("http://localhost:5984/log/")
                .WriteTo.MongoDB("mongodb://localhost/logdb")
                .CreateLogger();

            ProcessInput(new Position { Lat = 24.7, Long = 132.2 });
            ProcessInput(new Position { Lat = 24.71, Long = 132.15 });
            ProcessInput(new Position { Lat = 24.72, Long = 132.2 });

            Console.ReadKey(true);
        }

        static readonly Random Rng = new Random();

        static void ProcessInput(Position sensorInput)
        {
            var sw = new Stopwatch();
            sw.Start();
            Console.WriteLine("Processing some input...");
            Thread.Sleep(Rng.Next(0, 100));
            sw.Stop();

            Log.Information("Processed {@SensorInput} in {Time} ms", sensorInput, sw.ElapsedMilliseconds);
        }
    }
}
