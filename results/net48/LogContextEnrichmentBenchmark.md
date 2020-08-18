``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.450 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4200.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8 (4.8.4200.0), X86 LegacyJIT


```
|               Method |       Mean |     Error |    StdDev | Ratio | RatioSD |
|--------------------- |-----------:|----------:|----------:|------:|--------:|
|                 Bare |   9.673 ns | 0.2159 ns | 0.2019 ns |  1.00 |    0.00 |
|         PushProperty |  77.217 ns | 1.5472 ns | 1.7818 ns |  8.01 |    0.21 |
|   PushPropertyNested | 143.303 ns | 2.8797 ns | 3.9418 ns | 14.85 |    0.53 |
| PushPropertyEnriched | 159.235 ns | 3.1862 ns | 3.7930 ns | 16.46 |    0.62 |
