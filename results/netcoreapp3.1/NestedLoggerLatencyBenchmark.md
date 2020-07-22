``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.301
  [Host]     : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT
  DefaultJob : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT


```
|       Method |     Mean |    Error |   StdDev | Ratio | RatioSD |
|------------- |---------:|---------:|---------:|------:|--------:|
|   RootLogger | 10.63 ns | 0.171 ns | 0.160 ns |  1.00 |    0.00 |
| NestedLogger | 39.54 ns | 0.808 ns | 1.537 ns |  3.73 |    0.16 |
