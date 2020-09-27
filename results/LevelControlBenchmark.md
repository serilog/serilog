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
|         Method |             Job |       Jit |       Runtime |      Mean |     Error |    StdDev | Ratio | RatioSD |
|--------------- |---------------- |---------- |-------------- |----------:|----------:|----------:|------:|--------:|
|            Off |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  2.724 ns | 0.0420 ns | 0.0603 ns |  1.00 |    0.00 |
| LevelSwitchOff |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  3.055 ns | 0.0320 ns | 0.0478 ns |  1.12 |    0.03 |
| MinimumLevelOn |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10.782 ns | 0.2184 ns | 0.3269 ns |  3.96 |    0.16 |
|  LevelSwitchOn |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  9.939 ns | 0.0447 ns | 0.0655 ns |  3.65 |    0.09 |
|                |                 |           |               |           |           |           |       |         |
|            Off | net48 LegacyJit | LegacyJit |      .NET 4.8 |  3.226 ns | 0.0343 ns | 0.0513 ns |  1.00 |    0.00 |
| LevelSwitchOff | net48 LegacyJit | LegacyJit |      .NET 4.8 |  3.479 ns | 0.0357 ns | 0.0534 ns |  1.08 |    0.03 |
| MinimumLevelOn | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10.561 ns | 0.0652 ns | 0.0975 ns |  3.28 |    0.06 |
|  LevelSwitchOn | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10.628 ns | 0.2139 ns | 0.3201 ns |  3.30 |    0.12 |
|                |                 |           |               |           |           |           |       |         |
|            Off |    net48 RyuJit |    RyuJit |      .NET 4.8 |  3.222 ns | 0.0333 ns | 0.0499 ns |  1.00 |    0.00 |
| LevelSwitchOff |    net48 RyuJit |    RyuJit |      .NET 4.8 |  3.545 ns | 0.0799 ns | 0.1146 ns |  1.10 |    0.04 |
| MinimumLevelOn |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10.581 ns | 0.0651 ns | 0.0974 ns |  3.28 |    0.06 |
|  LevelSwitchOn |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10.485 ns | 0.0657 ns | 0.0963 ns |  3.25 |    0.06 |
