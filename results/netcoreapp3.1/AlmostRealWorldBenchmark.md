``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.388 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.302
  [Host]   : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT
  ShortRun : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
|                     Method |        Mean |       Error |    StdDev |  Ratio | RatioSD |     Gen 0 |   Gen 1 | Gen 2 |   Allocated |
|--------------------------- |------------:|------------:|----------:|-------:|--------:|----------:|--------:|------:|------------:|
| SimulateAAppWithoutSerilog |    147.0 μs |    104.6 μs |   5.73 μs |   1.00 |    0.00 |    6.3477 |  0.7324 |     - |    39.16 KB |
| SimulateAAppWithSerilogOff |  1,486.4 μs |    655.3 μs |  35.92 μs |  10.13 |    0.62 |  439.4531 | 54.6875 |     - |     2702 KB |
|  SimulateAAppWithSerilogOn | 48,317.9 μs | 16,965.1 μs | 929.91 μs | 328.88 |    8.77 | 7545.4545 | 90.9091 |     - | 46510.76 KB |
