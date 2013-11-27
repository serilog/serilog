using System;
using Serilog;
using Serilog.Enrichers;

namespace Harness
{
    class Program
    {
        static void Main()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console(
                    outputTemplate: "{Timestamp:HH:mm:ss} ({ThreadId}) [{Level}] {?Id:[0] }{Message:l}{NewLine:l}{Exception:l}")
                .WriteTo.DumpFile("Dumps\\" + DateTime.Now.Ticks + ".slog")
                .WriteTo.Trace()
                .Enrich.WithProperty("App", "Test Harness")
                .Enrich.With(new ThreadIdEnricher(),
                             new MachineNameEnricher())
                .CreateLogger();

            Log.Information("Just biting {Fruit} number {Count}", "Apple", 12);
            Log.ForContext<Program>().Information("Just biting {Fruit} number {Count:0000}", "Apple", 12);
            
            Log.Information("{?Scope1}{?Scope2}", new { Scope1 = "Test Scope"});
            Log.ForContext("Id", 1234).Information("This should have an id");

            // ReSharper disable CoVariantArrayConversion
            Log.Information("I've eaten {Dinner}", new[] { "potatoes", "peas" });
            // ReSharper restore CoVariantArrayConversion
            
            Log.Information("I sat at {@Chair}", new { Back = "straight", Legs = new[] { 1, 2, 3, 4 } });
            Log.Information("I sat at {Chair}", new { Back = "straight", Legs = new[] { 1, 2, 3, 4 } });

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
            
            Console.ReadKey(true);
        }
    }
}
