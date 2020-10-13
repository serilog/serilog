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
|   RootLogger |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  9.581 ns | 0.0541 ns | 0.0793 ns |  1.00 |    0.00 |
| NestedLogger |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 36.555 ns | 0.8229 ns | 1.2316 ns |  3.82 |    0.14 |
|              |                 |           |               |           |           |           |       |         |
|   RootLogger | net48 LegacyJit | LegacyJit |      .NET 4.8 |  9.743 ns | 0.0449 ns | 0.0672 ns |  1.00 |    0.00 |
| NestedLogger | net48 LegacyJit | LegacyJit |      .NET 4.8 | 42.475 ns | 2.1197 ns | 3.0400 ns |  4.36 |    0.32 |
|              |                 |           |               |           |           |           |       |         |
|   RootLogger |    net48 RyuJit |    RyuJit |      .NET 4.8 |  9.751 ns | 0.0379 ns | 0.0544 ns |  1.00 |    0.00 |
| NestedLogger |    net48 RyuJit |    RyuJit |      .NET 4.8 | 41.577 ns | 0.6717 ns | 1.0054 ns |  4.27 |    0.11 |
