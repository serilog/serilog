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
| SimulateAAppWithoutSerilog |    259.2 us |     72.88 us |     3.99 us |   1.00 |    0.00 |   24.9023 |   3.9063 |     - |   128.29 KB |
| SimulateAAppWithSerilogOff |  1,595.7 us |    440.51 us |    24.15 us |   6.16 |    0.18 |  208.9844 |   1.9531 |     - |  1071.64 KB |
|  SimulateAAppWithSerilogOn | 88,856.0 us | 18,420.90 us | 1,009.71 us | 342.85 |    8.27 | 5500.0000 | 166.6667 |     - | 28336.65 KB |
