``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.301
  [Host]     : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT
  DefaultJob : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT


```
|       Method |     Mean |    Error |   StdDev | Ratio | RatioSD |
|------------- |---------:|---------:|---------:|------:|--------:|
|   RootLogger | 10.01 ns | 0.067 ns | 0.059 ns |  1.00 |    0.00 |
| NestedLogger | 38.07 ns | 0.708 ns | 0.662 ns |  3.81 |    0.08 |
