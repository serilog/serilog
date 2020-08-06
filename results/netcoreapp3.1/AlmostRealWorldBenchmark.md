``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.388 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.302
  [Host]   : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT
  ShortRun : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
|                     Method |        Mean |        Error |      StdDev |  Ratio | RatioSD |     Gen 0 |    Gen 1 | Gen 2 |   Allocated |
|--------------------------- |------------:|-------------:|------------:|-------:|--------:|----------:|---------:|------:|------------:|
| SimulateAAppWithoutSerilog |    141.7 μs |     37.37 μs |     2.05 μs |   1.00 |    0.00 |    6.3477 |   0.7324 |     - |    39.16 KB |
| SimulateAAppWithSerilogOff |  1,468.0 μs |    283.90 μs |    15.56 μs |  10.36 |    0.15 |  439.4531 |  54.6875 |     - |     2702 KB |
|  SimulateAAppWithSerilogOn | 52,736.2 μs | 29,102.65 μs | 1,595.21 μs | 372.23 |   15.53 | 7700.0000 | 100.0000 |     - | 47214.61 KB |
