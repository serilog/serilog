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
|           NoMessage | core31 | .NET Core 3.1 |     3.257 ns |  0.0614 ns |  0.0919 ns |   1.00 |    0.00 |      - |     - |     - |         - |
|        NoProperties | core31 | .NET Core 3.1 |     3.286 ns |  0.1023 ns |  0.1531 ns |   1.01 |    0.05 |      - |     - |     - |         - |
| OneSimpleProperties | core31 | .NET Core 3.1 |    48.905 ns |  0.4429 ns |  0.6630 ns |  15.03 |    0.45 |      - |     - |     - |         - |
|    VariedProperties | core31 | .NET Core 3.1 |   287.072 ns |  4.8001 ns |  7.0359 ns |  88.15 |    2.98 | 0.0153 |     - |     - |      96 B |
|   ComplexProperties | core31 | .NET Core 3.1 | 1,262.517 ns | 17.5777 ns | 26.3095 ns | 388.05 |   15.43 | 0.1259 |     - |     - |     800 B |
|                     |        |               |              |            |            |        |         |        |       |       |           |
|           NoMessage |  net48 |      .NET 4.8 |     3.746 ns |  0.2710 ns |  0.4056 ns |   1.00 |    0.00 |      - |     - |     - |         - |
|        NoProperties |  net48 |      .NET 4.8 |     3.375 ns |  0.0680 ns |  0.1017 ns |   0.91 |    0.10 |      - |     - |     - |         - |
| OneSimpleProperties |  net48 |      .NET 4.8 |    85.259 ns |  0.9959 ns |  1.4907 ns |  23.02 |    2.57 | 0.0050 |     - |     - |      32 B |
|    VariedProperties |  net48 |      .NET 4.8 |   362.624 ns |  5.3245 ns |  7.9694 ns |  97.87 |   10.52 | 0.0153 |     - |     - |      96 B |
|   ComplexProperties |  net48 |      .NET 4.8 | 1,837.221 ns | 13.0609 ns | 19.5490 ns | 496.12 |   55.01 | 0.1698 |     - |     - |    1075 B |
|                     |        |               |              |            |            |        |         |        |       |       |           |
|           NoMessage |  net50 | .NET Core 5.0 |     3.946 ns |  0.0613 ns |  0.0917 ns |   1.00 |    0.00 |      - |     - |     - |         - |
|        NoProperties |  net50 | .NET Core 5.0 |     3.740 ns |  0.0670 ns |  0.1003 ns |   0.95 |    0.03 |      - |     - |     - |         - |
| OneSimpleProperties |  net50 | .NET Core 5.0 |    37.898 ns |  0.4890 ns |  0.7318 ns |   9.61 |    0.26 |      - |     - |     - |         - |
|    VariedProperties |  net50 | .NET Core 5.0 |   239.074 ns |  4.7686 ns |  7.1374 ns |  60.63 |    2.54 | 0.0153 |     - |     - |      96 B |
|   ComplexProperties |  net50 | .NET Core 5.0 | 1,123.258 ns | 25.2495 ns | 37.7923 ns | 284.77 |   10.95 | 0.1259 |     - |     - |     800 B |
