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
|   RootLogger |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  9.955 ns | 0.1048 ns | 0.1536 ns |  1.00 |    0.00 |
| NestedLogger |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 39.043 ns | 1.2682 ns | 1.8981 ns |  3.91 |    0.20 |
|              |                 |           |               |           |           |           |       |         |
|   RootLogger | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10.517 ns | 0.0927 ns | 0.1387 ns |  1.00 |    0.00 |
| NestedLogger | net48 LegacyJit | LegacyJit |      .NET 4.8 | 48.024 ns | 1.0855 ns | 1.6248 ns |  4.57 |    0.17 |
|              |                 |           |               |           |           |           |       |         |
|   RootLogger |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10.536 ns | 0.1462 ns | 0.2097 ns |  1.00 |    0.00 |
| NestedLogger |    net48 RyuJit |    RyuJit |      .NET 4.8 | 48.265 ns | 1.2194 ns | 1.8252 ns |  4.59 |    0.21 |
