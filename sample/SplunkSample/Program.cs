using System;
using System.Linq;
using Serilog;
using Serilog.Enrichers;
using Serilog.Sinks.Splunk;
using Splunk;

namespace SplunkSample
{
    public class Program
    {
        static void Main()
        {
            const string host = "192.168.93.128";
            const string username = "serilog";
            const string password = "serilog";

            var connectArgs = new ServiceArgs { Host = host, };

            var splunkConnectionInfoInfo = new SplunkConnectionInfo
            {
                ServiceArgs = connectArgs,
                Username = username,
                Password = password,
                SplunkEventType = "Serilog",
                SplunkSource = "Serilog.SplunkSample"
            };

            Log.Logger = new LoggerConfiguration()
                .Enrich.With(new ThreadIdEnricher())
                .WriteTo.ColoredConsole()
                .WriteTo.Splunk(splunkConnectionInfoInfo, 3, TimeSpan.FromSeconds(1))
                .CreateLogger();

            var serilogLogger = Log.ForContext<Program>();

            serilogLogger.Information("Hello from Serilog, running as {Username}!", Environment.UserName);

            var items = Enumerable.Range(1,10);

            foreach (var item in items)
            {
               serilogLogger.Information("Logging an int, what fun {Item}", item);
            }

            Console.WriteLine("OK");
            Console.ReadLine();
        }
    }
}