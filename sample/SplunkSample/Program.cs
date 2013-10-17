using System;
using Serilog;
using Serilog.Enrichers;
using Serilog.Sinks.Splunk;
using Serilog.Sinks.Splunk.Sinks;
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
            var userName = "admin";
            var password = "Oujms813!)";

            var connectArgs = new ServiceArgs { Host = host, };

            var splunkConnectionInfoInfo = new SplunkConnectionInfoInfo
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
                .WriteTo.Splunk(splunkConnectionInfoInfo, 1, null)
                .CreateLogger();

            var serilogLogger = Log.ForContext<Program>();
            var username = Environment.UserName;

            serilogLogger.Information("Hello from Serilog, running as {Username}!", username);

            Console.WriteLine("ok");
            Console.ReadLine();
        }
    }
}