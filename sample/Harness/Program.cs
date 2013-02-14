using System;
using Opi;

namespace Harness
{
    class Program
    {
        static void Main()
        {
            Log.Logger = new LoggerConfiguration()
                .WithConsoleSink()
                .WithDumpFile("Dumps\\" + DateTime.Now.Ticks + ".slog")
                .WithFixedProperty("app", "Test Harness")
                .EnrichedBy(new ThreadIdEnricher())
                .CreateLogger();

            Log.Information("Just biting {Fruit} number {Count}", "Apple", 12);
            Log.Information("Just biting {Fruit} number {Count:0000}", "Apple", 12);
            Log.Information("I've eaten {Dinner}", new[] { "potatoes", "peas" });
            Log.Information("I sat at {Chair}", new { Back = "straight", Legs = new[] { 1, 2, 3, 4 } });
            Log.Information("I sat at {Chair:s}", new { Back = "straight", Legs = new[] { 1, 2, 3, 4 } });

            var context = Log.Logger.CreateContext(new LogEventProperty("uow", LogEventPropertyValue.For(567)));
            context.Information("Processing a message");
            context.Warning("Rolling back transaction!");

            Console.ReadKey(true);
        }
    }
}
