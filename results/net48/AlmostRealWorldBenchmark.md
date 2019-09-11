``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-7700HQ CPU 2.80GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
  [Host]   : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.8.3815.0
  ShortRun : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.8.3815.0

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
|                     Method |       Mean |      Error |     StdDev | Ratio | RatioSD |     Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------------- |-----------:|-----------:|-----------:|------:|--------:|----------:|------:|------:|----------:|
| SimulateAAppWithoutSerilog |   1.758 ms |   3.054 ms |  0.1674 ms |  1.00 |    0.00 |  345.7031 |     - |     - |   1.04 MB |
| SimulateAAppWithSerilogOff |   2.382 ms |   1.817 ms |  0.0996 ms |  1.36 |    0.11 |  347.6563 |     - |     - |   1.05 MB |
|  SimulateAAppWithSerilogOn | 123.354 ms | 351.945 ms | 19.2913 ms | 69.92 |    4.80 | 9200.0000 |     - |     - |  27.68 MB |
