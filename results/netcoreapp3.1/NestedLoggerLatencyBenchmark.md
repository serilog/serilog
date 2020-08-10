``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.388 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.302
  [Host]     : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT
  DefaultJob : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT


```
|       Method |      Mean |     Error |    StdDev | Ratio | RatioSD |
|------------- |----------:|----------:|----------:|------:|--------:|
|   RootLogger |  9.546 ns | 0.2010 ns | 0.1880 ns |  1.00 |    0.00 |
| NestedLogger | 38.373 ns | 0.7469 ns | 1.2478 ns |  4.05 |    0.11 |
