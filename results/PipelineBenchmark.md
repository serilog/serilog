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
|                                                 Method |             Job |       Jit |       Runtime |            Mean |         Error |        StdDev |          Median |     Ratio |  RatioSD |    Gen 0 |   Gen 1 |   Gen 2 | Allocated |
|------------------------------------------------------- |---------------- |---------- |-------------- |----------------:|--------------:|--------------:|----------------:|----------:|---------:|---------:|--------:|--------:|----------:|
|                                   EmitLogAIgnoredEvent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |        12.68 ns |      0.929 ns |      1.303 ns |        11.73 ns |      1.00 |     0.00 |        - |       - |       - |         - |
|                                           EmitLogEvent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |       621.49 ns |      5.796 ns |      8.675 ns |       619.93 ns |     49.49 |     5.01 |   0.0582 |       - |       - |     368 B |
|          EmitLogEventWith1Enrich0ForContext0LogContext |   core31 RyuJit |    RyuJit | .NET Core 3.1 |       676.19 ns |      7.141 ns |     10.689 ns |       678.25 ns |     53.89 |     5.67 |   0.0668 |       - |       - |     424 B |
|          EmitLogEventWith0Enrich1ForContext0LogContext |   core31 RyuJit |    RyuJit | .NET Core 3.1 |       655.85 ns |      5.140 ns |      7.694 ns |       657.17 ns |     52.24 |     5.22 |   0.0582 |       - |       - |     368 B |
|          EmitLogEventWith0Enrich0ForContext1LogContext |   core31 RyuJit |    RyuJit | .NET Core 3.1 |     1,183.11 ns |     14.170 ns |     21.209 ns |     1,179.53 ns |     94.37 |     9.90 |   0.1564 |       - |       - |     984 B |
|          EmitLogEventWith1Enrich1ForContext1LogContext |   core31 RyuJit |    RyuJit | .NET Core 3.1 |     1,394.33 ns |     20.089 ns |     30.069 ns |     1,388.04 ns |    111.48 |    12.76 |   0.2041 |       - |       - |    1288 B |
|       EmitLogEventWith10Enrich10ForContext10LogContext |   core31 RyuJit |    RyuJit | .NET Core 3.1 |     6,551.82 ns |     62.093 ns |     91.015 ns |     6,549.10 ns |    521.55 |    49.03 |   1.2054 |  0.0076 |       - |    7584 B |
|    EmitLogEventWith100Enrich100ForContext100LogContext |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    57,067.13 ns |    444.222 ns |    664.891 ns |    57,196.57 ns |  4,540.12 |   428.57 |  11.8408 |  1.0986 |       - |   74616 B |
| EmitLogEventWith1000Enrich1000ForContext1000LogContext |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 1,254,466.90 ns | 55,534.955 ns | 79,646.573 ns | 1,244,904.00 ns | 99,649.51 | 8,174.18 | 119.1406 | 54.6875 | 29.2969 |  735962 B |
|                                                        |                 |           |               |                 |               |               |                 |           |          |          |         |         |           |
|                                   EmitLogAIgnoredEvent | net48 LegacyJit | LegacyJit |      .NET 4.8 |        14.24 ns |      0.206 ns |      0.301 ns |        14.19 ns |      1.00 |     0.00 |        - |       - |       - |         - |
|                                           EmitLogEvent | net48 LegacyJit | LegacyJit |      .NET 4.8 |       648.95 ns |      5.856 ns |      8.765 ns |       648.82 ns |     45.58 |     1.06 |   0.0591 |       - |       - |     377 B |
|          EmitLogEventWith1Enrich0ForContext0LogContext | net48 LegacyJit | LegacyJit |      .NET 4.8 |       731.46 ns |      6.541 ns |      8.954 ns |       733.10 ns |     51.45 |     0.96 |   0.0687 |       - |       - |     433 B |
|          EmitLogEventWith0Enrich1ForContext0LogContext | net48 LegacyJit | LegacyJit |      .NET 4.8 |       703.07 ns |     17.816 ns |     26.115 ns |       692.42 ns |     49.39 |     2.25 |   0.0591 |       - |       - |     377 B |
|          EmitLogEventWith0Enrich0ForContext1LogContext | net48 LegacyJit | LegacyJit |      .NET 4.8 |     1,336.43 ns |      9.267 ns |     13.584 ns |     1,335.95 ns |     93.87 |     2.18 |   0.1602 |       - |       - |    1011 B |
|          EmitLogEventWith1Enrich1ForContext1LogContext | net48 LegacyJit | LegacyJit |      .NET 4.8 |     1,555.30 ns |     14.095 ns |     20.660 ns |     1,559.76 ns |    109.25 |     2.75 |   0.2079 |       - |       - |    1316 B |
|       EmitLogEventWith10Enrich10ForContext10LogContext | net48 LegacyJit | LegacyJit |      .NET 4.8 |     7,635.84 ns |     89.313 ns |    133.679 ns |     7,612.02 ns |    536.53 |    11.26 |   1.1749 |       - |       - |    7486 B |
|    EmitLogEventWith100Enrich100ForContext100LogContext | net48 LegacyJit | LegacyJit |      .NET 4.8 |    69,877.33 ns |    632.124 ns |    946.134 ns |    69,586.07 ns |  4,907.58 |   113.90 |  11.7188 |  0.8545 |       - |   73988 B |
| EmitLogEventWith1000Enrich1000ForContext1000LogContext | net48 LegacyJit | LegacyJit |      .NET 4.8 | 1,281,024.06 ns | 14,779.181 ns | 22,120.783 ns | 1,282,758.79 ns | 89,930.60 | 2,575.43 | 119.1406 | 56.6406 | 29.2969 |  737000 B |
|                                                        |                 |           |               |                 |               |               |                 |           |          |          |         |         |           |
|                                   EmitLogAIgnoredEvent |    net48 RyuJit |    RyuJit |      .NET 4.8 |        13.88 ns |      0.150 ns |      0.225 ns |        13.88 ns |      1.00 |     0.00 |        - |       - |       - |         - |
|                                           EmitLogEvent |    net48 RyuJit |    RyuJit |      .NET 4.8 |       634.22 ns |      7.094 ns |     10.618 ns |       633.05 ns |     45.70 |     0.84 |   0.0591 |       - |       - |     377 B |
|          EmitLogEventWith1Enrich0ForContext0LogContext |    net48 RyuJit |    RyuJit |      .NET 4.8 |       708.07 ns |      6.583 ns |      9.853 ns |       706.29 ns |     51.03 |     1.17 |   0.0687 |       - |       - |     433 B |
|          EmitLogEventWith0Enrich1ForContext0LogContext |    net48 RyuJit |    RyuJit |      .NET 4.8 |       682.93 ns |      5.563 ns |      8.327 ns |       682.09 ns |     49.22 |     0.94 |   0.0591 |       - |       - |     377 B |
|          EmitLogEventWith0Enrich0ForContext1LogContext |    net48 RyuJit |    RyuJit |      .NET 4.8 |     1,365.74 ns |     12.153 ns |     17.430 ns |     1,363.11 ns |     98.40 |     2.07 |   0.1602 |       - |       - |    1011 B |
|          EmitLogEventWith1Enrich1ForContext1LogContext |    net48 RyuJit |    RyuJit |      .NET 4.8 |     1,556.83 ns |     16.205 ns |     23.753 ns |     1,556.98 ns |    112.13 |     2.61 |   0.2079 |       - |       - |    1316 B |
|       EmitLogEventWith10Enrich10ForContext10LogContext |    net48 RyuJit |    RyuJit |      .NET 4.8 |     7,691.39 ns |    105.803 ns |    158.360 ns |     7,661.43 ns |    554.33 |    15.87 |   1.1749 |       - |       - |    7486 B |
|    EmitLogEventWith100Enrich100ForContext100LogContext |    net48 RyuJit |    RyuJit |      .NET 4.8 |    70,071.62 ns |    715.533 ns |  1,070.976 ns |    69,915.05 ns |  5,049.32 |    86.53 |  11.7188 |  0.8545 |       - |   73988 B |
| EmitLogEventWith1000Enrich1000ForContext1000LogContext |    net48 RyuJit |    RyuJit |      .NET 4.8 | 1,288,544.56 ns | 16,055.843 ns | 24,031.629 ns | 1,289,247.46 ns | 92,866.72 | 2,520.74 | 119.1406 | 56.6406 | 29.2969 |  737000 B |
