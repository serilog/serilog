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
|   RootLogger | core31 | .NET Core 3.1 |  9.650 ns | 0.0876 ns | 0.1311 ns |  1.00 |    0.00 |
| NestedLogger | core31 | .NET Core 3.1 | 39.127 ns | 0.8806 ns | 1.3181 ns |  4.06 |    0.16 |
|              |        |               |           |           |           |       |         |
|   RootLogger |  net48 |      .NET 4.8 |  9.793 ns | 0.0891 ns | 0.1334 ns |  1.00 |    0.00 |
| NestedLogger |  net48 |      .NET 4.8 | 45.035 ns | 1.1804 ns | 1.7668 ns |  4.60 |    0.19 |
|              |        |               |           |           |           |       |         |
|   RootLogger |  net50 | .NET Core 5.0 |  9.876 ns | 0.2223 ns | 0.3328 ns |  1.00 |    0.00 |
| NestedLogger |  net50 | .NET Core 5.0 | 34.939 ns | 0.9457 ns | 1.3862 ns |  3.54 |    0.16 |
