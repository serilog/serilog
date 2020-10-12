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
|              Method |             Job |       Jit |       Runtime |         Mean |      Error |     StdDev |  Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------------- |---------------- |---------- |-------------- |-------------:|-----------:|-----------:|-------:|--------:|-------:|------:|------:|----------:|
|           NoMessage |   core31 RyuJit |    RyuJit | .NET Core 3.1 |     3.639 ns |  0.4640 ns |  0.6945 ns |   1.00 |    0.00 |      - |     - |     - |         - |
|        NoProperties |   core31 RyuJit |    RyuJit | .NET Core 3.1 |     4.481 ns |  0.1277 ns |  0.1831 ns |   1.25 |    0.21 |      - |     - |     - |         - |
| OneSimpleProperties |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    51.613 ns |  0.5417 ns |  0.8108 ns |  14.68 |    2.72 |      - |     - |     - |         - |
|    VariedProperties |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   291.733 ns |  5.3260 ns |  7.9716 ns |  83.16 |   16.45 | 0.0153 |     - |     - |      96 B |
|   ComplexProperties |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 1,259.429 ns | 18.6270 ns | 27.3033 ns | 357.30 |   72.39 | 0.1259 |     - |     - |     800 B |
|                     |                 |           |               |              |            |            |        |         |        |       |       |           |
|           NoMessage | net48 LegacyJit | LegacyJit |      .NET 4.8 |     3.402 ns |  0.0704 ns |  0.1054 ns |   1.00 |    0.00 |      - |     - |     - |         - |
|        NoProperties | net48 LegacyJit | LegacyJit |      .NET 4.8 |     3.383 ns |  0.0488 ns |  0.0730 ns |   0.99 |    0.02 |      - |     - |     - |         - |
| OneSimpleProperties | net48 LegacyJit | LegacyJit |      .NET 4.8 |    84.629 ns |  1.1712 ns |  1.7531 ns |  24.90 |    0.93 | 0.0050 |     - |     - |      32 B |
|    VariedProperties | net48 LegacyJit | LegacyJit |      .NET 4.8 |   362.810 ns |  4.4624 ns |  6.6790 ns | 106.75 |    3.77 | 0.0153 |     - |     - |      96 B |
|   ComplexProperties | net48 LegacyJit | LegacyJit |      .NET 4.8 | 1,869.230 ns | 24.5526 ns | 36.7491 ns | 550.05 |   21.75 | 0.1698 |     - |     - |    1075 B |
|                     |                 |           |               |              |            |            |        |         |        |       |       |           |
|           NoMessage |    net48 RyuJit |    RyuJit |      .NET 4.8 |     3.374 ns |  0.0551 ns |  0.0825 ns |   1.00 |    0.00 |      - |     - |     - |         - |
|        NoProperties |    net48 RyuJit |    RyuJit |      .NET 4.8 |     3.478 ns |  0.1054 ns |  0.1578 ns |   1.03 |    0.05 |      - |     - |     - |         - |
| OneSimpleProperties |    net48 RyuJit |    RyuJit |      .NET 4.8 |    85.664 ns |  2.2131 ns |  3.3124 ns |  25.40 |    1.12 | 0.0050 |     - |     - |      32 B |
|    VariedProperties |    net48 RyuJit |    RyuJit |      .NET 4.8 |   363.265 ns |  4.9153 ns |  7.3570 ns | 107.73 |    3.74 | 0.0153 |     - |     - |      96 B |
|   ComplexProperties |    net48 RyuJit |    RyuJit |      .NET 4.8 | 1,860.783 ns | 23.8260 ns | 35.6616 ns | 551.77 |   16.29 | 0.1698 |     - |     - |    1075 B |
