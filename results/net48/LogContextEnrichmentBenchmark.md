``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-7700HQ CPU 2.80GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
  [Host]     : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.8.4010.0
  DefaultJob : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.8.4010.0


```
|               Method |      Mean |     Error |    StdDev | Ratio | RatioSD |
|--------------------- |----------:|----------:|----------:|------:|--------:|
|                 Bare |  10.58 ns | 0.2364 ns | 0.2211 ns |  1.00 |    0.00 |
|         PushProperty |  91.55 ns | 0.6345 ns | 0.5625 ns |  8.65 |    0.20 |
|   PushPropertyNested | 165.68 ns | 0.6366 ns | 0.4970 ns | 15.61 |    0.33 |
| PushPropertyEnriched | 183.69 ns | 3.5304 ns | 4.2027 ns | 17.44 |    0.63 |
