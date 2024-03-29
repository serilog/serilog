namespace Serilog.PerformanceTests;

[SimpleJob(RuntimeMoniker.NetCoreApp21, baseline: true)]
[SimpleJob(RuntimeMoniker.NetCoreApp31)]
public class SourceContextMatchBenchmark
{
    readonly LevelOverrideMap _levelOverrideMap;
    readonly Logger _loggerWithOverrides;
    readonly List<ILogger> _loggersWithFilters = new();
    readonly LogEvent _event = Some.InformationEvent();
    readonly string[] _contexts =
        [
            "Serilog",
            "MyApp",
            "MyAppSomething",
            "MyOtherApp",
            "MyApp.Something",
            "MyApp.Api.Models.Person",
            "MyApp.Api.Controllers.AboutController",
            "MyApp.Api.Controllers.HomeController",
            "Api.Controllers.HomeController"
        ];

    public SourceContextMatchBenchmark()
    {
        var overrides = new Dictionary<string, LoggingLevelSwitch>
        {
            ["MyApp"] = new(LogEventLevel.Debug),
            ["MyApp.Api.Controllers"] = new(LogEventLevel.Information),
            ["MyApp.Api.Controllers.HomeController"] = new(LogEventLevel.Warning),
            ["MyApp.Api"] = new(LogEventLevel.Error)
        };

        _levelOverrideMap = new(overrides, LogEventLevel.Fatal, null);

        var loggerConfiguration = new LoggerConfiguration().MinimumLevel.Fatal();

        foreach (var @override in overrides)
        {
            loggerConfiguration = loggerConfiguration.MinimumLevel.Override(@override.Key, @override.Value);

            foreach (var ctx in _contexts)
            {
                _loggersWithFilters.Add(
                    new LoggerConfiguration().MinimumLevel.Verbose()
                        .Filter.ByIncludingOnly(Matching.FromSource(@override.Key))
                        .WriteTo.Sink<NullSink>()
                        .CreateLogger()
                        .ForContext(Constants.SourceContextPropertyName, ctx));
            }
        }

        _loggerWithOverrides = loggerConfiguration.WriteTo.Sink<NullSink>().CreateLogger();
    }

    [Benchmark]
    public void Filter_MatchingFromSource()
    {
        for (var i = 0; i < _loggersWithFilters.Count; ++i)
        {
            _loggersWithFilters[i].Write(_event);
        }
    }

    [Benchmark]
    public void Logger_ForContext()
    {
        for (var i = 0; i < _contexts.Length; ++i)
        {
            _loggerWithOverrides.ForContext(Constants.SourceContextPropertyName, _contexts[i]);
        }
    }

    [Benchmark]
    public void LevelOverrideMap_GetEffectiveLevel()
    {
        for (var i = 0; i < _contexts.Length; ++i)
        {
            _levelOverrideMap.GetEffectiveLevel(_contexts[i], out _, out _);
        }
    }
}
