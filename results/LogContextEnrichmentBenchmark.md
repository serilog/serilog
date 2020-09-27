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
|               Method |             Job |       Jit |       Runtime |      Mean |    Error |   StdDev | Ratio | RatioSD |
|--------------------- |---------------- |---------- |-------------- |----------:|---------:|---------:|------:|--------:|
|                 Bare |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  10.54 ns | 0.069 ns | 0.102 ns |  1.00 |    0.00 |
|         PushProperty |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  95.59 ns | 0.778 ns | 1.165 ns |  9.07 |    0.14 |
|   PushPropertyNested |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 195.49 ns | 1.088 ns | 1.561 ns | 18.55 |    0.23 |
| PushPropertyEnriched |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 174.23 ns | 1.940 ns | 2.904 ns | 16.52 |    0.29 |
|                      |                 |           |               |           |          |          |       |         |
|                 Bare | net48 LegacyJit | LegacyJit |      .NET 4.8 |  10.86 ns | 0.064 ns | 0.095 ns |  1.00 |    0.00 |
|         PushProperty | net48 LegacyJit | LegacyJit |      .NET 4.8 |  80.15 ns | 2.011 ns | 3.010 ns |  7.38 |    0.29 |
|   PushPropertyNested | net48 LegacyJit | LegacyJit |      .NET 4.8 | 157.60 ns | 1.340 ns | 2.006 ns | 14.51 |    0.19 |
| PushPropertyEnriched | net48 LegacyJit | LegacyJit |      .NET 4.8 | 169.72 ns | 3.421 ns | 5.120 ns | 15.63 |    0.49 |
|                      |                 |           |               |           |          |          |       |         |
|                 Bare |    net48 RyuJit |    RyuJit |      .NET 4.8 |  10.95 ns | 0.135 ns | 0.202 ns |  1.00 |    0.00 |
|         PushProperty |    net48 RyuJit |    RyuJit |      .NET 4.8 |  78.35 ns | 0.590 ns | 0.883 ns |  7.16 |    0.16 |
|   PushPropertyNested |    net48 RyuJit |    RyuJit |      .NET 4.8 | 158.96 ns | 3.153 ns | 4.720 ns | 14.53 |    0.56 |
| PushPropertyEnriched |    net48 RyuJit |    RyuJit |      .NET 4.8 | 167.69 ns | 1.194 ns | 1.787 ns | 15.32 |    0.31 |
