``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-7700HQ CPU 2.80GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
  [Host]   : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.8.3815.0
  ShortRun : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.8.3815.0

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
|                  Method |       Mean |     Error |    StdDev | Ratio | RatioSD |     Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------------ |-----------:|----------:|----------:|------:|--------:|----------:|------:|------:|----------:|
|            SimulateAApp |   1.704 ms |  2.732 ms | 0.1498 ms |  1.00 |    0.00 |  345.7031 |     - |     - |   1.04 MB |
| SimulateAAppWithSerilog | 123.475 ms | 62.058 ms | 3.4016 ms | 72.91 |    7.77 | 9250.0000 |     - |     - |  28.26 MB |
