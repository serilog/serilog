``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-7700HQ CPU 2.80GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
  [Host]   : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.8.4010.0
  ShortRun : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.8.4010.0

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
|                     Method |        Mean |        Error |       StdDev |  Ratio | RatioSD |     Gen 0 | Gen 1 | Gen 2 |  Allocated |
|--------------------------- |------------:|-------------:|-------------:|-------:|--------:|----------:|------:|------:|-----------:|
| SimulateAAppWithoutSerilog |    280.4 us |     29.70 us |     1.628 us |   1.00 |    0.00 |   41.5039 |     - |     - |  128.27 KB |
| SimulateAAppWithSerilogOff |  1,623.2 us |     93.29 us |     5.113 us |   5.79 |    0.04 |  349.6094 |     - |     - | 1077.13 KB |
|  SimulateAAppWithSerilogOn | 98,093.9 us | 81,013.51 us | 4,440.624 us | 349.88 |   15.12 | 9166.6667 |     - |     - | 28348.8 KB |
