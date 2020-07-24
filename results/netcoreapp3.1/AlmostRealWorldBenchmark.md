``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.302
  [Host]   : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT
  ShortRun : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
|                     Method |        Mean |       Error |    StdDev |  Ratio | RatioSD |     Gen 0 |    Gen 1 | Gen 2 |  Allocated |
|--------------------------- |------------:|------------:|----------:|-------:|--------:|----------:|---------:|------:|-----------:|
| SimulateAAppWithoutSerilog |    150.8 us |    37.64 us |   2.06 us |   1.00 |    0.00 |    6.3477 |   0.7324 |     - |   39.16 KB |
| SimulateAAppWithSerilogOff |  1,700.1 us |   188.51 us |  10.33 us |  11.28 |    0.22 |  439.4531 |  54.6875 |     - |    2702 KB |
|  SimulateAAppWithSerilogOn | 55,006.0 us | 4,273.47 us | 234.24 us | 364.89 |    5.83 | 7700.0000 | 100.0000 |     - | 47214.6 KB |
