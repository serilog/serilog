``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.404
  [Host]          : .NET Core 3.1.10 (CoreCLR 4.700.20.51601, CoreFX 4.700.20.51901), X64 RyuJIT
  core31 RyuJit   : .NET Core 3.1.10 (CoreCLR 4.700.20.51601, CoreFX 4.700.20.51901), X64 RyuJIT
  net48 LegacyJit : .NET Framework 4.8 (4.8.4300.0), X64 RyuJIT
  net48 RyuJit    : .NET Framework 4.8 (4.8.4300.0), X64 RyuJIT

IterationCount=15  LaunchCount=2  WarmupCount=10  

```
|         Method |             Job |       Jit |       Runtime |      Mean |     Error |    StdDev | Ratio | RatioSD |
|--------------- |---------------- |---------- |-------------- |----------:|----------:|----------:|------:|--------:|
|            Off |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  2.761 ns | 0.0099 ns | 0.0145 ns |  1.00 |    0.00 |
| LevelSwitchOff |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  3.030 ns | 0.0194 ns | 0.0266 ns |  1.10 |    0.01 |
| MinimumLevelOn |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10.829 ns | 0.0293 ns | 0.0420 ns |  3.92 |    0.02 |
|  LevelSwitchOn |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10.083 ns | 0.0645 ns | 0.0904 ns |  3.65 |    0.04 |
|                |                 |           |               |           |           |           |       |         |
|            Off | net48 LegacyJit | LegacyJit |      .NET 4.8 |  2.524 ns | 0.0187 ns | 0.0257 ns |  1.00 |    0.00 |
| LevelSwitchOff | net48 LegacyJit | LegacyJit |      .NET 4.8 |  2.774 ns | 0.0089 ns | 0.0122 ns |  1.10 |    0.01 |
| MinimumLevelOn | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10.881 ns | 0.0336 ns | 0.0472 ns |  4.31 |    0.04 |
|  LevelSwitchOn | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10.486 ns | 0.0304 ns | 0.0437 ns |  4.16 |    0.05 |
|                |                 |           |               |           |           |           |       |         |
|            Off |    net48 RyuJit |    RyuJit |      .NET 4.8 |  2.522 ns | 0.0134 ns | 0.0192 ns |  1.00 |    0.00 |
| LevelSwitchOff |    net48 RyuJit |    RyuJit |      .NET 4.8 |  2.776 ns | 0.0094 ns | 0.0140 ns |  1.10 |    0.01 |
| MinimumLevelOn |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10.894 ns | 0.0218 ns | 0.0326 ns |  4.32 |    0.04 |
|  LevelSwitchOn |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10.472 ns | 0.0345 ns | 0.0517 ns |  4.15 |    0.04 |
