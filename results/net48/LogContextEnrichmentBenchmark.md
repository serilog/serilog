``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.388 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT


```
|               Method |       Mean |     Error |    StdDev | Ratio | RatioSD |
|--------------------- |-----------:|----------:|----------:|------:|--------:|
|                 Bare |   9.714 ns | 0.2151 ns | 0.2012 ns |  1.00 |    0.00 |
|         PushProperty |  76.259 ns | 1.4304 ns | 1.9580 ns |  7.87 |    0.26 |
|   PushPropertyNested | 149.169 ns | 1.5976 ns | 1.4944 ns | 15.36 |    0.32 |
| PushPropertyEnriched | 159.549 ns | 3.0389 ns | 3.2516 ns | 16.43 |    0.56 |
