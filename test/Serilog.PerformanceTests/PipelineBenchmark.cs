// Copyright 2013-2016 Serilog Contributors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using BenchmarkDotNet.Attributes;
using System;
using Serilog.PerformanceTests.Support;

namespace Serilog.PerformanceTests
{
    /// <summary>
    /// Tests the cost of writing through the logging pipeline.
    /// </summary>
    [MemoryDiagnoser]
    public class PipelineBenchmark : BaseBenchmark
    {
        readonly ILogger _log;
        readonly ILogger _logOnlyFatal;

        readonly ILogger _log1e0fc0lc;
        readonly ILogger _log0e1fc0lc;
        readonly ILogger _log0e0fc1lc;

        readonly ILogger _log1e1fc1lc;
        readonly ILogger _log10e10fc10lc;
        readonly ILogger _log100e100fc100lc;
        readonly ILogger _log1000e1000fc1000lc;
        
        readonly Exception _exception = new Exception("An Error");

        public PipelineBenchmark()
        {
            _log = new LoggerConfiguration()
                .WriteTo.Sink(new NullSink())
                .CreateLogger();

            _logOnlyFatal = new LoggerConfiguration()
                .MinimumLevel.Fatal()
                .WriteTo.Sink(new NullSink())
                .CreateLogger();


            _log1e0fc0lc = new LoggerConfiguration()
                .Enrich.AddManyProperties(1)
                .WriteTo.Sink(new NullSink())
                .CreateLogger();

            _log0e1fc0lc = new LoggerConfiguration()
                .WriteTo.Sink(new NullSink())
                .CreateLogger()
                .AddManyProperties(1);

            _log0e0fc1lc = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Sink(new NullSink())
                .CreateLogger();


            _log1e1fc1lc = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.AddManyProperties(1)
                .WriteTo.Sink(new NullSink())
                .CreateLogger()
                .AddManyProperties(1);

            _log10e10fc10lc = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.AddManyProperties(10)
                .WriteTo.Sink(new NullSink())
                .CreateLogger()
                .AddManyProperties(10);

            _log100e100fc100lc = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.AddManyProperties(100)
                .WriteTo.Sink(new NullSink())
                .CreateLogger()
                .AddManyProperties(100);

            _log1000e1000fc1000lc = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.AddManyProperties(1000)
                .WriteTo.Sink(new NullSink())
                .CreateLogger()
                .AddManyProperties(1000);

            // Ensure template is cached
            _log.Information(_exception, "Hello, {Name}!", "World");
            _logOnlyFatal.Information(_exception, "Hello, {Name}!", "World");

            _log1e0fc0lc.Information(_exception, "Hello, {Name}!", "World");
            _log0e1fc0lc.Information(_exception, "Hello, {Name}!", "World");
            using (PropertiesAdderHelper.ManyLogContext(1))
            {
                _log0e0fc1lc.Information(_exception, "Hello, {Name}!", "World");
            }

            using (PropertiesAdderHelper.ManyLogContext(1))
            {
                _log1e1fc1lc.Information(_exception, "Hello, {Name}!", "World");
            }
            using (PropertiesAdderHelper.ManyLogContext(10))
            {
                _log10e10fc10lc.Information(_exception, "Hello, {Name}!", "World");
            }
            using (PropertiesAdderHelper.ManyLogContext(100))
            {
                _log100e100fc100lc.Information(_exception, "Hello, {Name}!", "World");
            }
            using (PropertiesAdderHelper.ManyLogContext(100))
            {
                _log1000e1000fc1000lc.Information(_exception, "Hello, {Name}!", "World");
            }
        }

        [Benchmark(Baseline = true)]
        public void EmitLogAIgnoredEvent()
        {
            _logOnlyFatal.Information(_exception, "Hello, {Name}!", "World");
        }

        [Benchmark]
        public void EmitLogEvent()
        {
            _log.Information(_exception, "Hello, {Name}!", "World");
        }

        [Benchmark]
        public void EmitLogEventWith1Enrich0ForContext0LogContext()
        {
            _log1e0fc0lc.Information(_exception, "Hello, {Name}!", "World");
        }
        [Benchmark]
        public void EmitLogEventWith0Enrich1ForContext0LogContext()
        {
            _log0e1fc0lc.Information(_exception, "Hello, {Name}!", "World");
        }
        [Benchmark]
        public void EmitLogEventWith0Enrich0ForContext1LogContext()
        {
            using (PropertiesAdderHelper.ManyLogContext(1))
            {
                _log0e0fc1lc.Information(_exception, "Hello, {Name}!", "World");
            }
        }

        [Benchmark]
        public void EmitLogEventWith1Enrich1ForContext1LogContext()
        {
            using (PropertiesAdderHelper.ManyLogContext(1))
            {
                _log1e1fc1lc.Information(_exception, "Hello, {Name}!", "World");
            }
        }
        [Benchmark]
        public void EmitLogEventWith10Enrich10ForContext10LogContext()
        {
            using (PropertiesAdderHelper.ManyLogContext(10))
            {
                _log10e10fc10lc.Information(_exception, "Hello, {Name}!", "World");
            }
        }
        [Benchmark]
        public void EmitLogEventWith100Enrich100ForContext100LogContext()
        {
            using (PropertiesAdderHelper.ManyLogContext(1))
            {
                _log100e100fc100lc.Information(_exception, "Hello, {Name}!", "World");
            }
        }
        [Benchmark]
        public void EmitLogEventWith1000Enrich1000ForContext1000LogContext()
        {
            using (PropertiesAdderHelper.ManyLogContext(1))
            {
                _log1000e1000fc1000lc.Information(_exception, "Hello, {Name}!", "World");
            }
        }
    }
}
