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
|                                                 Method |             Job |       Jit |       Runtime |            Mean |        Error |       StdDev |      Ratio |  RatioSD |    Gen 0 |   Gen 1 |   Gen 2 | Allocated |
|------------------------------------------------------- |---------------- |---------- |-------------- |----------------:|-------------:|-------------:|-----------:|---------:|---------:|--------:|--------:|----------:|
|                                   EmitLogAIgnoredEvent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |        11.62 ns |     0.038 ns |     0.054 ns |       1.00 |     0.00 |        - |       - |       - |         - |
|                                           EmitLogEvent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |       613.36 ns |     2.984 ns |     4.467 ns |      52.80 |     0.55 |   0.0582 |       - |       - |     368 B |
|          EmitLogEventWith1Enrich0ForContext0LogContext |   core31 RyuJit |    RyuJit | .NET Core 3.1 |       687.92 ns |     6.366 ns |     9.331 ns |      59.24 |     0.96 |   0.0668 |       - |       - |     424 B |
|          EmitLogEventWith0Enrich1ForContext0LogContext |   core31 RyuJit |    RyuJit | .NET Core 3.1 |       664.71 ns |     5.089 ns |     6.966 ns |      57.19 |     0.83 |   0.0582 |       - |       - |     368 B |
|          EmitLogEventWith0Enrich0ForContext1LogContext |   core31 RyuJit |    RyuJit | .NET Core 3.1 |     1,176.92 ns |     5.901 ns |     8.463 ns |     101.29 |     0.65 |   0.1564 |       - |       - |     984 B |
|          EmitLogEventWith1Enrich1ForContext1LogContext |   core31 RyuJit |    RyuJit | .NET Core 3.1 |     1,375.74 ns |     5.773 ns |     8.279 ns |     118.40 |     0.65 |   0.2041 |       - |       - |    1288 B |
|       EmitLogEventWith10Enrich10ForContext10LogContext |   core31 RyuJit |    RyuJit | .NET Core 3.1 |     6,596.66 ns |    50.440 ns |    73.934 ns |     567.53 |     6.84 |   1.2054 |  0.0076 |       - |    7584 B |
|    EmitLogEventWith100Enrich100ForContext100LogContext |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    57,150.32 ns |   275.189 ns |   394.668 ns |   4,918.44 |    29.74 |  11.8408 |  1.0986 |       - |   74616 B |
| EmitLogEventWith1000Enrich1000ForContext1000LogContext |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 1,176,837.99 ns | 6,071.397 ns | 9,087.381 ns | 101,281.63 |   944.25 | 119.1406 | 54.6875 | 29.2969 |  735962 B |
|                                                        |                 |           |               |                 |              |              |            |          |          |         |         |           |
|                                   EmitLogAIgnoredEvent | net48 LegacyJit | LegacyJit |      .NET 4.8 |        14.00 ns |     0.059 ns |     0.084 ns |       1.00 |     0.00 |        - |       - |       - |         - |
|                                           EmitLogEvent | net48 LegacyJit | LegacyJit |      .NET 4.8 |       636.91 ns |     6.930 ns |     9.939 ns |      45.50 |     0.70 |   0.0591 |       - |       - |     377 B |
|          EmitLogEventWith1Enrich0ForContext0LogContext | net48 LegacyJit | LegacyJit |      .NET 4.8 |       711.23 ns |     2.314 ns |     3.392 ns |      50.82 |     0.40 |   0.0687 |       - |       - |     433 B |
|          EmitLogEventWith0Enrich1ForContext0LogContext | net48 LegacyJit | LegacyJit |      .NET 4.8 |       687.61 ns |     2.079 ns |     3.111 ns |      49.13 |     0.47 |   0.0591 |       - |       - |     377 B |
|          EmitLogEventWith0Enrich0ForContext1LogContext | net48 LegacyJit | LegacyJit |      .NET 4.8 |     1,313.31 ns |     9.209 ns |    12.910 ns |      93.84 |     1.36 |   0.1602 |       - |       - |    1011 B |
|          EmitLogEventWith1Enrich1ForContext1LogContext | net48 LegacyJit | LegacyJit |      .NET 4.8 |     1,512.26 ns |     8.709 ns |    12.490 ns |     108.04 |     1.48 |   0.2079 |       - |       - |    1316 B |
|       EmitLogEventWith10Enrich10ForContext10LogContext | net48 LegacyJit | LegacyJit |      .NET 4.8 |     7,560.21 ns |    42.806 ns |    64.070 ns |     539.74 |     3.57 |   1.1749 |       - |       - |    7486 B |
|    EmitLogEventWith100Enrich100ForContext100LogContext | net48 LegacyJit | LegacyJit |      .NET 4.8 |    69,690.09 ns |   244.002 ns |   365.211 ns |   4,977.34 |    22.03 |  11.7188 |  0.8545 |       - |   73988 B |
| EmitLogEventWith1000Enrich1000ForContext1000LogContext | net48 LegacyJit | LegacyJit |      .NET 4.8 | 1,272,964.03 ns | 5,253.436 ns | 7,863.095 ns |  90,922.22 |   605.91 | 119.1406 | 56.6406 | 29.2969 |  736998 B |
|                                                        |                 |           |               |                 |              |              |            |          |          |         |         |           |
|                                   EmitLogAIgnoredEvent |    net48 RyuJit |    RyuJit |      .NET 4.8 |        14.25 ns |     0.272 ns |     0.390 ns |       1.00 |     0.00 |        - |       - |       - |         - |
|                                           EmitLogEvent |    net48 RyuJit |    RyuJit |      .NET 4.8 |       627.65 ns |     2.076 ns |     3.043 ns |      44.09 |     1.36 |   0.0591 |       - |       - |     377 B |
|          EmitLogEventWith1Enrich0ForContext0LogContext |    net48 RyuJit |    RyuJit |      .NET 4.8 |       707.10 ns |     1.939 ns |     2.842 ns |      49.65 |     1.32 |   0.0687 |       - |       - |     433 B |
|          EmitLogEventWith0Enrich1ForContext0LogContext |    net48 RyuJit |    RyuJit |      .NET 4.8 |       682.43 ns |     1.315 ns |     1.843 ns |      47.98 |     1.25 |   0.0591 |       - |       - |     377 B |
|          EmitLogEventWith0Enrich0ForContext1LogContext |    net48 RyuJit |    RyuJit |      .NET 4.8 |     1,295.39 ns |     6.639 ns |     9.522 ns |      90.99 |     2.99 |   0.1602 |       - |       - |    1011 B |
|          EmitLogEventWith1Enrich1ForContext1LogContext |    net48 RyuJit |    RyuJit |      .NET 4.8 |     1,506.92 ns |     5.842 ns |     8.562 ns |     105.85 |     2.98 |   0.2079 |       - |       - |    1316 B |
|       EmitLogEventWith10Enrich10ForContext10LogContext |    net48 RyuJit |    RyuJit |      .NET 4.8 |     7,524.58 ns |    26.306 ns |    37.727 ns |     528.53 |    16.54 |   1.1749 |       - |       - |    7486 B |
|    EmitLogEventWith100Enrich100ForContext100LogContext |    net48 RyuJit |    RyuJit |      .NET 4.8 |    69,553.78 ns |   224.578 ns |   329.183 ns |   4,884.18 |   121.88 |  11.7188 |  0.8545 |       - |   73988 B |
| EmitLogEventWith1000Enrich1000ForContext1000LogContext |    net48 RyuJit |    RyuJit |      .NET 4.8 | 1,268,679.44 ns | 3,202.510 ns | 4,592.945 ns |  89,108.94 | 2,635.68 | 119.1406 | 56.6406 | 29.2969 |  736998 B |
