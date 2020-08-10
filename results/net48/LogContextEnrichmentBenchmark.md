``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.388 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT


```
|               Method |       Mean |     Error |    StdDev | Ratio | RatioSD |
|--------------------- |-----------:|----------:|----------:|------:|--------:|
|                 Bare |   9.739 ns | 0.1861 ns | 0.1741 ns |  1.00 |    0.00 |
|         PushProperty |  75.607 ns | 1.4933 ns | 1.7197 ns |  7.77 |    0.26 |
|   PushPropertyNested | 146.950 ns | 2.8806 ns | 4.1313 ns | 15.08 |    0.47 |
| PushPropertyEnriched | 158.601 ns | 3.1250 ns | 3.3438 ns | 16.27 |    0.47 |
