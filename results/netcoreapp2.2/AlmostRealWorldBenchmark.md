``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=2.2.402
  [Host]   : .NET Core 2.2.7 (CoreCLR 4.6.28008.02, CoreFX 4.6.28008.03), 64bit RyuJIT
  ShortRun : .NET Core 2.2.7 (CoreCLR 4.6.28008.02, CoreFX 4.6.28008.03), 64bit RyuJIT

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
|                     Method |        Mean |       Error |     StdDev |  Ratio | RatioSD |     Gen 0 |    Gen 1 | Gen 2 |   Allocated |
|--------------------------- |------------:|------------:|-----------:|-------:|--------:|----------:|---------:|------:|------------:|
| SimulateAAppWithoutSerilog |    150.0 us |    61.84 us |   3.390 us |   1.00 |    0.00 |    6.3477 |   0.4883 |     - |    39.16 KB |
| SimulateAAppWithSerilogOff |  1,561.0 us |   192.90 us |  10.574 us |  10.41 |    0.18 |  439.4531 |  48.8281 |     - |  2702.01 KB |
|  SimulateAAppWithSerilogOn | 58,521.1 us | 9,448.02 us | 517.878 us | 390.34 |   11.69 | 7666.6667 | 111.1111 |     - | 47684.02 KB |
