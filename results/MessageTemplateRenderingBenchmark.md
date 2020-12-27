``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=5.0.101
  [Host] : .NET Core 5.0.1 (CoreCLR 5.0.120.57516, CoreFX 5.0.120.57516), X64 RyuJIT
  core31 : .NET Core 3.1.10 (CoreCLR 4.700.20.51601, CoreFX 4.700.20.51901), X64 RyuJIT
  net48  : .NET Framework 4.8 (4.8.4300.0), X64 RyuJIT
  net50  : .NET Core 5.0.1 (CoreCLR 5.0.120.57516, CoreFX 5.0.120.57516), X64 RyuJIT

Jit=RyuJit  IterationCount=15  LaunchCount=2  
WarmupCount=10  

```
|              Method |    Job |       Runtime |         Mean |      Error |     StdDev |  Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------------- |------- |-------------- |-------------:|-----------:|-----------:|-------:|--------:|-------:|------:|------:|----------:|
|           NoMessage | core31 | .NET Core 3.1 |     4.644 ns |  0.0381 ns |  0.0571 ns |   1.00 |    0.00 |      - |     - |     - |         - |
|        NoProperties | core31 | .NET Core 3.1 |     4.312 ns |  0.2444 ns |  0.3659 ns |   0.93 |    0.08 |      - |     - |     - |         - |
| OneSimpleProperties | core31 | .NET Core 3.1 |    52.000 ns |  0.4070 ns |  0.6091 ns |  11.20 |    0.22 |      - |     - |     - |         - |
|    VariedProperties | core31 | .NET Core 3.1 |   289.562 ns |  2.4766 ns |  3.7068 ns |  62.36 |    1.12 | 0.0153 |     - |     - |      96 B |
|   ComplexProperties | core31 | .NET Core 3.1 | 1,261.014 ns | 12.9509 ns | 19.3843 ns | 271.56 |    4.74 | 0.1259 |     - |     - |     800 B |
|                     |        |               |              |            |            |        |         |        |       |       |           |
|           NoMessage |  net48 |      .NET 4.8 |     3.546 ns |  0.0388 ns |  0.0557 ns |   1.00 |    0.00 |      - |     - |     - |         - |
|        NoProperties |  net48 |      .NET 4.8 |     3.568 ns |  0.0500 ns |  0.0749 ns |   1.01 |    0.03 |      - |     - |     - |         - |
| OneSimpleProperties |  net48 |      .NET 4.8 |    85.477 ns |  0.8748 ns |  1.3094 ns |  24.11 |    0.52 | 0.0050 |     - |     - |      32 B |
|    VariedProperties |  net48 |      .NET 4.8 |   367.383 ns |  2.5793 ns |  3.8605 ns | 103.69 |    1.95 | 0.0153 |     - |     - |      96 B |
|   ComplexProperties |  net48 |      .NET 4.8 | 1,827.475 ns | 11.4606 ns | 17.1537 ns | 515.38 |    9.58 | 0.1698 |     - |     - |    1075 B |
|                     |        |               |              |            |            |        |         |        |       |       |           |
|           NoMessage |  net50 | .NET Core 5.0 |     4.309 ns |  0.0799 ns |  0.1197 ns |   1.00 |    0.00 |      - |     - |     - |         - |
|        NoProperties |  net50 | .NET Core 5.0 |     4.448 ns |  0.0327 ns |  0.0489 ns |   1.03 |    0.03 |      - |     - |     - |         - |
| OneSimpleProperties |  net50 | .NET Core 5.0 |    38.485 ns |  0.3287 ns |  0.4608 ns |   8.91 |    0.27 |      - |     - |     - |         - |
|    VariedProperties |  net50 | .NET Core 5.0 |   244.141 ns |  2.0045 ns |  3.0003 ns |  56.69 |    1.50 | 0.0153 |     - |     - |      96 B |
|   ComplexProperties |  net50 | .NET Core 5.0 | 1,104.256 ns | 11.6734 ns | 17.4722 ns | 256.43 |    7.90 | 0.1259 |     - |     - |     800 B |
