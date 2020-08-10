``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.388 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]   : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT
  ShortRun : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
|                     Method |        Mean |       Error |    StdDev |  Ratio | RatioSD |     Gen 0 |    Gen 1 | Gen 2 |   Allocated |
|--------------------------- |------------:|------------:|----------:|-------:|--------:|----------:|---------:|------:|------------:|
| SimulateAAppWithoutSerilog |    245.0 μs |    81.79 μs |   4.48 μs |   1.00 |    0.00 |   24.9023 |   3.9063 |     - |   128.29 KB |
| SimulateAAppWithSerilogOff |  1,439.7 μs |   360.08 μs |  19.74 μs |   5.88 |    0.05 |  208.9844 |   1.9531 |     - |  1071.64 KB |
|  SimulateAAppWithSerilogOn | 79,506.9 μs | 4,959.48 μs | 271.85 μs | 324.55 |    6.99 | 5571.4286 | 142.8571 |     - | 28923.98 KB |
