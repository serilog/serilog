``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT


```
|               Method |      Mean |    Error |   StdDev | Ratio | RatioSD |
|--------------------- |----------:|---------:|---------:|------:|--------:|
|                 Bare |  10.17 ns | 0.165 ns | 0.146 ns |  1.00 |    0.00 |
|         PushProperty |  89.74 ns | 1.812 ns | 3.028 ns |  8.86 |    0.38 |
|   PushPropertyNested | 172.72 ns | 2.224 ns | 1.972 ns | 16.98 |    0.37 |
| PushPropertyEnriched | 183.02 ns | 2.591 ns | 2.297 ns | 17.99 |    0.34 |
