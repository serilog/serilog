``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.450 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]   : .NET Framework 4.8 (4.8.4200.0), X86 LegacyJIT
  ShortRun : .NET Framework 4.8 (4.8.4200.0), X86 LegacyJIT

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
|                     Method |        Mean |       Error |      StdDev |  Ratio | RatioSD |     Gen 0 |    Gen 1 | Gen 2 |   Allocated |
|--------------------------- |------------:|------------:|------------:|-------:|--------:|----------:|---------:|------:|------------:|
| SimulateAAppWithoutSerilog |    253.8 μs |    179.4 μs |     9.84 μs |   1.00 |    0.00 |   24.9023 |   3.9063 |     - |   128.29 KB |
| SimulateAAppWithSerilogOff |  1,419.4 μs |    494.4 μs |    27.10 μs |   5.60 |    0.22 |  208.9844 |   1.9531 |     - |  1071.66 KB |
|  SimulateAAppWithSerilogOn | 88,346.1 μs | 53,753.5 μs | 2,946.41 μs | 348.25 |    8.51 | 7166.6667 | 166.6667 |     - | 37025.06 KB |
