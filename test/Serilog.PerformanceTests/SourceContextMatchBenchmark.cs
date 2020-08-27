using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using Serilog.Core;
using Serilog.Events;
using Serilog.Filters;
using Serilog.PerformanceTests.Support;

namespace Serilog.PerformanceTests
{
    [MyBenchmarkRun(MyConfigs.SpanCompare)]
    public class SourceContextMatchBenchmark : BaseBenchmark
    {
        readonly LevelOverrideMap _levelOverrideMap;
        readonly Logger _loggerWithOverrides;
        readonly List<ILogger> _loggersWithFilters = new List<ILogger>();
        readonly LogEvent _event = Some.InformationEvent();
        readonly string[] _contexts;

        public SourceContextMatchBenchmark()
        {
            _contexts = new[]
            {
                "Serilog",
                "MyApp",
                "MyAppSomething",
                "MyOtherApp",
                "MyApp.Something",
                "MyApp.Api.Models.Person",
                "MyApp.Api.Controllers.AboutController",
                "MyApp.Api.Controllers.HomeController",
                "Api.Controllers.HomeController"
            };

            var overrides = new Dictionary<string, LoggingLevelSwitch>
            {
                ["MyApp"] = new LoggingLevelSwitch(LogEventLevel.Debug),
                ["MyApp.Api.Controllers"] = new LoggingLevelSwitch(LogEventLevel.Information),
                ["MyApp.Api.Controllers.HomeController"] = new LoggingLevelSwitch(LogEventLevel.Warning),
                ["MyApp.Api"] = new LoggingLevelSwitch(LogEventLevel.Error)
            };

            _levelOverrideMap = new LevelOverrideMap(overrides, LogEventLevel.Fatal, null);

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
}
