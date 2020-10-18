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
|                                                 Method |             Job |       Jit |       Runtime |            Mean |         Error |        StdDev |          Median |     Ratio |  RatioSD |    Gen 0 |   Gen 1 |   Gen 2 | Allocated |
|------------------------------------------------------- |---------------- |---------- |-------------- |----------------:|--------------:|--------------:|----------------:|----------:|---------:|---------:|--------:|--------:|----------:|
|                                   EmitLogAIgnoredEvent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |        12.37 ns |      1.119 ns |      1.641 ns |        12.91 ns |      1.00 |     0.00 |        - |       - |       - |         - |
|                                           EmitLogEvent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |       631.28 ns |      3.257 ns |      4.875 ns |       631.08 ns |     51.87 |     6.66 |   0.0582 |       - |       - |     368 B |
|          EmitLogEventWith1Enrich0ForContext0LogContext |   core31 RyuJit |    RyuJit | .NET Core 3.1 |       697.33 ns |      5.055 ns |      7.409 ns |       695.96 ns |     57.41 |     8.11 |   0.0668 |       - |       - |     424 B |
|          EmitLogEventWith0Enrich1ForContext0LogContext |   core31 RyuJit |    RyuJit | .NET Core 3.1 |       661.90 ns |      3.201 ns |      4.692 ns |       661.82 ns |     54.40 |     6.97 |   0.0582 |       - |       - |     368 B |
|          EmitLogEventWith0Enrich0ForContext1LogContext |   core31 RyuJit |    RyuJit | .NET Core 3.1 |     1,150.07 ns |      4.871 ns |      7.140 ns |     1,150.44 ns |     94.60 |    12.76 |   0.1564 |       - |       - |     984 B |
|          EmitLogEventWith1Enrich1ForContext1LogContext |   core31 RyuJit |    RyuJit | .NET Core 3.1 |     1,335.44 ns |     22.002 ns |     32.250 ns |     1,319.17 ns |    110.13 |    17.07 |   0.2041 |       - |       - |    1288 B |
|       EmitLogEventWith10Enrich10ForContext10LogContext |   core31 RyuJit |    RyuJit | .NET Core 3.1 |     6,119.51 ns |     49.961 ns |     70.038 ns |     6,108.19 ns |    508.32 |    67.98 |   1.2054 |  0.0076 |       - |    7584 B |
|    EmitLogEventWith100Enrich100ForContext100LogContext |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    52,362.63 ns |    581.944 ns |    815.805 ns |    52,060.06 ns |  4,355.66 |   628.44 |  11.8408 |  1.0376 |       - |   74617 B |
| EmitLogEventWith1000Enrich1000ForContext1000LogContext |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 1,035,099.10 ns | 35,171.274 ns | 51,553.597 ns | 1,062,456.05 ns | 84,581.97 | 7,118.40 | 119.1406 | 54.6875 | 29.2969 |  735963 B |
|                                                        |                 |           |               |                 |               |               |                 |           |          |          |         |         |           |
|                                   EmitLogAIgnoredEvent | net48 LegacyJit | LegacyJit |      .NET 4.8 |        13.84 ns |      0.166 ns |      0.249 ns |        13.86 ns |      1.00 |     0.00 |        - |       - |       - |         - |
|                                           EmitLogEvent | net48 LegacyJit | LegacyJit |      .NET 4.8 |       602.85 ns |      3.116 ns |      4.469 ns |       602.59 ns |     43.62 |     0.96 |   0.0591 |       - |       - |     377 B |
|          EmitLogEventWith1Enrich0ForContext0LogContext | net48 LegacyJit | LegacyJit |      .NET 4.8 |       675.83 ns |      3.212 ns |      4.607 ns |       676.64 ns |     48.90 |     1.03 |   0.0687 |       - |       - |     433 B |
|          EmitLogEventWith0Enrich1ForContext0LogContext | net48 LegacyJit | LegacyJit |      .NET 4.8 |       653.24 ns |      4.244 ns |      6.352 ns |       652.56 ns |     47.20 |     1.13 |   0.0591 |       - |       - |     377 B |
|          EmitLogEventWith0Enrich0ForContext1LogContext | net48 LegacyJit | LegacyJit |      .NET 4.8 |     1,248.06 ns |      7.325 ns |     10.736 ns |     1,248.03 ns |     90.21 |     1.19 |   0.1602 |       - |       - |    1011 B |
|          EmitLogEventWith1Enrich1ForContext1LogContext | net48 LegacyJit | LegacyJit |      .NET 4.8 |     1,421.55 ns |      3.690 ns |      5.408 ns |     1,421.43 ns |    102.77 |     1.94 |   0.2079 |       - |       - |    1316 B |
|       EmitLogEventWith10Enrich10ForContext10LogContext | net48 LegacyJit | LegacyJit |      .NET 4.8 |     6,979.75 ns |     24.697 ns |     35.419 ns |     6,975.19 ns |    505.02 |    10.19 |   1.1826 |  0.0076 |       - |    7486 B |
|    EmitLogEventWith100Enrich100ForContext100LogContext | net48 LegacyJit | LegacyJit |      .NET 4.8 |    65,496.72 ns |    323.241 ns |    483.811 ns |    65,376.23 ns |  4,732.21 |    77.55 |  11.7188 |  0.8545 |       - |   73988 B |
| EmitLogEventWith1000Enrich1000ForContext1000LogContext | net48 LegacyJit | LegacyJit |      .NET 4.8 | 1,184,592.27 ns |  5,338.178 ns |  7,824.632 ns | 1,185,210.16 ns | 85,637.36 | 1,714.01 | 119.1406 | 56.6406 | 29.2969 |  736993 B |
|                                                        |                 |           |               |                 |               |               |                 |           |          |          |         |         |           |
|                                   EmitLogAIgnoredEvent |    net48 RyuJit |    RyuJit |      .NET 4.8 |        13.60 ns |      0.035 ns |      0.052 ns |        13.60 ns |      1.00 |     0.00 |        - |       - |       - |         - |
|                                           EmitLogEvent |    net48 RyuJit |    RyuJit |      .NET 4.8 |       601.96 ns |      4.308 ns |      6.039 ns |       601.76 ns |     44.24 |     0.44 |   0.0591 |       - |       - |     377 B |
|          EmitLogEventWith1Enrich0ForContext0LogContext |    net48 RyuJit |    RyuJit |      .NET 4.8 |       668.35 ns |      3.497 ns |      5.235 ns |       668.75 ns |     49.11 |     0.42 |   0.0687 |       - |       - |     433 B |
|          EmitLogEventWith0Enrich1ForContext0LogContext |    net48 RyuJit |    RyuJit |      .NET 4.8 |       648.05 ns |      2.379 ns |      3.412 ns |       648.25 ns |     47.64 |     0.33 |   0.0591 |       - |       - |     377 B |
|          EmitLogEventWith0Enrich0ForContext1LogContext |    net48 RyuJit |    RyuJit |      .NET 4.8 |     1,241.04 ns |      9.142 ns |     13.684 ns |     1,241.25 ns |     91.26 |     1.01 |   0.1602 |       - |       - |    1011 B |
|          EmitLogEventWith1Enrich1ForContext1LogContext |    net48 RyuJit |    RyuJit |      .NET 4.8 |     1,428.80 ns |      4.783 ns |      7.011 ns |     1,430.27 ns |    105.02 |     0.64 |   0.2079 |       - |       - |    1316 B |
|       EmitLogEventWith10Enrich10ForContext10LogContext |    net48 RyuJit |    RyuJit |      .NET 4.8 |     7,001.73 ns |     24.009 ns |     34.433 ns |     7,006.44 ns |    514.70 |     2.94 |   1.1826 |  0.0076 |       - |    7486 B |
|    EmitLogEventWith100Enrich100ForContext100LogContext |    net48 RyuJit |    RyuJit |      .NET 4.8 |    65,473.62 ns |    321.923 ns |    471.870 ns |    65,475.94 ns |  4,812.63 |    40.24 |  11.7188 |  0.8545 |       - |   73988 B |
| EmitLogEventWith1000Enrich1000ForContext1000LogContext |    net48 RyuJit |    RyuJit |      .NET 4.8 | 1,187,066.73 ns |  4,615.568 ns |  6,765.439 ns | 1,185,073.05 ns | 87,255.42 |   650.47 | 119.1406 | 56.6406 | 29.2969 |  736993 B |
