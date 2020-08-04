``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.388 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.302
  [Host]   : .NET Core 2.1.20 (CoreCLR 4.6.29017.01, CoreFX 4.6.29018.12), X64 RyuJIT
  ShortRun : .NET Core 2.1.20 (CoreCLR 4.6.29017.01, CoreFX 4.6.29018.12), X64 RyuJIT

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
|                     Method |        Mean |        Error |    StdDev |  Ratio | RatioSD |     Gen 0 |    Gen 1 | Gen 2 |   Allocated |
|--------------------------- |------------:|-------------:|----------:|-------:|--------:|----------:|---------:|------:|------------:|
| SimulateAAppWithoutSerilog |    147.5 μs |     83.54 μs |   4.58 μs |   1.00 |    0.00 |    6.3477 |   0.4883 |     - |    39.16 KB |
| SimulateAAppWithSerilogOff |  1,535.3 μs |    505.13 μs |  27.69 μs |  10.42 |    0.52 |  439.4531 |  48.8281 |     - |  2702.01 KB |
|  SimulateAAppWithSerilogOn | 56,015.0 μs | 17,938.25 μs | 983.26 μs | 379.93 |    9.51 | 7700.0000 | 100.0000 |     - | 47684.02 KB |
