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
|               Method |             Job |       Jit |       Runtime |      Mean |    Error |   StdDev | Ratio | RatioSD |
|--------------------- |---------------- |---------- |-------------- |----------:|---------:|---------:|------:|--------:|
|                 Bare |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  10.07 ns | 0.100 ns | 0.150 ns |  1.00 |    0.00 |
|         PushProperty |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  94.00 ns | 1.068 ns | 1.565 ns |  9.33 |    0.20 |
|   PushPropertyNested |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 189.83 ns | 1.798 ns | 2.692 ns | 18.85 |    0.40 |
| PushPropertyEnriched |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 174.14 ns | 2.200 ns | 3.292 ns | 17.29 |    0.27 |
|                      |                 |           |               |           |          |          |       |         |
|                 Bare | net48 LegacyJit | LegacyJit |      .NET 4.8 |  10.80 ns | 0.281 ns | 0.420 ns |  1.00 |    0.00 |
|         PushProperty | net48 LegacyJit | LegacyJit |      .NET 4.8 |  80.36 ns | 0.953 ns | 1.426 ns |  7.45 |    0.32 |
|   PushPropertyNested | net48 LegacyJit | LegacyJit |      .NET 4.8 | 156.47 ns | 1.654 ns | 2.475 ns | 14.51 |    0.60 |
| PushPropertyEnriched | net48 LegacyJit | LegacyJit |      .NET 4.8 | 165.07 ns | 1.951 ns | 2.920 ns | 15.31 |    0.62 |
|                      |                 |           |               |           |          |          |       |         |
|                 Bare |    net48 RyuJit |    RyuJit |      .NET 4.8 |  10.67 ns | 0.112 ns | 0.168 ns |  1.00 |    0.00 |
|         PushProperty |    net48 RyuJit |    RyuJit |      .NET 4.8 |  80.11 ns | 0.937 ns | 1.403 ns |  7.51 |    0.19 |
|   PushPropertyNested |    net48 RyuJit |    RyuJit |      .NET 4.8 | 156.57 ns | 2.090 ns | 3.128 ns | 14.68 |    0.42 |
| PushPropertyEnriched |    net48 RyuJit |    RyuJit |      .NET 4.8 | 164.77 ns | 1.838 ns | 2.752 ns | 15.45 |    0.34 |
