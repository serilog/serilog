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
|              Method |             Job |       Jit |       Runtime |         Mean |     Error |    StdDev |  Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------------- |---------------- |---------- |-------------- |-------------:|----------:|----------:|-------:|--------:|-------:|------:|------:|----------:|
|           NoMessage |   core31 RyuJit |    RyuJit | .NET Core 3.1 |     4.266 ns | 0.0256 ns | 0.0383 ns |   1.00 |    0.00 |      - |     - |     - |         - |
|        NoProperties |   core31 RyuJit |    RyuJit | .NET Core 3.1 |     4.268 ns | 0.0186 ns | 0.0279 ns |   1.00 |    0.01 |      - |     - |     - |         - |
| OneSimpleProperties |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    50.112 ns | 0.2066 ns | 0.2963 ns |  11.74 |    0.14 |      - |     - |     - |         - |
|    VariedProperties |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   285.364 ns | 0.9393 ns | 1.3168 ns |  66.86 |    0.62 | 0.0153 |     - |     - |      96 B |
|   ComplexProperties |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 1,260.600 ns | 3.3006 ns | 4.9402 ns | 295.52 |    2.33 | 0.1259 |     - |     - |     800 B |
|                     |                 |           |               |              |           |           |        |         |        |       |       |           |
|           NoMessage | net48 LegacyJit | LegacyJit |      .NET 4.8 |     3.330 ns | 0.0128 ns | 0.0180 ns |   1.00 |    0.00 |      - |     - |     - |         - |
|        NoProperties | net48 LegacyJit | LegacyJit |      .NET 4.8 |     3.325 ns | 0.0127 ns | 0.0186 ns |   1.00 |    0.01 |      - |     - |     - |         - |
| OneSimpleProperties | net48 LegacyJit | LegacyJit |      .NET 4.8 |    81.950 ns | 0.0933 ns | 0.1307 ns |  24.61 |    0.14 | 0.0050 |     - |     - |      32 B |
|    VariedProperties | net48 LegacyJit | LegacyJit |      .NET 4.8 |   357.667 ns | 1.2484 ns | 1.7088 ns | 107.39 |    0.87 | 0.0153 |     - |     - |      96 B |
|   ComplexProperties | net48 LegacyJit | LegacyJit |      .NET 4.8 | 1,860.266 ns | 6.1500 ns | 9.2051 ns | 558.67 |    3.94 | 0.1698 |     - |     - |    1075 B |
|                     |                 |           |               |              |           |           |        |         |        |       |       |           |
|           NoMessage |    net48 RyuJit |    RyuJit |      .NET 4.8 |     3.334 ns | 0.0133 ns | 0.0194 ns |   1.00 |    0.00 |      - |     - |     - |         - |
|        NoProperties |    net48 RyuJit |    RyuJit |      .NET 4.8 |     3.340 ns | 0.0231 ns | 0.0316 ns |   1.00 |    0.01 |      - |     - |     - |         - |
| OneSimpleProperties |    net48 RyuJit |    RyuJit |      .NET 4.8 |    82.283 ns | 0.2630 ns | 0.3772 ns |  24.67 |    0.17 | 0.0050 |     - |     - |      32 B |
|    VariedProperties |    net48 RyuJit |    RyuJit |      .NET 4.8 |   358.631 ns | 1.0047 ns | 1.4727 ns | 107.57 |    0.80 | 0.0153 |     - |     - |      96 B |
|   ComplexProperties |    net48 RyuJit |    RyuJit |      .NET 4.8 | 1,855.498 ns | 5.6374 ns | 8.4378 ns | 556.50 |    4.18 | 0.1698 |     - |     - |    1075 B |
