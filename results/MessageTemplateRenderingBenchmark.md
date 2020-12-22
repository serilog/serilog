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
|              Method |             Job |       Jit |       Runtime |         Mean |      Error |     StdDev |  Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------------- |---------------- |---------- |-------------- |-------------:|-----------:|-----------:|-------:|--------:|-------:|------:|------:|----------:|
|           NoMessage |   core31 RyuJit |    RyuJit | .NET Core 3.1 |     4.796 ns |  0.0433 ns |  0.0648 ns |   1.00 |    0.00 |      - |     - |     - |         - |
|        NoProperties |   core31 RyuJit |    RyuJit | .NET Core 3.1 |     4.660 ns |  0.1197 ns |  0.1755 ns |   0.97 |    0.04 |      - |     - |     - |         - |
| OneSimpleProperties |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    52.866 ns |  0.4299 ns |  0.6301 ns |  11.02 |    0.18 |      - |     - |     - |         - |
|    VariedProperties |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   303.952 ns |  2.4927 ns |  3.7310 ns |  63.38 |    1.10 | 0.0153 |     - |     - |      96 B |
|   ComplexProperties |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 1,312.200 ns | 14.7088 ns | 22.0154 ns | 273.62 |    5.93 | 0.1259 |     - |     - |     800 B |
|                     |                 |           |               |              |            |            |        |         |        |       |       |           |
|           NoMessage | net48 LegacyJit | LegacyJit |      .NET 4.8 |     3.507 ns |  0.0390 ns |  0.0584 ns |   1.00 |    0.00 |      - |     - |     - |         - |
|        NoProperties | net48 LegacyJit | LegacyJit |      .NET 4.8 |     3.510 ns |  0.0400 ns |  0.0599 ns |   1.00 |    0.03 |      - |     - |     - |         - |
| OneSimpleProperties | net48 LegacyJit | LegacyJit |      .NET 4.8 |    89.103 ns |  0.6132 ns |  0.8795 ns |  25.42 |    0.47 | 0.0050 |     - |     - |      32 B |
|    VariedProperties | net48 LegacyJit | LegacyJit |      .NET 4.8 |   380.163 ns |  2.8798 ns |  4.3104 ns | 108.42 |    1.97 | 0.0153 |     - |     - |      96 B |
|   ComplexProperties | net48 LegacyJit | LegacyJit |      .NET 4.8 | 1,973.103 ns | 19.3825 ns | 29.0108 ns | 562.73 |   13.01 | 0.1678 |     - |     - |    1075 B |
|                     |                 |           |               |              |            |            |        |         |        |       |       |           |
|           NoMessage |    net48 RyuJit |    RyuJit |      .NET 4.8 |     3.496 ns |  0.0368 ns |  0.0540 ns |   1.00 |    0.00 |      - |     - |     - |         - |
|        NoProperties |    net48 RyuJit |    RyuJit |      .NET 4.8 |     4.086 ns |  0.3957 ns |  0.5923 ns |   1.16 |    0.17 |      - |     - |     - |         - |
| OneSimpleProperties |    net48 RyuJit |    RyuJit |      .NET 4.8 |    88.927 ns |  0.5913 ns |  0.8850 ns |  25.44 |    0.47 | 0.0050 |     - |     - |      32 B |
|    VariedProperties |    net48 RyuJit |    RyuJit |      .NET 4.8 |   378.103 ns |  2.2611 ns |  3.3843 ns | 108.20 |    2.40 | 0.0153 |     - |     - |      96 B |
|   ComplexProperties |    net48 RyuJit |    RyuJit |      .NET 4.8 | 1,959.300 ns | 18.4659 ns | 27.6389 ns | 560.57 |   13.02 | 0.1678 |     - |     - |    1075 B |
