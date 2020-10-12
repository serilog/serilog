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
|                                                 Method |             Job |       Jit |       Runtime |            Mean |         Error |         StdDev |          Median |      Ratio |   RatioSD |    Gen 0 |   Gen 1 |   Gen 2 | Allocated |
|------------------------------------------------------- |---------------- |---------- |-------------- |----------------:|--------------:|---------------:|----------------:|-----------:|----------:|---------:|--------:|--------:|----------:|
|                                   EmitLogAIgnoredEvent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |        11.09 ns |      0.112 ns |       0.168 ns |        11.13 ns |       1.00 |      0.00 |        - |       - |       - |         - |
|                                           EmitLogEvent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |       622.25 ns |     10.375 ns |      15.529 ns |       625.30 ns |      56.11 |      1.64 |   0.0582 |       - |       - |     368 B |
|          EmitLogEventWith1Enrich0ForContext0LogContext |   core31 RyuJit |    RyuJit | .NET Core 3.1 |       713.77 ns |      8.321 ns |      12.454 ns |       712.86 ns |      64.36 |      1.38 |   0.0668 |       - |       - |     424 B |
|          EmitLogEventWith0Enrich1ForContext0LogContext |   core31 RyuJit |    RyuJit | .NET Core 3.1 |       685.08 ns |      9.911 ns |      14.528 ns |       688.32 ns |      61.73 |      1.62 |   0.0582 |       - |       - |     368 B |
|          EmitLogEventWith0Enrich0ForContext1LogContext |   core31 RyuJit |    RyuJit | .NET Core 3.1 |     1,201.32 ns |     14.502 ns |      21.257 ns |     1,206.83 ns |     108.25 |      2.50 |   0.1564 |       - |       - |     984 B |
|          EmitLogEventWith1Enrich1ForContext1LogContext |   core31 RyuJit |    RyuJit | .NET Core 3.1 |     1,384.45 ns |     20.503 ns |      30.688 ns |     1,384.36 ns |     124.84 |      3.51 |   0.2041 |       - |       - |    1288 B |
|       EmitLogEventWith10Enrich10ForContext10LogContext |   core31 RyuJit |    RyuJit | .NET Core 3.1 |     6,303.37 ns |     64.607 ns |      96.700 ns |     6,301.41 ns |     568.34 |      9.54 |   1.2054 |  0.0076 |       - |    7584 B |
|    EmitLogEventWith100Enrich100ForContext100LogContext |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    54,483.13 ns |    605.235 ns |     905.887 ns |    54,684.74 ns |   4,912.97 |    114.11 |  11.8408 |  1.0376 |       - |   74616 B |
| EmitLogEventWith1000Enrich1000ForContext1000LogContext |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 1,209,761.13 ns | 80,300.172 ns | 120,189.517 ns | 1,158,237.11 ns | 109,064.70 | 10,695.49 | 119.1406 | 54.6875 | 29.2969 |  735957 B |
|                                                        |                 |           |               |                 |               |                |                 |            |           |          |         |         |           |
|                                   EmitLogAIgnoredEvent | net48 LegacyJit | LegacyJit |      .NET 4.8 |        14.24 ns |      0.224 ns |       0.328 ns |        14.15 ns |       1.00 |      0.00 |        - |       - |       - |         - |
|                                           EmitLogEvent | net48 LegacyJit | LegacyJit |      .NET 4.8 |       622.91 ns |      7.906 ns |      11.833 ns |       622.80 ns |      43.83 |      1.22 |   0.0591 |       - |       - |     377 B |
|          EmitLogEventWith1Enrich0ForContext0LogContext | net48 LegacyJit | LegacyJit |      .NET 4.8 |       698.99 ns |      9.240 ns |      13.830 ns |       696.93 ns |      49.17 |      1.42 |   0.0687 |       - |       - |     433 B |
|          EmitLogEventWith0Enrich1ForContext0LogContext | net48 LegacyJit | LegacyJit |      .NET 4.8 |       678.64 ns |      8.465 ns |      12.670 ns |       680.24 ns |      47.66 |      1.60 |   0.0591 |       - |       - |     377 B |
|          EmitLogEventWith0Enrich0ForContext1LogContext | net48 LegacyJit | LegacyJit |      .NET 4.8 |     1,302.12 ns |     24.565 ns |      35.230 ns |     1,303.93 ns |      91.61 |      3.27 |   0.1602 |       - |       - |    1011 B |
|          EmitLogEventWith1Enrich1ForContext1LogContext | net48 LegacyJit | LegacyJit |      .NET 4.8 |     1,482.45 ns |     17.618 ns |      25.824 ns |     1,479.80 ns |     104.19 |      3.63 |   0.2079 |       - |       - |    1316 B |
|       EmitLogEventWith10Enrich10ForContext10LogContext | net48 LegacyJit | LegacyJit |      .NET 4.8 |     7,745.61 ns |    400.435 ns |     599.353 ns |     7,356.67 ns |     545.30 |     44.02 |   1.1826 |  0.0076 |       - |    7486 B |
|    EmitLogEventWith100Enrich100ForContext100LogContext | net48 LegacyJit | LegacyJit |      .NET 4.8 |    67,426.43 ns |    965.259 ns |   1,444.754 ns |    67,226.42 ns |   4,739.69 |    143.96 |  11.7188 |  0.8545 |       - |   73988 B |
| EmitLogEventWith1000Enrich1000ForContext1000LogContext | net48 LegacyJit | LegacyJit |      .NET 4.8 | 1,237,575.73 ns | 22,294.108 ns |  33,368.770 ns | 1,233,341.02 ns |  86,879.66 |  3,195.22 | 119.1406 | 56.6406 | 29.2969 |  737000 B |
|                                                        |                 |           |               |                 |               |                |                 |            |           |          |         |         |           |
|                                   EmitLogAIgnoredEvent |    net48 RyuJit |    RyuJit |      .NET 4.8 |        13.91 ns |      0.340 ns |       0.509 ns |        13.93 ns |       1.00 |      0.00 |        - |       - |       - |         - |
|                                           EmitLogEvent |    net48 RyuJit |    RyuJit |      .NET 4.8 |       628.45 ns |      7.779 ns |      11.644 ns |       628.47 ns |      45.24 |      1.91 |   0.0591 |       - |       - |     377 B |
|          EmitLogEventWith1Enrich0ForContext0LogContext |    net48 RyuJit |    RyuJit |      .NET 4.8 |       692.19 ns |      8.145 ns |      12.191 ns |       693.27 ns |      49.82 |      1.83 |   0.0687 |       - |       - |     433 B |
|          EmitLogEventWith0Enrich1ForContext0LogContext |    net48 RyuJit |    RyuJit |      .NET 4.8 |       683.93 ns |      9.331 ns |      13.967 ns |       687.11 ns |      49.24 |      2.12 |   0.0591 |       - |       - |     377 B |
|          EmitLogEventWith0Enrich0ForContext1LogContext |    net48 RyuJit |    RyuJit |      .NET 4.8 |     1,289.77 ns |     17.925 ns |      26.830 ns |     1,287.65 ns |      92.85 |      3.98 |   0.1602 |       - |       - |    1011 B |
|          EmitLogEventWith1Enrich1ForContext1LogContext |    net48 RyuJit |    RyuJit |      .NET 4.8 |     1,479.38 ns |     19.057 ns |      27.933 ns |     1,491.40 ns |     106.55 |      4.71 |   0.2079 |       - |       - |    1316 B |
|       EmitLogEventWith10Enrich10ForContext10LogContext |    net48 RyuJit |    RyuJit |      .NET 4.8 |     7,387.33 ns |    113.899 ns |     170.479 ns |     7,353.18 ns |     531.69 |     20.15 |   1.1749 |       - |       - |    7486 B |
|    EmitLogEventWith100Enrich100ForContext100LogContext |    net48 RyuJit |    RyuJit |      .NET 4.8 |    67,799.38 ns |  1,062.920 ns |   1,590.929 ns |    67,819.35 ns |   4,881.96 |    237.06 |  11.7188 |  0.8545 |       - |   73988 B |
| EmitLogEventWith1000Enrich1000ForContext1000LogContext |    net48 RyuJit |    RyuJit |      .NET 4.8 | 1,241,711.20 ns | 17,933.562 ns |  26,842.111 ns | 1,243,464.99 ns |  89,394.83 |  3,955.62 | 119.1406 | 56.6406 | 29.2969 |  737000 B |
