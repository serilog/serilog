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
                .WithConsoleSink()
                .WithCouchDBSink("http://localhost:5984/log/")
                .CreateLogger();

            ProcessInput(new Position { Lat = 24.7, Long = 132.2 });

            Console.ReadKey(true);
        }

        static void ProcessInput(Position sensorInput)
        {
            var sw = new Stopwatch();
            sw.Start();
            Console.WriteLine("Processing some input...");
            Thread.Sleep(10);
            sw.Stop();

            Log.Information("Processed {@SensorInput} in {Time} ms", sensorInput, sw.ElapsedMilliseconds);
        }
    }
}
