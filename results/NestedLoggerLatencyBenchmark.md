``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.508 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.402
  [Host]          : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT
  core31 RyuJit   : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT
  net48 LegacyJit : .NET Framework 4.8 (4.8.4220.0), X64 RyuJIT
  net48 RyuJit    : .NET Framework 4.8 (4.8.4220.0), X64 RyuJIT

IterationCount=15  LaunchCount=2  WarmupCount=10  

```
|       Method |             Job |       Jit |       Runtime |      Mean |     Error |    StdDev | Ratio | RatioSD |
|------------- |---------------- |---------- |-------------- |----------:|----------:|----------:|------:|--------:|
|   RootLogger |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10.353 ns | 0.0348 ns | 0.0510 ns |  1.00 |    0.00 |
| NestedLogger |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 37.343 ns | 0.6662 ns | 0.9554 ns |  3.61 |    0.09 |
|              |                 |           |               |           |           |           |       |         |
|   RootLogger | net48 LegacyJit | LegacyJit |      .NET 4.8 |  9.856 ns | 0.0341 ns | 0.0501 ns |  1.00 |    0.00 |
| NestedLogger | net48 LegacyJit | LegacyJit |      .NET 4.8 | 41.264 ns | 0.5649 ns | 0.8456 ns |  4.18 |    0.09 |
|              |                 |           |               |           |           |           |       |         |
|   RootLogger |    net48 RyuJit |    RyuJit |      .NET 4.8 |  9.844 ns | 0.0330 ns | 0.0494 ns |  1.00 |    0.00 |
| NestedLogger |    net48 RyuJit |    RyuJit |      .NET 4.8 | 41.560 ns | 0.7851 ns | 1.1752 ns |  4.22 |    0.12 |
