namespace Serilog.PerformanceTests;

[ShortRunJob, MemoryDiagnoser]
public class InitializationBenchmark
{
    static readonly LoggingLevelSwitch LoggingLevelSwitch = new(LogEventLevel.Verbose);

    [Params(0, 1, 4)]
    public int NumberOfSinks { get; set; }

    [Params(0, 1, 4)]
    public int NumberOfAuditSinks { get; set; }

    [Params(0, 1, 4)]
    public int NumberOfFilters { get; set; }

    [Params(0, 1, 4)]
    public int NumberOfProperties { get; set; }

    [Benchmark(Baseline = true)]
    public ILogger CreateBasicLogger()
    {
        return new LoggerConfiguration()
            .AddManyProperties(NumberOfProperties)
            .AddManyFilters(NumberOfFilters)
            .AddManyAuditSink(NumberOfAuditSinks)
            .AddManySink(NumberOfSinks)
            .CreateLogger();
    }

    [Benchmark]
    public ILogger CreateLoggerWithLogContext()
    {
        return new LoggerConfiguration()
            .Enrich.FromLogContext()
            .AddManyProperties(NumberOfProperties)
            .AddManyFilters(NumberOfFilters)
            .AddManyAuditSink(NumberOfAuditSinks)
            .AddManySink(NumberOfSinks)
            .CreateLogger();
    }

    [Benchmark]
    public ILogger CreateLoggerWithLoggingLevelSwitch()
    {
        return new LoggerConfiguration()
            .MinimumLevel.ControlledBy(LoggingLevelSwitch)
            .AddManyProperties(NumberOfProperties)
            .AddManyFilters(NumberOfFilters)
            .AddManyAuditSink(NumberOfAuditSinks)
            .AddManySink(NumberOfSinks)
            .CreateLogger();
    }

    [Benchmark]
    public ILogger CreateLoggerComplex()
    {
        return new LoggerConfiguration()
            .MinimumLevel.ControlledBy(LoggingLevelSwitch)
            .Enrich.FromLogContext()
            .AddManyProperties(NumberOfProperties)
            .AddManyFilters(NumberOfFilters)
            .AddManyAuditSink(NumberOfAuditSinks)
            .AddManySink(NumberOfSinks)
            .CreateLogger();
    }
}
