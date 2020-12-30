``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=5.0.101
  [Host] : .NET Core 5.0.1 (CoreCLR 5.0.120.57516, CoreFX 5.0.120.57516), X64 RyuJIT
  core31 : .NET Core 3.1.10 (CoreCLR 4.700.20.51601, CoreFX 4.700.20.51901), X64 RyuJIT
  net48  : .NET Framework 4.8 (4.8.4300.0), X64 RyuJIT
  net50  : .NET Core 5.0.1 (CoreCLR 5.0.120.57516, CoreFX 5.0.120.57516), X64 RyuJIT

Jit=RyuJit  IterationCount=15  LaunchCount=2  
WarmupCount=10  

```
|       Method |    Job |       Runtime |      Mean |     Error |    StdDev | Ratio | RatioSD |
|------------- |------- |-------------- |----------:|----------:|----------:|------:|--------:|
|   RootLogger | core31 | .NET Core 3.1 |  9.873 ns | 0.1439 ns | 0.2154 ns |  1.00 |    0.00 |
| NestedLogger | core31 | .NET Core 3.1 | 39.264 ns | 1.5288 ns | 2.2882 ns |  3.98 |    0.24 |
|              |        |               |           |           |           |       |         |
|   RootLogger |  net48 |      .NET 4.8 | 10.304 ns | 0.1259 ns | 0.1884 ns |  1.00 |    0.00 |
| NestedLogger |  net48 |      .NET 4.8 | 44.581 ns | 1.1322 ns | 1.6946 ns |  4.33 |    0.17 |
|              |        |               |           |           |           |       |         |
|   RootLogger |  net50 | .NET Core 5.0 |  9.177 ns | 0.1365 ns | 0.2043 ns |  1.00 |    0.00 |
| NestedLogger |  net50 | .NET Core 5.0 | 33.826 ns | 1.0906 ns | 1.5987 ns |  3.69 |    0.18 |
