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
            // To append to Couch or Mongo use-
            //   .WriteTo.CouchDB("http://localhost:5984/log/")
            //   .WriteTo.MongoDB("mongodb://localhost/logdb")

			// To append to the Windows event log use-
			//   .WriteTo.EventLog("Demo", "Serilog")
			//     where 'Demo' is the name of the source, as it appears in the event log, and 'Serilog' is the name of the event log written to- Appliction, by default.

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.ColoredConsole()
				.WriteTo.EventLog("Demo")
                .CreateLogger();

            Log.Verbose("This app, {ExeName}, demonstrates the basics of using Serilog", "Demo.exe");

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
