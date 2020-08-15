``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.450 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4200.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8 (4.8.4200.0), X86 LegacyJIT


```
|               Method |       Mean |     Error |    StdDev | Ratio | RatioSD |
|--------------------- |-----------:|----------:|----------:|------:|--------:|
|                 Bare |   9.764 ns | 0.1599 ns | 0.1496 ns |  1.00 |    0.00 |
|         PushProperty |  77.680 ns | 1.5586 ns | 1.6006 ns |  7.96 |    0.22 |
|   PushPropertyNested | 143.932 ns | 2.1831 ns | 2.0421 ns | 14.74 |    0.28 |
| PushPropertyEnriched | 161.793 ns | 3.2181 ns | 3.3047 ns | 16.56 |    0.48 |
