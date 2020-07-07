``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.8.4010.0
  DefaultJob : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.8.4010.0


```
|               Method |       Mean |     Error |    StdDev | Ratio | RatioSD |
|--------------------- |-----------:|----------:|----------:|------:|--------:|
|                 Bare |   9.194 ns | 0.0683 ns | 0.0638 ns |  1.00 |    0.00 |
|         PushProperty |  76.910 ns | 0.7857 ns | 0.7349 ns |  8.37 |    0.09 |
|   PushPropertyNested | 145.741 ns | 1.1252 ns | 0.9396 ns | 15.83 |    0.14 |
| PushPropertyEnriched | 157.526 ns | 0.9964 ns | 0.8833 ns | 17.12 |    0.18 |
