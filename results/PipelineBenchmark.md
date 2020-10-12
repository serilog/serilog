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
|                                                 Method |             Job |       Jit |       Runtime |          Mean |        Error |        StdDev |        Median |     Ratio |  RatioSD |   Gen 0 |   Gen 1 | Gen 2 | Allocated |
|------------------------------------------------------- |---------------- |---------- |-------------- |--------------:|-------------:|--------------:|--------------:|----------:|---------:|--------:|--------:|------:|----------:|
|                                   EmitLogAIgnoredEvent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |      12.50 ns |     0.937 ns |      1.403 ns |      11.61 ns |      1.00 |     0.00 |       - |       - |     - |         - |
|                                           EmitLogEvent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |     692.31 ns |     9.773 ns |     14.628 ns |     688.80 ns |     55.95 |     5.35 |  0.0582 |       - |     - |     368 B |
|          EmitLogEventWith1Enrich0ForContext0LogContext |   core31 RyuJit |    RyuJit | .NET Core 3.1 |     768.05 ns |    10.928 ns |     16.357 ns |     765.75 ns |     62.21 |     7.24 |  0.0620 |       - |     - |     392 B |
|          EmitLogEventWith0Enrich1ForContext0LogContext |   core31 RyuJit |    RyuJit | .NET Core 3.1 |     731.93 ns |     8.173 ns |     12.233 ns |     733.01 ns |     59.24 |     6.50 |  0.0582 |       - |     - |     368 B |
|          EmitLogEventWith0Enrich0ForContext1LogContext |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   1,317.67 ns |    48.599 ns |     69.699 ns |   1,299.93 ns |    105.96 |    12.83 |  0.1507 |       - |     - |     952 B |
|          EmitLogEventWith1Enrich1ForContext1LogContext |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   1,413.63 ns |    17.172 ns |     25.702 ns |   1,414.71 ns |    114.34 |    11.85 |  0.1717 |       - |     - |    1088 B |
|       EmitLogEventWith10Enrich10ForContext10LogContext |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   6,529.51 ns |    63.446 ns |     94.964 ns |   6,522.25 ns |    528.51 |    58.30 |  0.9613 |  0.0076 |     - |    6032 B |
|    EmitLogEventWith100Enrich100ForContext100LogContext |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  54,786.86 ns |   547.791 ns |    819.908 ns |  54,831.40 ns |  4,434.78 |   490.79 |  8.9111 |  0.6714 |     - |   55920 B |
| EmitLogEventWith1000Enrich1000ForContext1000LogContext |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 571,980.07 ns | 8,014.973 ns | 11,996.434 ns | 569,591.21 ns | 46,273.51 | 4,928.22 | 86.9141 | 34.1797 |     - |  550828 B |
|                                                        |                 |           |               |               |              |               |               |           |          |         |         |       |           |
|                                   EmitLogAIgnoredEvent | net48 LegacyJit | LegacyJit |      .NET 4.8 |      14.52 ns |     0.199 ns |      0.297 ns |      14.42 ns |      1.00 |     0.00 |       - |       - |     - |         - |
|                                           EmitLogEvent | net48 LegacyJit | LegacyJit |      .NET 4.8 |     670.78 ns |     7.756 ns |     11.609 ns |     671.52 ns |     46.22 |     1.29 |  0.0591 |       - |     - |     377 B |
|          EmitLogEventWith1Enrich0ForContext0LogContext | net48 LegacyJit | LegacyJit |      .NET 4.8 |     757.87 ns |     7.155 ns |     10.710 ns |     761.21 ns |     52.22 |     1.21 |  0.0629 |       - |     - |     401 B |
|          EmitLogEventWith0Enrich1ForContext0LogContext | net48 LegacyJit | LegacyJit |      .NET 4.8 |     751.57 ns |    33.643 ns |     50.355 ns |     736.09 ns |     51.80 |     3.79 |  0.0591 |       - |     - |     377 B |
|          EmitLogEventWith0Enrich0ForContext1LogContext | net48 LegacyJit | LegacyJit |      .NET 4.8 |   1,375.71 ns |    15.027 ns |     22.491 ns |   1,372.34 ns |     94.79 |     2.02 |  0.1545 |       - |     - |     979 B |
|          EmitLogEventWith1Enrich1ForContext1LogContext | net48 LegacyJit | LegacyJit |      .NET 4.8 |   1,532.89 ns |    15.117 ns |     22.626 ns |   1,532.92 ns |    105.63 |     2.65 |  0.1755 |       - |     - |    1115 B |
|       EmitLogEventWith10Enrich10ForContext10LogContext | net48 LegacyJit | LegacyJit |      .NET 4.8 |   7,350.85 ns |    51.751 ns |     75.856 ns |   7,352.24 ns |    506.23 |    11.33 |  0.9384 |       - |     - |    5929 B |
|    EmitLogEventWith100Enrich100ForContext100LogContext | net48 LegacyJit | LegacyJit |      .NET 4.8 |  66,484.53 ns |   856.180 ns |  1,281.490 ns |  66,669.79 ns |  4,582.05 |   153.13 |  8.6670 |  0.8545 |     - |   55241 B |
| EmitLogEventWith1000Enrich1000ForContext1000LogContext | net48 LegacyJit | LegacyJit |      .NET 4.8 | 720,906.09 ns | 7,123.124 ns | 10,661.557 ns | 721,709.33 ns | 49,673.21 | 1,121.84 | 86.9141 | 27.3438 |     - |  551390 B |
|                                                        |                 |           |               |               |              |               |               |           |          |         |         |       |           |
|                                   EmitLogAIgnoredEvent |    net48 RyuJit |    RyuJit |      .NET 4.8 |      14.73 ns |     0.220 ns |      0.329 ns |      14.78 ns |      1.00 |     0.00 |       - |       - |     - |         - |
|                                           EmitLogEvent |    net48 RyuJit |    RyuJit |      .NET 4.8 |     677.13 ns |     7.373 ns |     10.808 ns |     678.21 ns |     46.02 |     1.32 |  0.0591 |       - |     - |     377 B |
|          EmitLogEventWith1Enrich0ForContext0LogContext |    net48 RyuJit |    RyuJit |      .NET 4.8 |     758.65 ns |     7.795 ns |     11.667 ns |     762.02 ns |     51.54 |     1.30 |  0.0629 |       - |     - |     401 B |
|          EmitLogEventWith0Enrich1ForContext0LogContext |    net48 RyuJit |    RyuJit |      .NET 4.8 |     737.43 ns |    14.293 ns |     20.036 ns |     734.58 ns |     50.17 |     1.90 |  0.0591 |       - |     - |     377 B |
|          EmitLogEventWith0Enrich0ForContext1LogContext |    net48 RyuJit |    RyuJit |      .NET 4.8 |   1,368.03 ns |    13.841 ns |     20.717 ns |   1,366.18 ns |     92.95 |     2.76 |  0.1545 |       - |     - |     979 B |
|          EmitLogEventWith1Enrich1ForContext1LogContext |    net48 RyuJit |    RyuJit |      .NET 4.8 |   1,538.09 ns |    16.519 ns |     24.724 ns |   1,543.63 ns |    104.50 |     2.91 |  0.1755 |       - |     - |    1115 B |
|       EmitLogEventWith10Enrich10ForContext10LogContext |    net48 RyuJit |    RyuJit |      .NET 4.8 |   7,360.72 ns |    62.108 ns |     92.960 ns |   7,380.06 ns |    500.08 |    12.32 |  0.9384 |       - |     - |    5929 B |
|    EmitLogEventWith100Enrich100ForContext100LogContext |    net48 RyuJit |    RyuJit |      .NET 4.8 |  66,610.61 ns |   882.507 ns |  1,320.894 ns |  66,519.40 ns |  4,524.84 |   108.11 |  8.6670 |  0.8545 |     - |   55241 B |
| EmitLogEventWith1000Enrich1000ForContext1000LogContext |    net48 RyuJit |    RyuJit |      .NET 4.8 | 719,929.71 ns | 9,384.604 ns | 13,459.119 ns | 720,597.71 ns | 48,964.14 | 1,332.27 | 86.9141 | 27.3438 |     - |  551390 B |
