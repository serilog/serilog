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
|   RootLogger | core31 | .NET Core 3.1 | 10.652 ns | 0.2727 ns | 0.3997 ns |  1.00 |    0.00 |
| NestedLogger | core31 | .NET Core 3.1 | 37.830 ns | 0.9548 ns | 1.3995 ns |  3.55 |    0.15 |
|              |        |               |           |           |           |       |         |
|   RootLogger |  net48 |      .NET 4.8 |  9.922 ns | 0.1154 ns | 0.1727 ns |  1.00 |    0.00 |
| NestedLogger |  net48 |      .NET 4.8 | 44.763 ns | 1.0428 ns | 1.5609 ns |  4.51 |    0.15 |
|              |        |               |           |           |           |       |         |
|   RootLogger |  net50 | .NET Core 5.0 |  9.949 ns | 0.0921 ns | 0.1379 ns |  1.00 |    0.00 |
| NestedLogger |  net50 | .NET Core 5.0 | 33.296 ns | 0.9725 ns | 1.4556 ns |  3.35 |    0.16 |
