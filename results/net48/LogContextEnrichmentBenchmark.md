``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT


```
|               Method |       Mean |     Error |    StdDev | Ratio | RatioSD |
|--------------------- |-----------:|----------:|----------:|------:|--------:|
|                 Bare |   9.822 ns | 0.0996 ns | 0.0932 ns |  1.00 |    0.00 |
|         PushProperty |  80.039 ns | 1.1868 ns | 1.1102 ns |  8.15 |    0.14 |
|   PushPropertyNested | 160.471 ns | 2.7639 ns | 2.5853 ns | 16.34 |    0.30 |
| PushPropertyEnriched | 166.077 ns | 1.7834 ns | 1.6682 ns | 16.91 |    0.20 |
