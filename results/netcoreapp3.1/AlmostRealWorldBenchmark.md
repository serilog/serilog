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
| SimulateAAppWithoutSerilog |    141.0 us |     71.97 us |     3.94 us |   1.00 |    0.00 |    6.3477 |   0.7324 |     - |   39.16 KB |
| SimulateAAppWithSerilogOff |  1,559.3 us |    648.29 us |    35.53 us |  11.06 |    0.21 |  439.4531 |  54.6875 |     - |    2702 KB |
|  SimulateAAppWithSerilogOn | 53,040.4 us | 29,875.48 us | 1,637.58 us | 376.11 |    1.97 | 7700.0000 | 100.0000 |     - | 47214.6 KB |
