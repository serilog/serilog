``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.572 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.403
  [Host]          : .NET Core 3.1.9 (CoreCLR 4.700.20.47201, CoreFX 4.700.20.47203), X64 RyuJIT
  core31 RyuJit   : .NET Core 3.1.9 (CoreCLR 4.700.20.47201, CoreFX 4.700.20.47203), X64 RyuJIT
  net48 LegacyJit : .NET Framework 4.8 (4.8.4250.0), X64 RyuJIT
  net48 RyuJit    : .NET Framework 4.8 (4.8.4250.0), X64 RyuJIT

IterationCount=15  LaunchCount=2  WarmupCount=10  

```
|              Method |             Job |       Jit |       Runtime |         Mean |     Error |     StdDev |       Median |  Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------------- |---------------- |---------- |-------------- |-------------:|----------:|-----------:|-------------:|-------:|--------:|-------:|------:|------:|----------:|
|           NoMessage |   core31 RyuJit |    RyuJit | .NET Core 3.1 |     3.845 ns | 0.2590 ns |  0.3796 ns |     3.556 ns |   1.00 |    0.00 |      - |     - |     - |         - |
|        NoProperties |   core31 RyuJit |    RyuJit | .NET Core 3.1 |     3.498 ns | 0.0294 ns |  0.0441 ns |     3.493 ns |   0.92 |    0.10 |      - |     - |     - |         - |
| OneSimpleProperties |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    47.829 ns | 0.2368 ns |  0.3396 ns |    47.800 ns |  12.61 |    1.23 |      - |     - |     - |         - |
|    VariedProperties |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   280.303 ns | 2.1881 ns |  3.2073 ns |   279.765 ns |  73.53 |    6.75 | 0.0153 |     - |     - |      96 B |
|   ComplexProperties |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 1,227.848 ns | 8.5197 ns | 12.2187 ns | 1,228.462 ns | 323.44 |   29.53 | 0.1259 |     - |     - |     800 B |
|                     |                 |           |               |              |           |            |              |        |         |        |       |       |           |
|           NoMessage | net48 LegacyJit | LegacyJit |      .NET 4.8 |     4.891 ns | 0.0242 ns |  0.0362 ns |     4.888 ns |   1.00 |    0.00 |      - |     - |     - |         - |
|        NoProperties | net48 LegacyJit | LegacyJit |      .NET 4.8 |     4.883 ns | 0.0218 ns |  0.0320 ns |     4.878 ns |   1.00 |    0.01 |      - |     - |     - |         - |
| OneSimpleProperties | net48 LegacyJit | LegacyJit |      .NET 4.8 |    84.856 ns | 0.3908 ns |  0.5728 ns |    84.992 ns |  17.35 |    0.21 | 0.0050 |     - |     - |      32 B |
|    VariedProperties | net48 LegacyJit | LegacyJit |      .NET 4.8 |   359.656 ns | 3.9463 ns |  5.7844 ns |   359.074 ns |  73.53 |    1.26 | 0.0153 |     - |     - |      96 B |
|   ComplexProperties | net48 LegacyJit | LegacyJit |      .NET 4.8 | 1,817.717 ns | 6.3146 ns |  9.2559 ns | 1,817.773 ns | 371.61 |    3.38 | 0.1698 |     - |     - |    1075 B |
|                     |                 |           |               |              |           |            |              |        |         |        |       |       |           |
|           NoMessage |    net48 RyuJit |    RyuJit |      .NET 4.8 |     5.010 ns | 0.0875 ns |  0.1309 ns |     5.014 ns |   1.00 |    0.00 |      - |     - |     - |         - |
|        NoProperties |    net48 RyuJit |    RyuJit |      .NET 4.8 |     4.888 ns | 0.0232 ns |  0.0332 ns |     4.886 ns |   0.98 |    0.02 |      - |     - |     - |         - |
| OneSimpleProperties |    net48 RyuJit |    RyuJit |      .NET 4.8 |    84.861 ns | 0.3717 ns |  0.5564 ns |    84.834 ns |  16.95 |    0.47 | 0.0050 |     - |     - |      32 B |
|    VariedProperties |    net48 RyuJit |    RyuJit |      .NET 4.8 |   357.129 ns | 1.4083 ns |  2.0197 ns |   357.273 ns |  71.47 |    1.93 | 0.0153 |     - |     - |      96 B |
|   ComplexProperties |    net48 RyuJit |    RyuJit |      .NET 4.8 | 1,822.887 ns | 8.3844 ns | 12.5494 ns | 1,825.833 ns | 364.16 |   11.49 | 0.1698 |     - |     - |    1075 B |
