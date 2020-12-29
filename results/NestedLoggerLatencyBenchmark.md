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
|   RootLogger | core31 | .NET Core 3.1 | 10.441 ns | 0.1327 ns | 0.1946 ns |  1.00 |    0.00 |
| NestedLogger | core31 | .NET Core 3.1 | 39.077 ns | 0.8947 ns | 1.3391 ns |  3.75 |    0.16 |
|              |        |               |           |           |           |       |         |
|   RootLogger |  net48 |      .NET 4.8 |  9.913 ns | 0.1087 ns | 0.1627 ns |  1.00 |    0.00 |
| NestedLogger |  net48 |      .NET 4.8 | 41.789 ns | 0.9865 ns | 1.4765 ns |  4.22 |    0.12 |
|              |        |               |           |           |           |       |         |
|   RootLogger |  net50 | .NET Core 5.0 |  9.802 ns | 0.1279 ns | 0.1915 ns |  1.00 |    0.00 |
| NestedLogger |  net50 | .NET Core 5.0 | 32.960 ns | 1.0886 ns | 1.6294 ns |  3.36 |    0.17 |
