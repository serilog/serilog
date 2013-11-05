using System;
using System.Linq;
using Serilog;
using Serilog.Enrichers;
using Serilog.Sinks.Splunk;
using Splunk;

namespace Serilig.SplunkSample
{
    public class Program
    {
        private const string OutputTemplate =
            "[SERILOG] {Timestamp:G} ({ThreadId}) {Level} {SourceContext:l} - {Message:l}{NewLine:l}{Exception:l}";

        private static void Main(string[] args)
        {
            var host = "192.168.93.128";
            var userName = "serilog";
            var password = "serilog";

            var connectArgs = new ServiceArgs { Host = host, };

            var splunkConnectionInfoInfo = new SplunkConnectionInfo
            {
                ServiceArgs = connectArgs,
                UserName = userName,
                Password = password,
                SplunkEventType = "Serilog",
                SplunkSource = "Serilog.SplunkSample"
            };

            Log.Logger = new LoggerConfiguration()
                .Enrich.With(new ThreadIdEnricher())
                .WriteTo.ColoredConsole(outputTemplate: OutputTemplate)
                .WriteTo.Splunk(splunkConnectionInfoInfo, 3, TimeSpan.FromSeconds(1))
                .CreateLogger();

            var serilogLogger = Log.ForContext<Program>();
            var username = Environment.UserName;

            serilogLogger.Information("Hello from Serilog, running as {Username}!", username);

            var items = Enumerable.Range(1,10);

            foreach (var item in items)
            {
               serilogLogger.Information("Logging an int, what fun {item}", item);
            }

            System.Console.WriteLine("ok");
            Console.ReadLine();
        }
    }
}