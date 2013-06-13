using System;

namespace Serilog.PerformanceTests
{
    class Program
    {
        static void Main()
        {
            const int iterations = 1000000;
            var log = new LoggerConfiguration()
                .WriteTo.File("test-" + Guid.NewGuid() + ".log")
                .CreateLogger();
            for (var i = 0; i < iterations; i++)
            {
                log.Information("Running iteration {I:00.0} for {@J}!", i, new { Goal = "Speed" });
            }
        }
    }
}
