namespace Serilog.PerformanceTests;

[ShortRunJob, MemoryDiagnoser]
public class InitializationBenchmark
{
    static readonly LoggingLevelSwitch LoggingLevelSwitch = new(LogEventLevel.Verbose);

    [Params(0, 1, 4, 100)]
    public int NumberOfSinksFiltersAndProps { get; set; }

    [Benchmark]
    public ILogger CreateLogger()
    {
        return new LoggerConfiguration()
            .MinimumLevel.ControlledBy(LoggingLevelSwitch)
            .AddManyProperties(NumberOfSinksFiltersAndProps)
            .AddManyFilters(NumberOfSinksFiltersAndProps)
            .AddManyAuditSink(NumberOfSinksFiltersAndProps)
            .AddManySink(NumberOfSinksFiltersAndProps)
            .CreateLogger();
    }
}
