``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.450 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.401
  [Host]          : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT
  core31 RyuJit   : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT
  net48 LegacyJit : .NET Framework 4.8 (4.8.4200.0), X64 RyuJIT
  net48 RyuJit    : .NET Framework 4.8 (4.8.4200.0), X64 RyuJIT

IterationCount=15  LaunchCount=2  WarmupCount=10  

```
|                                                 Method |             Job |       Jit |       Runtime |          Mean |        Error |       StdDev |     Ratio | RatioSD |   Gen 0 |  Gen 1 | Gen 2 | Allocated |
|------------------------------------------------------- |---------------- |---------- |-------------- |--------------:|-------------:|-------------:|----------:|--------:|--------:|-------:|------:|----------:|
|                                   EmitLogAIgnoredEvent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |      12.17 ns |     0.576 ns |     0.827 ns |      1.00 |    0.00 |       - |      - |     - |         - |
|                                           EmitLogEvent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |     669.64 ns |     9.185 ns |    13.747 ns |     55.35 |    3.87 |  0.0582 |      - |     - |     368 B |
|          EmitLogEventWith1Enrich0ForContext0LogContext |   core31 RyuJit |    RyuJit | .NET Core 3.1 |     722.97 ns |     6.956 ns |    10.196 ns |     59.64 |    4.39 |  0.0668 |      - |     - |     424 B |
|          EmitLogEventWith0Enrich1ForContext0LogContext |   core31 RyuJit |    RyuJit | .NET Core 3.1 |     711.22 ns |    15.914 ns |    23.819 ns |     58.84 |    5.01 |  0.0582 |      - |     - |     368 B |
|          EmitLogEventWith0Enrich0ForContext1LogContext |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   1,238.52 ns |    16.573 ns |    24.293 ns |    102.18 |    6.91 |  0.1564 |      - |     - |     984 B |
|          EmitLogEventWith1Enrich1ForContext1LogContext |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   1,405.81 ns |    23.411 ns |    33.576 ns |    115.91 |    7.14 |  0.1640 |      - |     - |    1040 B |
|       EmitLogEventWith10Enrich10ForContext10LogContext |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   6,709.45 ns |    61.902 ns |    92.652 ns |    553.61 |   35.47 |  1.2054 | 0.0076 |     - |    7584 B |
|    EmitLogEventWith100Enrich100ForContext100LogContext |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  13,730.70 ns |   168.502 ns |   246.989 ns |  1,133.03 |   81.48 |  2.6245 | 0.0458 |     - |   16560 B |
| EmitLogEventWith1000Enrich1000ForContext1000LogContext |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 129,550.54 ns | 2,073.541 ns | 3,103.579 ns | 10,688.30 |  708.10 | 25.1465 | 4.1504 |     - |  158985 B |
|                                                        |                 |           |               |               |              |              |           |         |         |        |       |           |
|                                   EmitLogAIgnoredEvent | net48 LegacyJit | LegacyJit |      .NET 4.8 |      14.70 ns |     0.528 ns |     0.757 ns |      1.00 |    0.00 |       - |      - |     - |         - |
|                                           EmitLogEvent | net48 LegacyJit | LegacyJit |      .NET 4.8 |     650.14 ns |     5.723 ns |     8.389 ns |     44.36 |    2.49 |  0.0591 |      - |     - |     377 B |
|          EmitLogEventWith1Enrich0ForContext0LogContext | net48 LegacyJit | LegacyJit |      .NET 4.8 |     729.96 ns |     7.018 ns |    10.504 ns |     49.76 |    2.43 |  0.0687 |      - |     - |     433 B |
|          EmitLogEventWith0Enrich1ForContext0LogContext | net48 LegacyJit | LegacyJit |      .NET 4.8 |     711.87 ns |     7.346 ns |    10.995 ns |     48.50 |    2.13 |  0.0591 |      - |     - |     377 B |
|          EmitLogEventWith0Enrich0ForContext1LogContext | net48 LegacyJit | LegacyJit |      .NET 4.8 |   1,362.68 ns |     5.087 ns |     7.132 ns |     93.05 |    4.96 |  0.1602 |      - |     - |    1011 B |
|          EmitLogEventWith1Enrich1ForContext1LogContext | net48 LegacyJit | LegacyJit |      .NET 4.8 |   1,525.87 ns |    19.830 ns |    29.067 ns |    104.03 |    4.67 |  0.1678 |      - |     - |    1067 B |
|       EmitLogEventWith10Enrich10ForContext10LogContext | net48 LegacyJit | LegacyJit |      .NET 4.8 |   7,497.90 ns |    58.427 ns |    87.450 ns |    511.91 |   28.87 |  1.1826 | 0.0076 |     - |    7486 B |
|    EmitLogEventWith100Enrich100ForContext100LogContext | net48 LegacyJit | LegacyJit |      .NET 4.8 |  15,154.12 ns |   136.195 ns |   203.850 ns |  1,034.90 |   60.94 |  2.6398 | 0.0458 |     - |   16633 B |
| EmitLogEventWith1000Enrich1000ForContext1000LogContext | net48 LegacyJit | LegacyJit |      .NET 4.8 | 136,216.81 ns | 1,198.629 ns | 1,794.051 ns |  9,295.72 |  507.26 | 25.1465 | 4.1504 |     - |  159367 B |
|                                                        |                 |           |               |               |              |              |           |         |         |        |       |           |
|                                   EmitLogAIgnoredEvent |    net48 RyuJit |    RyuJit |      .NET 4.8 |      13.79 ns |     0.157 ns |     0.235 ns |      1.00 |    0.00 |       - |      - |     - |         - |
|                                           EmitLogEvent |    net48 RyuJit |    RyuJit |      .NET 4.8 |     655.77 ns |     5.880 ns |     8.618 ns |     47.55 |    1.02 |  0.0591 |      - |     - |     377 B |
|          EmitLogEventWith1Enrich0ForContext0LogContext |    net48 RyuJit |    RyuJit |      .NET 4.8 |     731.08 ns |     6.314 ns |     9.451 ns |     53.02 |    1.13 |  0.0687 |      - |     - |     433 B |
|          EmitLogEventWith0Enrich1ForContext0LogContext |    net48 RyuJit |    RyuJit |      .NET 4.8 |     731.15 ns |    12.243 ns |    17.558 ns |     53.04 |    1.62 |  0.0591 |      - |     - |     377 B |
|          EmitLogEventWith0Enrich0ForContext1LogContext |    net48 RyuJit |    RyuJit |      .NET 4.8 |   1,419.15 ns |    28.354 ns |    38.811 ns |    102.91 |    3.54 |  0.1602 |      - |     - |    1011 B |
|          EmitLogEventWith1Enrich1ForContext1LogContext |    net48 RyuJit |    RyuJit |      .NET 4.8 |   1,557.18 ns |    59.914 ns |    83.991 ns |    112.94 |    6.63 |  0.1678 |      - |     - |    1067 B |
|       EmitLogEventWith10Enrich10ForContext10LogContext |    net48 RyuJit |    RyuJit |      .NET 4.8 |   7,793.07 ns |   187.618 ns |   269.076 ns |    565.39 |   23.94 |  1.1749 |      - |     - |    7486 B |
|    EmitLogEventWith100Enrich100ForContext100LogContext |    net48 RyuJit |    RyuJit |      .NET 4.8 |  15,410.23 ns |   457.138 ns |   640.844 ns |  1,117.56 |   51.45 |  2.6245 | 0.0305 |     - |   16633 B |
| EmitLogEventWith1000Enrich1000ForContext1000LogContext |    net48 RyuJit |    RyuJit |      .NET 4.8 | 141,247.90 ns | 2,599.962 ns | 3,558.857 ns | 10,243.17 |  344.91 | 25.1465 | 4.1504 |     - |  159367 B |
