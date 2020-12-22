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
|               Method |             Job |       Jit |       Runtime |       Mean |     Error |    StdDev | Ratio | RatioSD |
|--------------------- |---------------- |---------- |-------------- |-----------:|----------:|----------:|------:|--------:|
|                 Bare |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   9.995 ns | 0.0755 ns | 0.1130 ns |  1.00 |    0.00 |
|         PushProperty |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  99.174 ns | 1.0067 ns | 1.5068 ns |  9.92 |    0.18 |
|   PushPropertyNested |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 187.172 ns | 1.5496 ns | 2.3193 ns | 18.73 |    0.28 |
| PushPropertyEnriched |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 171.284 ns | 2.7945 ns | 4.1826 ns | 17.14 |    0.40 |
|                      |                 |           |               |            |           |           |       |         |
|                 Bare | net48 LegacyJit | LegacyJit |      .NET 4.8 |  11.060 ns | 0.0849 ns | 0.1217 ns |  1.00 |    0.00 |
|         PushProperty | net48 LegacyJit | LegacyJit |      .NET 4.8 |  81.157 ns | 0.8968 ns | 1.3422 ns |  7.34 |    0.16 |
|   PushPropertyNested | net48 LegacyJit | LegacyJit |      .NET 4.8 | 158.023 ns | 1.4823 ns | 2.2186 ns | 14.29 |    0.22 |
| PushPropertyEnriched | net48 LegacyJit | LegacyJit |      .NET 4.8 | 160.211 ns | 1.4070 ns | 2.1059 ns | 14.49 |    0.23 |
|                      |                 |           |               |            |           |           |       |         |
|                 Bare |    net48 RyuJit |    RyuJit |      .NET 4.8 |  10.995 ns | 0.1229 ns | 0.1840 ns |  1.00 |    0.00 |
|         PushProperty |    net48 RyuJit |    RyuJit |      .NET 4.8 |  81.139 ns | 0.5629 ns | 0.8425 ns |  7.38 |    0.14 |
|   PushPropertyNested |    net48 RyuJit |    RyuJit |      .NET 4.8 | 157.442 ns | 1.5567 ns | 2.3300 ns | 14.32 |    0.31 |
| PushPropertyEnriched |    net48 RyuJit |    RyuJit |      .NET 4.8 | 159.699 ns | 1.4546 ns | 2.1772 ns | 14.53 |    0.31 |
