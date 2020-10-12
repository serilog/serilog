``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.508 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.402
  [Host]          : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT
  core31 RyuJit   : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT
  net48 LegacyJit : .NET Framework 4.8 (4.8.4250.0), X64 RyuJIT
  net48 RyuJit    : .NET Framework 4.8 (4.8.4250.0), X64 RyuJIT

IterationCount=15  LaunchCount=2  WarmupCount=10  

```
|       Method |             Job |       Jit |       Runtime |      Mean |     Error |    StdDev | Ratio | RatioSD |
|------------- |---------------- |---------- |-------------- |----------:|----------:|----------:|------:|--------:|
|   RootLogger |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10.443 ns | 0.1453 ns | 0.2174 ns |  1.00 |    0.00 |
| NestedLogger |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 37.290 ns | 0.9015 ns | 1.3493 ns |  3.57 |    0.16 |
|              |                 |           |               |           |           |           |       |         |
|   RootLogger | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10.024 ns | 0.1172 ns | 0.1754 ns |  1.00 |    0.00 |
| NestedLogger | net48 LegacyJit | LegacyJit |      .NET 4.8 | 41.484 ns | 0.7039 ns | 1.0318 ns |  4.13 |    0.10 |
|              |                 |           |               |           |           |           |       |         |
|   RootLogger |    net48 RyuJit |    RyuJit |      .NET 4.8 |  9.973 ns | 0.1272 ns | 0.1904 ns |  1.00 |    0.00 |
| NestedLogger |    net48 RyuJit |    RyuJit |      .NET 4.8 | 41.135 ns | 0.9173 ns | 1.2860 ns |  4.13 |    0.15 |
