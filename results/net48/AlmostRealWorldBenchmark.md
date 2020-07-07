``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]   : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.8.4010.0
  ShortRun : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.8.4010.0

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
|                     Method |        Mean |       Error |    StdDev |  Ratio | RatioSD |     Gen 0 |    Gen 1 | Gen 2 |   Allocated |
|--------------------------- |------------:|------------:|----------:|-------:|--------:|----------:|---------:|------:|------------:|
| SimulateAAppWithoutSerilog |    247.3 us |    31.51 us |  1.727 us |   1.00 |    0.00 |   24.9023 |   3.9063 |     - |   128.29 KB |
| SimulateAAppWithSerilogOff |  1,418.2 us |   166.45 us |  9.124 us |   5.74 |    0.00 |  208.9844 |   1.9531 |     - |  1071.64 KB |
|  SimulateAAppWithSerilogOn | 81,797.7 us | 1,280.92 us | 70.212 us | 330.82 |    2.60 | 5428.5714 | 142.8571 |     - | 28336.62 KB |
