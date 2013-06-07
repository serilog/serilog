namespace Serilog.PerformanceTests
{
    class Program
    {
        static void Main()
        {
            const int iterations = 10000000;
            var log = new LoggerConfiguration().CreateLogger();
            for (var i = 0; i < iterations; i++)
            {
                log.Information("Running iteration {I}", i);
            }
        }
    }
}
