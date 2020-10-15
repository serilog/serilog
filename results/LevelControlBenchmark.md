``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.572 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.402
  [Host]          : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT
  core31 RyuJit   : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT
  net48 LegacyJit : .NET Framework 4.8 (4.8.4250.0), X64 RyuJIT
  net48 RyuJit    : .NET Framework 4.8 (4.8.4250.0), X64 RyuJIT

IterationCount=15  LaunchCount=2  WarmupCount=10  

```
|         Method |             Job |       Jit |       Runtime |     Mean |     Error |    StdDev | Ratio | RatioSD |
|--------------- |---------------- |---------- |-------------- |---------:|----------:|----------:|------:|--------:|
|            Off |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 1.653 ns | 0.0114 ns | 0.0163 ns |  1.00 |    0.00 |
| LevelSwitchOff |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 1.698 ns | 0.0129 ns | 0.0189 ns |  1.03 |    0.01 |
| MinimumLevelOn |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 7.641 ns | 0.0411 ns | 0.0576 ns |  4.62 |    0.06 |
|  LevelSwitchOn |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 8.171 ns | 0.0444 ns | 0.0664 ns |  4.94 |    0.06 |
|                |                 |           |               |          |           |           |       |         |
|            Off | net48 LegacyJit | LegacyJit |      .NET 4.8 | 1.446 ns | 0.0100 ns | 0.0150 ns |  1.00 |    0.00 |
| LevelSwitchOff | net48 LegacyJit | LegacyJit |      .NET 4.8 | 1.458 ns | 0.0222 ns | 0.0326 ns |  1.01 |    0.02 |
| MinimumLevelOn | net48 LegacyJit | LegacyJit |      .NET 4.8 | 7.690 ns | 0.0329 ns | 0.0462 ns |  5.32 |    0.07 |
|  LevelSwitchOn | net48 LegacyJit | LegacyJit |      .NET 4.8 | 8.124 ns | 0.0466 ns | 0.0683 ns |  5.62 |    0.07 |
|                |                 |           |               |          |           |           |       |         |
|            Off |    net48 RyuJit |    RyuJit |      .NET 4.8 | 1.464 ns | 0.0140 ns | 0.0201 ns |  1.00 |    0.00 |
| LevelSwitchOff |    net48 RyuJit |    RyuJit |      .NET 4.8 | 1.447 ns | 0.0140 ns | 0.0205 ns |  0.99 |    0.02 |
| MinimumLevelOn |    net48 RyuJit |    RyuJit |      .NET 4.8 | 7.697 ns | 0.0511 ns | 0.0749 ns |  5.26 |    0.09 |
|  LevelSwitchOn |    net48 RyuJit |    RyuJit |      .NET 4.8 | 8.143 ns | 0.0373 ns | 0.0558 ns |  5.57 |    0.07 |
