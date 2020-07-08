``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]   : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT
  ShortRun : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
|                     Method |        Mean |        Error |      StdDev |  Ratio | RatioSD |     Gen 0 |    Gen 1 | Gen 2 |   Allocated |
|--------------------------- |------------:|-------------:|------------:|-------:|--------:|----------:|---------:|------:|------------:|
| SimulateAAppWithoutSerilog |    271.0 us |     75.35 us |     4.13 us |   1.00 |    0.00 |   24.9023 |   3.9063 |     - |   128.29 KB |
| SimulateAAppWithSerilogOff |  1,632.1 us |  1,800.97 us |    98.72 us |   6.03 |    0.45 |  208.9844 |   1.9531 |     - |  1071.64 KB |
|  SimulateAAppWithSerilogOn | 92,916.1 us | 30,396.92 us | 1,666.16 us | 342.97 |   11.15 | 5500.0000 | 166.6667 |     - | 28336.65 KB |
