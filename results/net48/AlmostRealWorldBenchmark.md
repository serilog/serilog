``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-7700HQ CPU 2.80GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
  [Host]   : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.8.3815.0
  ShortRun : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.8.3815.0

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
|      Method |     Mean |    Error |   StdDev | Ratio |     Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------ |---------:|---------:|---------:|------:|----------:|------:|------:|----------:|
| LogLikeAApp | 96.16 ms | 50.81 ms | 2.785 ms |  1.00 | 9166.6667 |     - |     - |  27.68 MB |
