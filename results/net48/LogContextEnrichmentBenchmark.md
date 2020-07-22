``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT


```
|               Method |      Mean |    Error |   StdDev | Ratio | RatioSD |
|--------------------- |----------:|---------:|---------:|------:|--------:|
|                 Bare |  10.14 ns | 0.227 ns | 0.252 ns |  1.00 |    0.00 |
|         PushProperty |  83.71 ns | 1.712 ns | 1.758 ns |  8.24 |    0.29 |
|   PushPropertyNested | 163.51 ns | 0.882 ns | 0.825 ns | 16.06 |    0.41 |
| PushPropertyEnriched | 174.58 ns | 3.697 ns | 3.458 ns | 17.15 |    0.63 |
