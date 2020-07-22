``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT


```
|       Method |      Mean |     Error |    StdDev | Ratio | RatioSD |
|------------- |----------:|----------:|----------:|------:|--------:|
|   RootLogger |  9.386 ns | 0.0503 ns | 0.0446 ns |  1.00 |    0.00 |
| NestedLogger | 47.411 ns | 0.9705 ns | 1.5671 ns |  5.17 |    0.20 |
