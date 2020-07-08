``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.301
  [Host]   : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT
  ShortRun : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
|                     Method |        Mean |        Error |    StdDev |  Ratio | RatioSD |     Gen 0 |    Gen 1 | Gen 2 |  Allocated |
|--------------------------- |------------:|-------------:|----------:|-------:|--------:|----------:|---------:|------:|-----------:|
| SimulateAAppWithoutSerilog |    149.6 us |      6.11 us |   0.34 us |   1.00 |    0.00 |    6.3477 |   0.7324 |     - |   39.16 KB |
| SimulateAAppWithSerilogOff |  1,752.2 us |    594.84 us |  32.61 us |  11.71 |    0.23 |  439.4531 |  54.6875 |     - |    2702 KB |
|  SimulateAAppWithSerilogOn | 58,198.1 us | 14,488.99 us | 794.19 us | 389.03 |    6.16 | 7666.6667 | 111.1111 |     - | 47214.6 KB |
