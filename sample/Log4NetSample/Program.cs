using System;
using Serilog;
using Serilog.Enrichers;
using log4net;
using log4net.Config;

namespace Log4NetSample
{
    class Program
    {
        const string OutputTemplate = "[SERILOG] {Timestamp:G} ({ThreadId}) {Level} {SourceContext} - {Message}{NewLine}{Exception}";
        
        static void Main()
        {
            XmlConfigurator.Configure();

            Log.Logger = new LoggerConfiguration()
                .Enrich.With(new ThreadIdEnricher())
                .WriteTo.ColoredConsole(outputTemplate: OutputTemplate)
                .WriteTo.Log4Net()
                .CreateLogger();


            var log4NetLogger = LogManager.GetLogger(typeof(Program));
            var serilogLogger = Log.ForContext<Program>();

            var username = Environment.UserName;

            log4NetLogger.InfoFormat("Hello from log4net, running as {0}!", username);
            serilogLogger.Information("Hello from Serilog, running as {Username}!", username);

            Console.ReadKey(true);
        }
    }
}
