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
|                                                 Method |             Job |       Jit |       Runtime |          Mean |        Error |        StdDev |        Median |     Ratio |  RatioSD |   Gen 0 |   Gen 1 | Gen 2 | Allocated |
|------------------------------------------------------- |---------------- |---------- |-------------- |--------------:|-------------:|--------------:|--------------:|----------:|---------:|--------:|--------:|------:|----------:|
|                                   EmitLogAIgnoredEvent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |      13.05 ns |     0.959 ns |      1.345 ns |      14.11 ns |      1.00 |     0.00 |       - |       - |     - |         - |
|                                           EmitLogEvent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |     670.64 ns |     8.838 ns |     13.228 ns |     669.03 ns |     52.04 |     5.31 |  0.0582 |       - |     - |     368 B |
|          EmitLogEventWith1Enrich0ForContext0LogContext |   core31 RyuJit |    RyuJit | .NET Core 3.1 |     735.53 ns |     4.607 ns |      6.896 ns |     735.01 ns |     57.00 |     6.16 |  0.0801 |       - |     - |     504 B |
|          EmitLogEventWith0Enrich1ForContext0LogContext |   core31 RyuJit |    RyuJit | .NET Core 3.1 |     723.96 ns |    11.774 ns |     17.623 ns |     720.16 ns |     56.18 |     6.72 |  0.0763 |       - |     - |     480 B |
|          EmitLogEventWith0Enrich0ForContext1LogContext |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   1,262.26 ns |     8.210 ns |     11.774 ns |   1,262.84 ns |     97.71 |    10.39 |  0.1678 |       - |     - |    1064 B |
|          EmitLogEventWith1Enrich1ForContext1LogContext |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   1,363.80 ns |     9.247 ns |     13.841 ns |   1,363.88 ns |    105.57 |    11.23 |  0.1717 |       - |     - |    1088 B |
|       EmitLogEventWith10Enrich10ForContext10LogContext |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   6,401.11 ns |   136.807 ns |    204.766 ns |   6,324.36 ns |    497.50 |    63.05 |  0.9232 |  0.0076 |     - |    5808 B |
|    EmitLogEventWith100Enrich100ForContext100LogContext |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  54,759.03 ns |   753.653 ns |  1,128.032 ns |  54,616.30 ns |  4,249.74 |   448.49 |  8.3618 |  0.7324 |     - |   52728 B |
| EmitLogEventWith1000Enrich1000ForContext1000LogContext |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 546,012.72 ns | 6,174.566 ns |  9,241.800 ns | 546,090.48 ns | 42,166.20 | 4,037.23 | 83.0078 | 27.3438 |     - |  521769 B |
|                                                        |                 |           |               |               |              |               |               |           |          |         |         |       |           |
|                                   EmitLogAIgnoredEvent | net48 LegacyJit | LegacyJit |      .NET 4.8 |      14.20 ns |     0.294 ns |      0.440 ns |      14.08 ns |      1.00 |     0.00 |       - |       - |     - |         - |
|                                           EmitLogEvent | net48 LegacyJit | LegacyJit |      .NET 4.8 |     668.23 ns |     4.118 ns |      6.163 ns |     669.13 ns |     47.09 |     1.58 |  0.0591 |       - |     - |     377 B |
|          EmitLogEventWith1Enrich0ForContext0LogContext | net48 LegacyJit | LegacyJit |      .NET 4.8 |     769.12 ns |    13.518 ns |     19.387 ns |     762.20 ns |     54.07 |     2.32 |  0.0811 |       - |     - |     514 B |
|          EmitLogEventWith0Enrich1ForContext0LogContext | net48 LegacyJit | LegacyJit |      .NET 4.8 |     721.83 ns |     4.645 ns |      6.952 ns |     723.30 ns |     50.87 |     1.66 |  0.0772 |       - |     - |     489 B |
|          EmitLogEventWith0Enrich0ForContext1LogContext | net48 LegacyJit | LegacyJit |      .NET 4.8 |   1,411.37 ns |    10.539 ns |     15.774 ns |   1,412.95 ns |     99.45 |     2.98 |  0.1717 |       - |     - |    1091 B |
|          EmitLogEventWith1Enrich1ForContext1LogContext | net48 LegacyJit | LegacyJit |      .NET 4.8 |   1,557.38 ns |    27.442 ns |     41.074 ns |   1,546.81 ns |    109.77 |     5.09 |  0.1755 |       - |     - |    1115 B |
|       EmitLogEventWith10Enrich10ForContext10LogContext | net48 LegacyJit | LegacyJit |      .NET 4.8 |   7,168.87 ns |    33.861 ns |     50.682 ns |   7,179.22 ns |    505.16 |    15.63 |  0.9003 |       - |     - |    5705 B |
|    EmitLogEventWith100Enrich100ForContext100LogContext | net48 LegacyJit | LegacyJit |      .NET 4.8 |  63,861.69 ns |   545.340 ns |    816.239 ns |  63,906.55 ns |  4,499.45 |   127.13 |  8.1787 |  0.6104 |     - |   52042 B |
| EmitLogEventWith1000Enrich1000ForContext1000LogContext | net48 LegacyJit | LegacyJit |      .NET 4.8 | 659,609.45 ns | 5,980.986 ns |  8,952.058 ns | 660,877.00 ns | 46,475.05 | 1,390.99 | 82.0313 | 24.4141 |     - |  522323 B |
|                                                        |                 |           |               |               |              |               |               |           |          |         |         |       |           |
|                                   EmitLogAIgnoredEvent |    net48 RyuJit |    RyuJit |      .NET 4.8 |      13.88 ns |     0.153 ns |      0.220 ns |      13.83 ns |      1.00 |     0.00 |       - |       - |     - |         - |
|                                           EmitLogEvent |    net48 RyuJit |    RyuJit |      .NET 4.8 |     671.79 ns |     5.016 ns |      7.507 ns |     672.46 ns |     48.41 |     0.92 |  0.0591 |       - |     - |     377 B |
|          EmitLogEventWith1Enrich0ForContext0LogContext |    net48 RyuJit |    RyuJit |      .NET 4.8 |     758.97 ns |     6.391 ns |      9.566 ns |     759.72 ns |     54.73 |     1.15 |  0.0811 |       - |     - |     514 B |
|          EmitLogEventWith0Enrich1ForContext0LogContext |    net48 RyuJit |    RyuJit |      .NET 4.8 |     724.75 ns |     4.344 ns |      6.502 ns |     725.18 ns |     52.21 |     1.08 |  0.0772 |       - |     - |     489 B |
|          EmitLogEventWith0Enrich0ForContext1LogContext |    net48 RyuJit |    RyuJit |      .NET 4.8 |   1,416.00 ns |     8.972 ns |     13.428 ns |   1,417.89 ns |    102.04 |     1.72 |  0.1717 |       - |     - |    1091 B |
|          EmitLogEventWith1Enrich1ForContext1LogContext |    net48 RyuJit |    RyuJit |      .NET 4.8 |   1,543.81 ns |     9.447 ns |     13.244 ns |   1,542.46 ns |    111.20 |     2.27 |  0.1755 |       - |     - |    1115 B |
|       EmitLogEventWith10Enrich10ForContext10LogContext |    net48 RyuJit |    RyuJit |      .NET 4.8 |   7,293.55 ns |   143.054 ns |    214.117 ns |   7,208.36 ns |    525.86 |    16.04 |  0.9003 |       - |     - |    5705 B |
|    EmitLogEventWith100Enrich100ForContext100LogContext |    net48 RyuJit |    RyuJit |      .NET 4.8 |  63,878.73 ns |   509.126 ns |    762.037 ns |  63,627.31 ns |  4,603.98 |    97.73 |  8.1787 |  0.6104 |     - |   52042 B |
| EmitLogEventWith1000Enrich1000ForContext1000LogContext |    net48 RyuJit |    RyuJit |      .NET 4.8 | 663,294.92 ns | 7,307.002 ns | 10,243.396 ns | 662,624.12 ns | 47,770.82 |   844.60 | 82.0313 | 24.4141 |     - |  522323 B |
