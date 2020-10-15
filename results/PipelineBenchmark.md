``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.572 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.402
  [Host]          : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT
  core31 RyuJit   : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT
  net48 LegacyJit : .NET Framework 4.8 (4.8.4250.0), X64 RyuJIT
  net48 RyuJit    : .NET Framework 4.8 (4.8.4250.0), X64 RyuJIT

IterationCount=15  LaunchCount=2  WarmupCount=10  

```
|                                                 Method |             Job |       Jit |       Runtime |            Mean |        Error |       StdDev |          Median |     Ratio |  RatioSD |    Gen 0 |   Gen 1 |   Gen 2 | Allocated |
|------------------------------------------------------- |---------------- |---------- |-------------- |----------------:|-------------:|-------------:|----------------:|----------:|---------:|---------:|--------:|--------:|----------:|
|                                   EmitLogAIgnoredEvent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |        12.25 ns |     0.083 ns |     0.119 ns |        12.27 ns |      1.00 |     0.00 |        - |       - |       - |         - |
|                                           EmitLogEvent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |       616.49 ns |     5.266 ns |     7.718 ns |       613.60 ns |     50.37 |     1.02 |   0.0582 |       - |       - |     368 B |
|          EmitLogEventWith1Enrich0ForContext0LogContext |   core31 RyuJit |    RyuJit | .NET Core 3.1 |       702.59 ns |     2.876 ns |     4.125 ns |       702.82 ns |     57.38 |     0.44 |   0.0668 |       - |       - |     424 B |
|          EmitLogEventWith0Enrich1ForContext0LogContext |   core31 RyuJit |    RyuJit | .NET Core 3.1 |       646.73 ns |     2.832 ns |     4.152 ns |       645.68 ns |     52.81 |     0.63 |   0.0582 |       - |       - |     368 B |
|          EmitLogEventWith0Enrich0ForContext1LogContext |   core31 RyuJit |    RyuJit | .NET Core 3.1 |     1,160.19 ns |     6.973 ns |    10.437 ns |     1,159.98 ns |     94.68 |     0.91 |   0.1564 |       - |       - |     984 B |
|          EmitLogEventWith1Enrich1ForContext1LogContext |   core31 RyuJit |    RyuJit | .NET Core 3.1 |     1,326.39 ns |     9.916 ns |    14.222 ns |     1,327.08 ns |    108.33 |     1.95 |   0.2041 |       - |       - |    1288 B |
|       EmitLogEventWith10Enrich10ForContext10LogContext |   core31 RyuJit |    RyuJit | .NET Core 3.1 |     6,091.54 ns |    26.320 ns |    36.898 ns |     6,103.98 ns |    497.60 |     4.04 |   1.2054 |  0.0076 |       - |    7584 B |
|    EmitLogEventWith100Enrich100ForContext100LogContext |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    53,244.34 ns |   381.003 ns |   570.268 ns |    53,190.07 ns |  4,352.18 |    76.02 |  11.8408 |  1.0376 |       - |   74617 B |
| EmitLogEventWith1000Enrich1000ForContext1000LogContext |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 1,099,395.98 ns | 6,254.948 ns | 8,970.659 ns | 1,099,144.82 ns | 89,789.90 | 1,315.63 | 119.1406 | 52.7344 | 29.2969 |  735962 B |
|                                                        |                 |           |               |                 |              |              |                 |           |          |          |         |         |           |
|                                   EmitLogAIgnoredEvent | net48 LegacyJit | LegacyJit |      .NET 4.8 |        13.37 ns |     0.346 ns |     0.507 ns |        13.70 ns |      1.00 |     0.00 |        - |       - |       - |         - |
|                                           EmitLogEvent | net48 LegacyJit | LegacyJit |      .NET 4.8 |       606.18 ns |     2.687 ns |     4.022 ns |       605.96 ns |     45.41 |     1.85 |   0.0591 |       - |       - |     377 B |
|          EmitLogEventWith1Enrich0ForContext0LogContext | net48 LegacyJit | LegacyJit |      .NET 4.8 |       673.49 ns |     2.764 ns |     4.137 ns |       673.28 ns |     50.44 |     1.80 |   0.0687 |       - |       - |     433 B |
|          EmitLogEventWith0Enrich1ForContext0LogContext | net48 LegacyJit | LegacyJit |      .NET 4.8 |       654.08 ns |     4.119 ns |     5.907 ns |       652.91 ns |     48.90 |     1.51 |   0.0591 |       - |       - |     377 B |
|          EmitLogEventWith0Enrich0ForContext1LogContext | net48 LegacyJit | LegacyJit |      .NET 4.8 |     1,245.04 ns |     4.982 ns |     7.302 ns |     1,245.14 ns |     93.26 |     3.64 |   0.1602 |       - |       - |    1011 B |
|          EmitLogEventWith1Enrich1ForContext1LogContext | net48 LegacyJit | LegacyJit |      .NET 4.8 |     1,443.55 ns |     6.226 ns |     9.318 ns |     1,441.63 ns |    108.15 |     4.12 |   0.2079 |       - |       - |    1316 B |
|       EmitLogEventWith10Enrich10ForContext10LogContext | net48 LegacyJit | LegacyJit |      .NET 4.8 |     6,971.83 ns |    22.280 ns |    32.657 ns |     6,970.34 ns |    522.23 |    20.09 |   1.1826 |  0.0076 |       - |    7486 B |
|    EmitLogEventWith100Enrich100ForContext100LogContext | net48 LegacyJit | LegacyJit |      .NET 4.8 |    64,998.49 ns |   493.760 ns |   723.747 ns |    64,967.80 ns |  4,870.18 |   228.02 |  11.7188 |  0.8545 |       - |   73988 B |
| EmitLogEventWith1000Enrich1000ForContext1000LogContext | net48 LegacyJit | LegacyJit |      .NET 4.8 | 1,184,193.36 ns | 5,505.022 ns | 8,239.657 ns | 1,182,408.59 ns | 88,729.23 | 3,376.63 | 119.1406 | 56.6406 | 29.2969 |  737000 B |
|                                                        |                 |           |               |                 |              |              |                 |           |          |          |         |         |           |
|                                   EmitLogAIgnoredEvent |    net48 RyuJit |    RyuJit |      .NET 4.8 |        13.53 ns |     0.222 ns |     0.326 ns |        13.32 ns |      1.00 |     0.00 |        - |       - |       - |         - |
|                                           EmitLogEvent |    net48 RyuJit |    RyuJit |      .NET 4.8 |       598.09 ns |     2.435 ns |     3.492 ns |       598.61 ns |     44.20 |     0.94 |   0.0591 |       - |       - |     377 B |
|          EmitLogEventWith1Enrich0ForContext0LogContext |    net48 RyuJit |    RyuJit |      .NET 4.8 |       670.55 ns |     2.340 ns |     3.430 ns |       670.52 ns |     49.59 |     1.20 |   0.0687 |       - |       - |     433 B |
|          EmitLogEventWith0Enrich1ForContext0LogContext |    net48 RyuJit |    RyuJit |      .NET 4.8 |       652.92 ns |     3.907 ns |     5.847 ns |       651.99 ns |     48.29 |     0.87 |   0.0591 |       - |       - |     377 B |
|          EmitLogEventWith0Enrich0ForContext1LogContext |    net48 RyuJit |    RyuJit |      .NET 4.8 |     1,263.77 ns |    17.586 ns |    25.222 ns |     1,253.14 ns |     93.38 |     1.96 |   0.1602 |       - |       - |    1011 B |
|          EmitLogEventWith1Enrich1ForContext1LogContext |    net48 RyuJit |    RyuJit |      .NET 4.8 |     1,437.05 ns |     5.766 ns |     8.452 ns |     1,437.30 ns |    106.28 |     2.77 |   0.2079 |       - |       - |    1316 B |
|       EmitLogEventWith10Enrich10ForContext10LogContext |    net48 RyuJit |    RyuJit |      .NET 4.8 |     7,052.41 ns |    24.897 ns |    36.493 ns |     7,055.63 ns |    521.60 |    14.15 |   1.1826 |  0.0076 |       - |    7486 B |
|    EmitLogEventWith100Enrich100ForContext100LogContext |    net48 RyuJit |    RyuJit |      .NET 4.8 |    64,774.74 ns |   321.623 ns |   471.431 ns |    64,777.43 ns |  4,790.00 |    99.08 |  11.7188 |  0.8545 |       - |   73988 B |
| EmitLogEventWith1000Enrich1000ForContext1000LogContext |    net48 RyuJit |    RyuJit |      .NET 4.8 | 1,181,690.68 ns | 4,766.468 ns | 7,134.225 ns | 1,180,755.08 ns | 87,400.11 | 2,217.25 | 119.1406 | 56.6406 | 29.2969 |  737000 B |
