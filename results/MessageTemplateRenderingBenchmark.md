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
|              Method |             Job |       Jit |       Runtime |         Mean |      Error |     StdDev |       Median |  Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------------- |---------------- |---------- |-------------- |-------------:|-----------:|-----------:|-------------:|-------:|--------:|-------:|------:|------:|----------:|
|           NoMessage |   core31 RyuJit |    RyuJit | .NET Core 3.1 |     4.421 ns |  0.1307 ns |  0.1956 ns |     4.424 ns |   1.00 |    0.00 |      - |     - |     - |         - |
|        NoProperties |   core31 RyuJit |    RyuJit | .NET Core 3.1 |     4.429 ns |  0.1347 ns |  0.2016 ns |     4.435 ns |   1.01 |    0.09 |      - |     - |     - |         - |
| OneSimpleProperties |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    50.531 ns |  0.4213 ns |  0.6306 ns |    50.598 ns |  11.45 |    0.52 |      - |     - |     - |         - |
|    VariedProperties |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   302.906 ns |  4.7680 ns |  7.1366 ns |   300.849 ns |  68.61 |    2.74 | 0.0153 |     - |     - |      96 B |
|   ComplexProperties |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 1,312.175 ns | 15.6709 ns | 22.9702 ns | 1,316.004 ns | 296.87 |   12.04 | 0.1259 |     - |     - |     800 B |
|                     |                 |           |               |              |            |            |              |        |         |        |       |       |           |
|           NoMessage | net48 LegacyJit | LegacyJit |      .NET 4.8 |     4.806 ns |  0.7242 ns |  1.0386 ns |     5.623 ns |   1.00 |    0.00 |      - |     - |     - |         - |
|        NoProperties | net48 LegacyJit | LegacyJit |      .NET 4.8 |     4.176 ns |  0.3365 ns |  0.5036 ns |     4.176 ns |   0.88 |    0.11 |      - |     - |     - |         - |
| OneSimpleProperties | net48 LegacyJit | LegacyJit |      .NET 4.8 |    89.435 ns |  0.9378 ns |  1.4037 ns |    89.524 ns |  19.48 |    4.28 | 0.0050 |     - |     - |      32 B |
|    VariedProperties | net48 LegacyJit | LegacyJit |      .NET 4.8 |   380.763 ns |  3.6273 ns |  5.4292 ns |   381.522 ns |  83.05 |   18.59 | 0.0153 |     - |     - |      96 B |
|   ComplexProperties | net48 LegacyJit | LegacyJit |      .NET 4.8 | 1,927.514 ns | 20.6071 ns | 30.2056 ns | 1,929.283 ns | 420.57 |   94.16 | 0.1698 |     - |     - |    1075 B |
|                     |                 |           |               |              |            |            |              |        |         |        |       |       |           |
|           NoMessage |    net48 RyuJit |    RyuJit |      .NET 4.8 |     3.698 ns |  0.0490 ns |  0.0734 ns |     3.688 ns |   1.00 |    0.00 |      - |     - |     - |         - |
|        NoProperties |    net48 RyuJit |    RyuJit |      .NET 4.8 |     3.688 ns |  0.0501 ns |  0.0750 ns |     3.676 ns |   1.00 |    0.03 |      - |     - |     - |         - |
| OneSimpleProperties |    net48 RyuJit |    RyuJit |      .NET 4.8 |    90.224 ns |  0.8573 ns |  1.2832 ns |    90.478 ns |  24.41 |    0.57 | 0.0050 |     - |     - |      32 B |
|    VariedProperties |    net48 RyuJit |    RyuJit |      .NET 4.8 |   380.224 ns |  5.1817 ns |  7.2641 ns |   380.021 ns | 102.97 |    3.03 | 0.0153 |     - |     - |      96 B |
|   ComplexProperties |    net48 RyuJit |    RyuJit |      .NET 4.8 | 1,927.867 ns | 22.1241 ns | 32.4293 ns | 1,925.825 ns | 521.95 |   16.16 | 0.1698 |     - |     - |    1075 B |
