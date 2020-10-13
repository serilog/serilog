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
|                                                 Method |             Job |       Jit |       Runtime |            Mean |        Error |        StdDev |     Ratio |  RatioSD |    Gen 0 |   Gen 1 |   Gen 2 | Allocated |
|------------------------------------------------------- |---------------- |---------- |-------------- |----------------:|-------------:|--------------:|----------:|---------:|---------:|--------:|--------:|----------:|
|                                   EmitLogAIgnoredEvent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |        12.39 ns |     0.043 ns |      0.059 ns |      1.00 |     0.00 |        - |       - |       - |         - |
|                                           EmitLogEvent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |       616.19 ns |     2.511 ns |      3.758 ns |     49.72 |     0.39 |   0.0582 |       - |       - |     368 B |
|          EmitLogEventWith1Enrich0ForContext0LogContext |   core31 RyuJit |    RyuJit | .NET Core 3.1 |       692.96 ns |    24.821 ns |     37.150 ns |     55.87 |     3.18 |   0.0668 |       - |       - |     424 B |
|          EmitLogEventWith0Enrich1ForContext0LogContext |   core31 RyuJit |    RyuJit | .NET Core 3.1 |       647.46 ns |     2.842 ns |      4.076 ns |     52.25 |     0.40 |   0.0582 |       - |       - |     368 B |
|          EmitLogEventWith0Enrich0ForContext1LogContext |   core31 RyuJit |    RyuJit | .NET Core 3.1 |     1,139.05 ns |     9.165 ns |     13.718 ns |     92.05 |     1.16 |   0.1564 |       - |       - |     984 B |
|          EmitLogEventWith1Enrich1ForContext1LogContext |   core31 RyuJit |    RyuJit | .NET Core 3.1 |     1,334.23 ns |    12.506 ns |     18.331 ns |    107.50 |     1.55 |   0.2041 |       - |       - |    1288 B |
|       EmitLogEventWith10Enrich10ForContext10LogContext |   core31 RyuJit |    RyuJit | .NET Core 3.1 |     6,195.67 ns |    18.686 ns |     27.390 ns |    499.66 |     3.29 |   1.2054 |  0.0076 |       - |    7584 B |
|    EmitLogEventWith100Enrich100ForContext100LogContext |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    53,341.80 ns |   358.135 ns |    536.040 ns |  4,299.79 |    51.27 |  11.8408 |  1.0376 |       - |   74617 B |
| EmitLogEventWith1000Enrich1000ForContext1000LogContext |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 1,098,364.78 ns | 7,174.496 ns | 10,516.283 ns | 88,736.92 |   954.64 | 119.1406 | 52.7344 | 29.2969 |  735963 B |
|                                                        |                 |           |               |                 |              |               |           |          |          |         |         |           |
|                                   EmitLogAIgnoredEvent | net48 LegacyJit | LegacyJit |      .NET 4.8 |        13.69 ns |     0.040 ns |      0.055 ns |      1.00 |     0.00 |        - |       - |       - |         - |
|                                           EmitLogEvent | net48 LegacyJit | LegacyJit |      .NET 4.8 |       608.08 ns |     4.193 ns |      6.013 ns |     44.42 |     0.44 |   0.0591 |       - |       - |     377 B |
|          EmitLogEventWith1Enrich0ForContext0LogContext | net48 LegacyJit | LegacyJit |      .NET 4.8 |       673.79 ns |     2.495 ns |      3.734 ns |     49.22 |     0.32 |   0.0687 |       - |       - |     433 B |
|          EmitLogEventWith0Enrich1ForContext0LogContext | net48 LegacyJit | LegacyJit |      .NET 4.8 |       662.93 ns |     2.936 ns |      4.394 ns |     48.41 |     0.45 |   0.0591 |       - |       - |     377 B |
|          EmitLogEventWith0Enrich0ForContext1LogContext | net48 LegacyJit | LegacyJit |      .NET 4.8 |     1,243.65 ns |     9.120 ns |     13.367 ns |     90.80 |     0.90 |   0.1602 |       - |       - |    1011 B |
|          EmitLogEventWith1Enrich1ForContext1LogContext | net48 LegacyJit | LegacyJit |      .NET 4.8 |     1,450.00 ns |    16.465 ns |     23.614 ns |    105.97 |     2.03 |   0.2079 |       - |       - |    1316 B |
|       EmitLogEventWith10Enrich10ForContext10LogContext | net48 LegacyJit | LegacyJit |      .NET 4.8 |     7,032.11 ns |    20.394 ns |     29.893 ns |    513.86 |     3.32 |   1.1826 |  0.0076 |       - |    7486 B |
|    EmitLogEventWith100Enrich100ForContext100LogContext | net48 LegacyJit | LegacyJit |      .NET 4.8 |    65,065.62 ns |   299.317 ns |    429.272 ns |  4,754.50 |    44.99 |  11.7188 |  0.8545 |       - |   73988 B |
| EmitLogEventWith1000Enrich1000ForContext1000LogContext | net48 LegacyJit | LegacyJit |      .NET 4.8 | 1,184,341.40 ns | 4,988.955 ns |  6,993.817 ns | 86,552.89 |   438.86 | 119.1406 | 56.6406 | 29.2969 |  737000 B |
|                                                        |                 |           |               |                 |              |               |           |          |          |         |         |           |
|                                   EmitLogAIgnoredEvent |    net48 RyuJit |    RyuJit |      .NET 4.8 |        13.33 ns |     0.232 ns |      0.348 ns |      1.00 |     0.00 |        - |       - |       - |         - |
|                                           EmitLogEvent |    net48 RyuJit |    RyuJit |      .NET 4.8 |       601.34 ns |     1.991 ns |      2.979 ns |     45.14 |     1.16 |   0.0591 |       - |       - |     377 B |
|          EmitLogEventWith1Enrich0ForContext0LogContext |    net48 RyuJit |    RyuJit |      .NET 4.8 |       675.91 ns |     3.397 ns |      4.979 ns |     50.76 |     1.06 |   0.0687 |       - |       - |     433 B |
|          EmitLogEventWith0Enrich1ForContext0LogContext |    net48 RyuJit |    RyuJit |      .NET 4.8 |       655.57 ns |     2.542 ns |      3.645 ns |     49.28 |     1.37 |   0.0591 |       - |       - |     377 B |
|          EmitLogEventWith0Enrich0ForContext1LogContext |    net48 RyuJit |    RyuJit |      .NET 4.8 |     1,242.00 ns |     7.332 ns |     10.279 ns |     93.42 |     2.11 |   0.1602 |       - |       - |    1011 B |
|          EmitLogEventWith1Enrich1ForContext1LogContext |    net48 RyuJit |    RyuJit |      .NET 4.8 |     1,444.61 ns |     5.282 ns |      7.742 ns |    108.50 |     3.18 |   0.2079 |       - |       - |    1316 B |
|       EmitLogEventWith10Enrich10ForContext10LogContext |    net48 RyuJit |    RyuJit |      .NET 4.8 |     7,036.00 ns |    30.997 ns |     46.395 ns |    528.13 |    13.42 |   1.1826 |  0.0076 |       - |    7486 B |
|    EmitLogEventWith100Enrich100ForContext100LogContext |    net48 RyuJit |    RyuJit |      .NET 4.8 |    65,236.07 ns |   360.156 ns |    539.064 ns |  4,896.48 |   116.85 |  11.7188 |  0.8545 |       - |   73988 B |
| EmitLogEventWith1000Enrich1000ForContext1000LogContext |    net48 RyuJit |    RyuJit |      .NET 4.8 | 1,186,937.53 ns | 4,943.296 ns |  6,929.810 ns | 89,293.78 | 2,548.91 | 119.1406 | 56.6406 | 29.2969 |  737000 B |
