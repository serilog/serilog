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
|                                                 Method |             Job |       Jit |       Runtime |            Mean |         Error |        StdDev |     Ratio |  RatioSD |    Gen 0 |   Gen 1 |   Gen 2 | Allocated |
|------------------------------------------------------- |---------------- |---------- |-------------- |----------------:|--------------:|--------------:|----------:|---------:|---------:|--------:|--------:|----------:|
|                                   EmitLogAIgnoredEvent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |        10.93 ns |      0.042 ns |      0.063 ns |      1.00 |     0.00 |        - |       - |       - |         - |
|                                           EmitLogEvent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |       612.18 ns |      1.340 ns |      1.965 ns |     56.04 |     0.36 |   0.0582 |       - |       - |     368 B |
|          EmitLogEventWith1Enrich0ForContext0LogContext |   core31 RyuJit |    RyuJit | .NET Core 3.1 |       681.52 ns |      1.412 ns |      2.114 ns |     62.38 |     0.45 |   0.0668 |       - |       - |     424 B |
|          EmitLogEventWith0Enrich1ForContext0LogContext |   core31 RyuJit |    RyuJit | .NET Core 3.1 |       654.88 ns |      1.617 ns |      2.371 ns |     59.95 |     0.40 |   0.0582 |       - |       - |     368 B |
|          EmitLogEventWith0Enrich0ForContext1LogContext |   core31 RyuJit |    RyuJit | .NET Core 3.1 |     1,164.34 ns |      7.065 ns |     10.133 ns |    106.63 |     1.13 |   0.1564 |       - |       - |     984 B |
|          EmitLogEventWith1Enrich1ForContext1LogContext |   core31 RyuJit |    RyuJit | .NET Core 3.1 |     1,292.24 ns |      8.295 ns |     11.897 ns |    118.34 |     1.31 |   0.1640 |       - |       - |    1040 B |
|       EmitLogEventWith10Enrich10ForContext10LogContext |   core31 RyuJit |    RyuJit | .NET Core 3.1 |     6,088.92 ns |     14.899 ns |     21.838 ns |    557.42 |     3.24 |   1.2054 |  0.0076 |       - |    7584 B |
|    EmitLogEventWith100Enrich100ForContext100LogContext |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    52,306.75 ns |    212.565 ns |    304.854 ns |  4,790.09 |    33.25 |  11.8408 |  0.9766 |       - |   74616 B |
| EmitLogEventWith1000Enrich1000ForContext1000LogContext |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   977,154.11 ns | 26,610.475 ns | 39,829.305 ns | 89,435.42 | 3,753.94 | 119.1406 | 54.6875 | 29.2969 |  735945 B |
|                                                        |                 |           |               |                 |               |               |           |          |          |         |         |           |
|                                   EmitLogAIgnoredEvent | net48 LegacyJit | LegacyJit |      .NET 4.8 |        13.73 ns |      0.393 ns |      0.588 ns |      1.00 |     0.00 |        - |       - |       - |         - |
|                                           EmitLogEvent | net48 LegacyJit | LegacyJit |      .NET 4.8 |       622.19 ns |      1.438 ns |      2.062 ns |     45.28 |     1.88 |   0.0591 |       - |       - |     377 B |
|          EmitLogEventWith1Enrich0ForContext0LogContext | net48 LegacyJit | LegacyJit |      .NET 4.8 |       683.41 ns |      1.848 ns |      2.708 ns |     49.81 |     2.18 |   0.0687 |       - |       - |     433 B |
|          EmitLogEventWith0Enrich1ForContext0LogContext | net48 LegacyJit | LegacyJit |      .NET 4.8 |       671.80 ns |      2.180 ns |      3.195 ns |     48.97 |     2.24 |   0.0591 |       - |       - |     377 B |
|          EmitLogEventWith0Enrich0ForContext1LogContext | net48 LegacyJit | LegacyJit |      .NET 4.8 |     1,299.02 ns |     14.612 ns |     21.870 ns |     94.86 |     5.59 |   0.1602 |       - |       - |    1011 B |
|          EmitLogEventWith1Enrich1ForContext1LogContext | net48 LegacyJit | LegacyJit |      .NET 4.8 |     1,419.90 ns |     13.247 ns |     19.828 ns |    103.68 |     5.82 |   0.1678 |       - |       - |    1067 B |
|       EmitLogEventWith10Enrich10ForContext10LogContext | net48 LegacyJit | LegacyJit |      .NET 4.8 |     6,980.59 ns |     34.194 ns |     47.936 ns |    507.34 |    24.72 |   1.1826 |  0.0076 |       - |    7486 B |
|    EmitLogEventWith100Enrich100ForContext100LogContext | net48 LegacyJit | LegacyJit |      .NET 4.8 |    63,733.82 ns |    141.147 ns |    202.429 ns |  4,637.84 |   194.31 |  11.7188 |  0.8545 |       - |   73988 B |
| EmitLogEventWith1000Enrich1000ForContext1000LogContext | net48 LegacyJit | LegacyJit |      .NET 4.8 | 1,179,479.16 ns |  4,479.819 ns |  6,566.460 ns | 85,963.44 | 3,749.20 | 119.1406 | 56.6406 | 29.2969 |  736889 B |
|                                                        |                 |           |               |                 |               |               |           |          |          |         |         |           |
|                                   EmitLogAIgnoredEvent |    net48 RyuJit |    RyuJit |      .NET 4.8 |        14.00 ns |      0.396 ns |      0.555 ns |      1.00 |     0.00 |        - |       - |       - |         - |
|                                           EmitLogEvent |    net48 RyuJit |    RyuJit |      .NET 4.8 |       625.77 ns |      1.603 ns |      2.299 ns |     44.77 |     1.87 |   0.0591 |       - |       - |     377 B |
|          EmitLogEventWith1Enrich0ForContext0LogContext |    net48 RyuJit |    RyuJit |      .NET 4.8 |       693.38 ns |      3.688 ns |      5.406 ns |     49.67 |     2.26 |   0.0687 |       - |       - |     433 B |
|          EmitLogEventWith0Enrich1ForContext0LogContext |    net48 RyuJit |    RyuJit |      .NET 4.8 |       666.38 ns |      1.552 ns |      2.226 ns |     47.68 |     1.95 |   0.0591 |       - |       - |     377 B |
|          EmitLogEventWith0Enrich0ForContext1LogContext |    net48 RyuJit |    RyuJit |      .NET 4.8 |     1,282.62 ns |      4.303 ns |      6.172 ns |     91.76 |     3.83 |   0.1602 |       - |       - |    1011 B |
|          EmitLogEventWith1Enrich1ForContext1LogContext |    net48 RyuJit |    RyuJit |      .NET 4.8 |     1,429.31 ns |     12.794 ns |     18.753 ns |    102.14 |     2.90 |   0.1678 |       - |       - |    1067 B |
|       EmitLogEventWith10Enrich10ForContext10LogContext |    net48 RyuJit |    RyuJit |      .NET 4.8 |     6,985.08 ns |     36.253 ns |     54.262 ns |    500.33 |    23.02 |   1.1826 |  0.0076 |       - |    7486 B |
|    EmitLogEventWith100Enrich100ForContext100LogContext |    net48 RyuJit |    RyuJit |      .NET 4.8 |    63,234.14 ns |    254.209 ns |    356.366 ns |  4,525.07 |   201.64 |  11.7188 |  0.8545 |       - |   73988 B |
| EmitLogEventWith1000Enrich1000ForContext1000LogContext |    net48 RyuJit |    RyuJit |      .NET 4.8 | 1,186,330.61 ns |  4,612.544 ns |  6,903.839 ns | 84,869.15 | 3,607.99 | 119.1406 | 56.6406 | 29.2969 |  736889 B |
