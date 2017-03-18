using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Threading.Tasks;
using Serilog.PerformanceTests.Support;
using Xunit;

namespace Serilog.PerformanceTests
{
    /// <summary>
    /// Tests the overhead of determining the active logging level.
    /// </summary>
    public class LevelControlBenchmark
    {
        ILogger _off, _levelSwitchOff, _minLevel, _levelSwitch;
        readonly LogEvent _event = Some.InformationEvent();

        [Setup]
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
        public Task Off()
        {
            return _off.Write(_event);
        }  
        
        [Benchmark]
        public Task LevelSwitchOff()
        {
            return _levelSwitchOff.Write(_event);
        } 
                
        [Benchmark]
        public Task MinimumLevelOn()
        {
            return _minLevel.Write(_event);
        }

        [Benchmark]
        public Task LevelSwitchOn()
        {
            return _levelSwitch.Write(_event);
        }
    }
}
  