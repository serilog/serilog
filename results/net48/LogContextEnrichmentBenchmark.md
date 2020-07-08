``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT


```
|               Method |       Mean |     Error |    StdDev | Ratio | RatioSD |
|--------------------- |-----------:|----------:|----------:|------:|--------:|
|                 Bare |   9.907 ns | 0.0226 ns | 0.0200 ns |  1.00 |    0.00 |
|         PushProperty |  82.229 ns | 1.2217 ns | 1.1427 ns |  8.29 |    0.11 |
|   PushPropertyNested | 163.711 ns | 2.6741 ns | 2.5014 ns | 16.51 |    0.24 |
| PushPropertyEnriched | 170.015 ns | 2.0104 ns | 1.7822 ns | 17.16 |    0.19 |
