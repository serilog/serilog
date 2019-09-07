``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-7700HQ CPU 2.80GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
  [Host]     : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.8.3815.0
  DefaultJob : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.8.3815.0


```
|               Method |      Mean |     Error |    StdDev | Ratio | RatioSD |
|--------------------- |----------:|----------:|----------:|------:|--------:|
|                 Bare |  10.93 ns | 0.2503 ns | 0.2458 ns |  1.00 |    0.00 |
|         PushProperty |  90.29 ns | 1.8298 ns | 2.1072 ns |  8.27 |    0.30 |
|   PushPropertyNested | 177.01 ns | 3.8505 ns | 5.6440 ns | 16.15 |    0.60 |
| PushPropertyEnriched | 188.91 ns | 3.3902 ns | 5.7568 ns | 17.14 |    0.55 |
