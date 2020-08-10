``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.388 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT


```
|       Method |      Mean |     Error |    StdDev | Ratio | RatioSD |
|------------- |----------:|----------:|----------:|------:|--------:|
|   RootLogger |  9.345 ns | 0.2101 ns | 0.1966 ns |  1.00 |    0.00 |
| NestedLogger | 47.408 ns | 0.9698 ns | 1.1545 ns |  5.07 |    0.14 |
