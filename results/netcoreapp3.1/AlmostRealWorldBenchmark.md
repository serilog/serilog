``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.301
  [Host]   : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT
  ShortRun : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
|                     Method |        Mean |        Error |      StdDev |  Ratio | RatioSD |     Gen 0 |    Gen 1 | Gen 2 |  Allocated |
|--------------------------- |------------:|-------------:|------------:|-------:|--------:|----------:|---------:|------:|-----------:|
| SimulateAAppWithoutSerilog |    149.4 us |     34.49 us |     1.89 us |   1.00 |    0.00 |    6.3477 |   0.7324 |     - |   39.16 KB |
| SimulateAAppWithSerilogOff |  1,602.9 us |  1,006.27 us |    55.16 us |  10.74 |    0.50 |  439.4531 |  54.6875 |     - |    2702 KB |
|  SimulateAAppWithSerilogOn | 55,558.4 us | 26,978.55 us | 1,478.79 us | 372.07 |   13.84 | 7666.6667 | 111.1111 |     - | 47214.6 KB |
