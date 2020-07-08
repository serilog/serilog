``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT


```
|               Method |      Mean |    Error |   StdDev | Ratio | RatioSD |
|--------------------- |----------:|---------:|---------:|------:|--------:|
|                 Bare |  11.01 ns | 0.067 ns | 0.062 ns |  1.00 |    0.00 |
|         PushProperty |  83.89 ns | 1.113 ns | 1.041 ns |  7.62 |    0.09 |
|   PushPropertyNested | 167.61 ns | 2.733 ns | 2.556 ns | 15.22 |    0.24 |
| PushPropertyEnriched | 172.32 ns | 1.754 ns | 1.641 ns | 15.65 |    0.19 |
