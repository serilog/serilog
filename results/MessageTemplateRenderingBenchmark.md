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
|           NoMessage |   core31 RyuJit |    RyuJit | .NET Core 3.1 |     3.179 ns |  0.0169 ns |  0.0248 ns |     3.179 ns |   1.00 |    0.00 |      - |     - |     - |         - |
|        NoProperties |   core31 RyuJit |    RyuJit | .NET Core 3.1 |     3.179 ns |  0.0128 ns |  0.0187 ns |     3.182 ns |   1.00 |    0.01 |      - |     - |     - |         - |
| OneSimpleProperties |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    48.626 ns |  1.1507 ns |  1.7222 ns |    47.947 ns |  15.31 |    0.58 |      - |     - |     - |         - |
|    VariedProperties |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   277.792 ns |  1.4267 ns |  2.0000 ns |   278.321 ns |  87.45 |    0.99 | 0.0153 |     - |     - |      96 B |
|   ComplexProperties |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 1,260.927 ns | 12.7173 ns | 18.6409 ns | 1,257.829 ns | 396.70 |    5.17 | 0.1259 |     - |     - |     800 B |
|                     |                 |           |               |              |            |            |              |        |         |        |       |       |           |
|           NoMessage | net48 LegacyJit | LegacyJit |      .NET 4.8 |     3.428 ns |  0.1055 ns |  0.1546 ns |     3.316 ns |   1.00 |    0.00 |      - |     - |     - |         - |
|        NoProperties | net48 LegacyJit | LegacyJit |      .NET 4.8 |     3.391 ns |  0.0682 ns |  0.1000 ns |     3.441 ns |   0.99 |    0.02 |      - |     - |     - |         - |
| OneSimpleProperties | net48 LegacyJit | LegacyJit |      .NET 4.8 |    81.460 ns |  0.3208 ns |  0.4600 ns |    81.482 ns |  23.77 |    1.06 | 0.0050 |     - |     - |      32 B |
|    VariedProperties | net48 LegacyJit | LegacyJit |      .NET 4.8 |   352.900 ns |  1.4748 ns |  2.2074 ns |   352.766 ns | 103.10 |    4.42 | 0.0153 |     - |     - |      96 B |
|   ComplexProperties | net48 LegacyJit | LegacyJit |      .NET 4.8 | 1,827.160 ns |  6.9806 ns | 10.2321 ns | 1,827.456 ns | 534.07 |   26.06 | 0.1698 |     - |     - |    1075 B |
|                     |                 |           |               |              |            |            |              |        |         |        |       |       |           |
|           NoMessage |    net48 RyuJit |    RyuJit |      .NET 4.8 |     3.302 ns |  0.0188 ns |  0.0269 ns |     3.299 ns |   1.00 |    0.00 |      - |     - |     - |         - |
|        NoProperties |    net48 RyuJit |    RyuJit |      .NET 4.8 |     3.280 ns |  0.0139 ns |  0.0204 ns |     3.277 ns |   0.99 |    0.01 |      - |     - |     - |         - |
| OneSimpleProperties |    net48 RyuJit |    RyuJit |      .NET 4.8 |    81.446 ns |  0.2973 ns |  0.4358 ns |    81.466 ns |  24.67 |    0.27 | 0.0050 |     - |     - |      32 B |
|    VariedProperties |    net48 RyuJit |    RyuJit |      .NET 4.8 |   357.689 ns |  6.6685 ns |  9.9811 ns |   354.095 ns | 108.40 |    3.26 | 0.0153 |     - |     - |      96 B |
|   ComplexProperties |    net48 RyuJit |    RyuJit |      .NET 4.8 | 1,813.178 ns |  6.6052 ns |  9.6818 ns | 1,811.085 ns | 549.13 |    5.10 | 0.1698 |     - |     - |    1075 B |
