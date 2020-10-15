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
|              Method |             Job |       Jit |       Runtime |         Mean |     Error |     StdDev |  Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------------- |---------------- |---------- |-------------- |-------------:|----------:|-----------:|-------:|--------:|-------:|------:|------:|----------:|
|           NoMessage |   core31 RyuJit |    RyuJit | .NET Core 3.1 |     3.510 ns | 0.0348 ns |  0.0499 ns |   1.00 |    0.00 |      - |     - |     - |         - |
|        NoProperties |   core31 RyuJit |    RyuJit | .NET Core 3.1 |     3.485 ns | 0.0194 ns |  0.0284 ns |   0.99 |    0.02 |      - |     - |     - |         - |
| OneSimpleProperties |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    48.366 ns | 0.1286 ns |  0.1884 ns |  13.78 |    0.19 |      - |     - |     - |         - |
|    VariedProperties |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   279.487 ns | 2.0224 ns |  3.0271 ns |  79.70 |    1.06 | 0.0153 |     - |     - |      96 B |
|   ComplexProperties |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 1,230.673 ns | 6.7086 ns | 10.0412 ns | 350.77 |    4.92 | 0.1259 |     - |     - |     800 B |
|                     |                 |           |               |              |           |            |        |         |        |       |       |           |
|           NoMessage | net48 LegacyJit | LegacyJit |      .NET 4.8 |     4.240 ns | 0.0692 ns |  0.1036 ns |   1.00 |    0.00 |      - |     - |     - |         - |
|        NoProperties | net48 LegacyJit | LegacyJit |      .NET 4.8 |     4.252 ns | 0.0734 ns |  0.1052 ns |   1.00 |    0.02 |      - |     - |     - |         - |
| OneSimpleProperties | net48 LegacyJit | LegacyJit |      .NET 4.8 |    81.646 ns | 0.2760 ns |  0.4046 ns |  19.27 |    0.48 | 0.0050 |     - |     - |      32 B |
|    VariedProperties | net48 LegacyJit | LegacyJit |      .NET 4.8 |   352.697 ns | 1.2775 ns |  1.8322 ns |  83.14 |    1.98 | 0.0153 |     - |     - |      96 B |
|   ComplexProperties | net48 LegacyJit | LegacyJit |      .NET 4.8 | 1,806.924 ns | 3.0421 ns |  4.4591 ns | 426.43 |   10.39 | 0.1698 |     - |     - |    1075 B |
|                     |                 |           |               |              |           |            |        |         |        |       |       |           |
|           NoMessage |    net48 RyuJit |    RyuJit |      .NET 4.8 |     4.145 ns | 0.0234 ns |  0.0335 ns |   1.00 |    0.00 |      - |     - |     - |         - |
|        NoProperties |    net48 RyuJit |    RyuJit |      .NET 4.8 |     4.167 ns | 0.0342 ns |  0.0501 ns |   1.01 |    0.02 |      - |     - |     - |         - |
| OneSimpleProperties |    net48 RyuJit |    RyuJit |      .NET 4.8 |    81.735 ns | 0.2745 ns |  0.4023 ns |  19.72 |    0.19 | 0.0050 |     - |     - |      32 B |
|    VariedProperties |    net48 RyuJit |    RyuJit |      .NET 4.8 |   352.758 ns | 1.2960 ns |  1.9398 ns |  85.10 |    0.90 | 0.0153 |     - |     - |      96 B |
|   ComplexProperties |    net48 RyuJit |    RyuJit |      .NET 4.8 | 1,798.913 ns | 5.4430 ns |  7.6303 ns | 433.91 |    3.41 | 0.1698 |     - |     - |    1075 B |
