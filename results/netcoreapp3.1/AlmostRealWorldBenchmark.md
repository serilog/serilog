``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.450 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.401
  [Host]   : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT
  ShortRun : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
|                     Method |        Mean |        Error |    StdDev |  Ratio | RatioSD |     Gen 0 |   Gen 1 | Gen 2 |   Allocated |
|--------------------------- |------------:|-------------:|----------:|-------:|--------:|----------:|--------:|------:|------------:|
| SimulateAAppWithoutSerilog |    147.7 μs |     32.88 μs |   1.80 μs |   1.00 |    0.00 |    6.3477 |  0.7324 |     - |    39.16 KB |
| SimulateAAppWithSerilogOff |  1,527.6 μs |    377.54 μs |  20.69 μs |  10.34 |    0.06 |  439.4531 | 54.6875 |     - |     2702 KB |
|  SimulateAAppWithSerilogOn | 49,110.8 μs | 14,602.39 μs | 800.41 μs | 332.57 |    8.32 | 7545.4545 | 90.9091 |     - | 46507.96 KB |
