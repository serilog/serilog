namespace Serilog.PerformanceTests;

/// <summary>
/// Tests the overhead of determining the active logging level.
/// </summary>
public class LevelControlBenchmark
{
    ILogger _off = null!, _levelSwitchOff = null!, _minLevel = null!, _levelSwitch = null!;
    readonly LogEvent _event = Some.InformationEvent();

    [GlobalSetup]
    public void Setup()
    {
        _off = new LoggerConfiguration()
            .MinimumLevel.Fatal()
            .WriteTo.Sink(new NullSink())
            .CreateLogger();
        _levelSwitchOff = new LoggerConfiguration()
            .MinimumLevel.ControlledBy(new LoggingLevelSwitch(LogEventLevel.Fatal))
            .WriteTo.Sink(new NullSink())
            .CreateLogger();
        _minLevel = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Sink(new NullSink())
            .CreateLogger();
        _levelSwitch = new LoggerConfiguration()
            .MinimumLevel.ControlledBy(new LoggingLevelSwitch(LogEventLevel.Information))
            .WriteTo.Sink(new NullSink())
            .CreateLogger();
    }

    [Benchmark(Baseline = true)]
    public void Off()
    {
        _off.Write(_event);
    }

    [Benchmark]
    public void LevelSwitchOff()
    {
        _levelSwitchOff.Write(_event);
    }

    [Benchmark]
    public void MinimumLevelOn()
    {
        _minLevel.Write(_event);
    }

    [Benchmark]
    public void LevelSwitchOn()
    {
        _levelSwitch.Write(_event);
    }
}
