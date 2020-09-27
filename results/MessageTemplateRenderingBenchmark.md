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
|              Method |             Job |       Jit |       Runtime |         Mean |      Error |     StdDev |  Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------------- |---------------- |---------- |-------------- |-------------:|-----------:|-----------:|-------:|--------:|-------:|------:|------:|----------:|
|           NoMessage |   core31 RyuJit |    RyuJit | .NET Core 3.1 |     4.848 ns |  0.0338 ns |  0.0506 ns |   1.00 |    0.00 |      - |     - |     - |         - |
|        NoProperties |   core31 RyuJit |    RyuJit | .NET Core 3.1 |     4.548 ns |  0.3353 ns |  0.4914 ns |   0.94 |    0.10 |      - |     - |     - |         - |
| OneSimpleProperties |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    52.775 ns |  0.7545 ns |  1.1293 ns |  10.89 |    0.28 |      - |     - |     - |         - |
|    VariedProperties |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   319.393 ns |  1.9599 ns |  2.9335 ns |  65.89 |    0.87 | 0.0153 |     - |     - |      96 B |
|   ComplexProperties |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 1,332.658 ns |  9.4116 ns | 14.0869 ns | 274.92 |    4.22 | 0.1259 |     - |     - |     800 B |
|                     |                 |           |               |              |            |            |        |         |        |       |       |           |
|           NoMessage | net48 LegacyJit | LegacyJit |      .NET 4.8 |     5.176 ns |  0.0337 ns |  0.0505 ns |   1.00 |    0.00 |      - |     - |     - |         - |
|        NoProperties | net48 LegacyJit | LegacyJit |      .NET 4.8 |     5.197 ns |  0.0513 ns |  0.0735 ns |   1.00 |    0.02 |      - |     - |     - |         - |
| OneSimpleProperties | net48 LegacyJit | LegacyJit |      .NET 4.8 |    88.855 ns |  0.5751 ns |  0.8430 ns |  17.16 |    0.19 | 0.0050 |     - |     - |      32 B |
|    VariedProperties | net48 LegacyJit | LegacyJit |      .NET 4.8 |   378.390 ns |  2.2093 ns |  3.3068 ns |  73.10 |    0.83 | 0.0153 |     - |     - |      96 B |
|   ComplexProperties | net48 LegacyJit | LegacyJit |      .NET 4.8 | 1,961.933 ns | 15.9034 ns | 23.8035 ns | 379.03 |    4.65 | 0.1678 |     - |     - |    1075 B |
|                     |                 |           |               |              |            |            |        |         |        |       |       |           |
|           NoMessage |    net48 RyuJit |    RyuJit |      .NET 4.8 |     5.168 ns |  0.0291 ns |  0.0427 ns |   1.00 |    0.00 |      - |     - |     - |         - |
|        NoProperties |    net48 RyuJit |    RyuJit |      .NET 4.8 |     5.175 ns |  0.0276 ns |  0.0386 ns |   1.00 |    0.01 |      - |     - |     - |         - |
| OneSimpleProperties |    net48 RyuJit |    RyuJit |      .NET 4.8 |    88.821 ns |  0.6222 ns |  0.9313 ns |  17.19 |    0.22 | 0.0050 |     - |     - |      32 B |
|    VariedProperties |    net48 RyuJit |    RyuJit |      .NET 4.8 |   382.027 ns |  3.7902 ns |  5.5556 ns |  73.92 |    1.17 | 0.0153 |     - |     - |      96 B |
|   ComplexProperties |    net48 RyuJit |    RyuJit |      .NET 4.8 | 1,993.609 ns | 51.0265 ns | 74.7940 ns | 385.74 |   14.59 | 0.1678 |     - |     - |    1075 B |
