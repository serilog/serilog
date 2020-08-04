``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT


```
|       Method |      Mean |     Error |    StdDev | Ratio | RatioSD |
|------------- |----------:|----------:|----------:|------:|--------:|
|   RootLogger |  9.109 ns | 0.1917 ns | 0.1793 ns |  1.00 |    0.00 |
| NestedLogger | 45.655 ns | 0.9305 ns | 1.1427 ns |  5.02 |    0.17 |
