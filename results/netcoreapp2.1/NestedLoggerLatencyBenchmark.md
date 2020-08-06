``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.388 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.302
  [Host]     : .NET Core 2.1.20 (CoreCLR 4.6.29017.01, CoreFX 4.6.29018.12), X64 RyuJIT
  DefaultJob : .NET Core 2.1.20 (CoreCLR 4.6.29017.01, CoreFX 4.6.29018.12), X64 RyuJIT


```
|       Method |     Mean |    Error |   StdDev | Ratio | RatioSD |
|------------- |---------:|---------:|---------:|------:|--------:|
|   RootLogger | 10.98 ns | 0.166 ns | 0.155 ns |  1.00 |    0.00 |
| NestedLogger | 42.64 ns | 0.876 ns | 1.904 ns |  3.87 |    0.17 |
