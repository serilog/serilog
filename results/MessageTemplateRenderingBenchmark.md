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
|           NoMessage | core31 | .NET Core 3.1 |     3.615 ns |  0.3406 ns |  0.5098 ns |   1.00 |    0.00 |      - |     - |     - |         - |
|        NoProperties | core31 | .NET Core 3.1 |     3.581 ns |  0.3505 ns |  0.5246 ns |   1.03 |    0.29 |      - |     - |     - |         - |
| OneSimpleProperties | core31 | .NET Core 3.1 |    49.677 ns |  0.6915 ns |  0.9917 ns |  14.12 |    1.83 |      - |     - |     - |         - |
|    VariedProperties | core31 | .NET Core 3.1 |   286.184 ns |  4.1996 ns |  6.2857 ns |  80.76 |   11.80 | 0.0153 |     - |     - |      96 B |
|   ComplexProperties | core31 | .NET Core 3.1 | 1,251.811 ns | 18.1765 ns | 27.2057 ns | 352.82 |   48.67 | 0.1259 |     - |     - |     800 B |
|                     |        |               |              |            |            |        |         |        |       |       |           |
|           NoMessage |  net48 |      .NET 4.8 |     4.276 ns |  0.0682 ns |  0.1020 ns |   1.00 |    0.00 |      - |     - |     - |         - |
|        NoProperties |  net48 |      .NET 4.8 |     4.245 ns |  0.0528 ns |  0.0790 ns |   0.99 |    0.04 |      - |     - |     - |         - |
| OneSimpleProperties |  net48 |      .NET 4.8 |    84.629 ns |  1.1080 ns |  1.6583 ns |  19.80 |    0.67 | 0.0050 |     - |     - |      32 B |
|    VariedProperties |  net48 |      .NET 4.8 |   367.149 ns |  4.6453 ns |  6.9529 ns |  85.89 |    1.72 | 0.0153 |     - |     - |      96 B |
|   ComplexProperties |  net48 |      .NET 4.8 | 1,830.743 ns | 13.2930 ns | 19.4847 ns | 428.82 |   10.62 | 0.1698 |     - |     - |    1075 B |
|                     |        |               |              |            |            |        |         |        |       |       |           |
|           NoMessage |  net50 | .NET Core 5.0 |     3.965 ns |  0.0651 ns |  0.0974 ns |   1.00 |    0.00 |      - |     - |     - |         - |
|        NoProperties |  net50 | .NET Core 5.0 |     3.863 ns |  0.0880 ns |  0.1318 ns |   0.98 |    0.05 |      - |     - |     - |         - |
| OneSimpleProperties |  net50 | .NET Core 5.0 |    38.172 ns |  0.4563 ns |  0.6829 ns |   9.63 |    0.27 |      - |     - |     - |         - |
|    VariedProperties |  net50 | .NET Core 5.0 |   239.037 ns |  3.7318 ns |  5.5855 ns |  60.31 |    1.65 | 0.0153 |     - |     - |      96 B |
|   ComplexProperties |  net50 | .NET Core 5.0 | 1,112.097 ns | 17.8198 ns | 26.6718 ns | 280.71 |   11.24 | 0.1259 |     - |     - |     800 B |
