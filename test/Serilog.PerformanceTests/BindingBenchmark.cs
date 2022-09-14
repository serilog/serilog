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

namespace Serilog.PerformanceTests;

/// <summary>
/// Tests the cost of writing through the logging pipeline.
/// </summary>
[MemoryDiagnoser]
public class BindingBenchmark
{
    const string
        MT0 = "Zero",
        MT1 = "Zero{A}one",
        MT5 = "Zero{A}one{B}two{C}three{D}four{E}five";

    ILogger _log = null!;
    object[] _zero= null!, _one= null!, _five = null!;

    [GlobalSetup]
    public void Setup()
    {
        _log = new LoggerConfiguration()
            .WriteTo.Sink(new NullSink())
            .CreateLogger();

        _zero = Array.Empty<object>();
        _one = new object[] { 1 };
        _five = new object[] { 1, 2, 3, 4, 5 };
    }

    // The benchmarks run p.Count() to force enumeration; this will be representative of how the API
    // is consumed (there's not much point benchmarking time to return a lazy enumerator).

    [Benchmark(Baseline = true)]
    public (MessageTemplate, int) BindZero()
    {
        _log.BindMessageTemplate(MT0, _zero, out var mt, out var p);
        return (mt, p!.Count())!;
    }

    [Benchmark]
    public (MessageTemplate, int) BindOne()
    {
        _log.BindMessageTemplate(MT1, _one, out var mt, out var p);
        return (mt, p!.Count())!;
    }

    [Benchmark]
    public (MessageTemplate, int) BindFive()
    {
        _log.BindMessageTemplate(MT5, _five, out var mt, out var p);
        return (mt, p!.Count())!;
    }
}
