``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=5.0.101
  [Host] : .NET Core 5.0.1 (CoreCLR 5.0.120.57516, CoreFX 5.0.120.57516), X64 RyuJIT
  core31 : .NET Core 3.1.10 (CoreCLR 4.700.20.51601, CoreFX 4.700.20.51901), X64 RyuJIT
  net48  : .NET Framework 4.8 (4.8.4300.0), X64 RyuJIT
  net50  : .NET Core 5.0.1 (CoreCLR 5.0.120.57516, CoreFX 5.0.120.57516), X64 RyuJIT

Jit=RyuJit  IterationCount=15  LaunchCount=2  
WarmupCount=10  

```
|                                                 Method |    Job |       Runtime |             Mean |          Error |         StdDev |      Ratio |   RatioSD |    Gen 0 |   Gen 1 |   Gen 2 | Allocated |
|------------------------------------------------------- |------- |-------------- |-----------------:|---------------:|---------------:|-----------:|----------:|---------:|--------:|--------:|----------:|
|                                   EmitLogAIgnoredEvent | core31 | .NET Core 3.1 |        13.055 ns |      1.1191 ns |      1.6751 ns |       1.00 |      0.00 |        - |       - |       - |         - |
|                                           EmitLogEvent | core31 | .NET Core 3.1 |       614.847 ns |      6.0196 ns |      9.0099 ns |      47.83 |      6.00 |   0.0582 |       - |       - |     368 B |
|          EmitLogEventWith1Enrich0ForContext0LogContext | core31 | .NET Core 3.1 |       674.221 ns |      6.4629 ns |      9.6734 ns |      52.54 |      7.28 |   0.0668 |       - |       - |     424 B |
|          EmitLogEventWith0Enrich1ForContext0LogContext | core31 | .NET Core 3.1 |       649.193 ns |      5.6020 ns |      8.3849 ns |      50.53 |      6.50 |   0.0582 |       - |       - |     368 B |
|          EmitLogEventWith0Enrich0ForContext1LogContext | core31 | .NET Core 3.1 |     1,173.589 ns |     13.0232 ns |     19.4925 ns |      91.45 |     12.64 |   0.1564 |       - |       - |     984 B |
|          EmitLogEventWith1Enrich1ForContext1LogContext | core31 | .NET Core 3.1 |     1,367.612 ns |     10.2643 ns |     15.0453 ns |     106.97 |     14.23 |   0.2041 |       - |       - |    1288 B |
|       EmitLogEventWith10Enrich10ForContext10LogContext | core31 | .NET Core 3.1 |     6,528.445 ns |     41.1164 ns |     61.5411 ns |     508.23 |     66.12 |   1.2054 |  0.0076 |       - |    7584 B |
|    EmitLogEventWith100Enrich100ForContext100LogContext | core31 | .NET Core 3.1 |    56,665.183 ns |    337.7965 ns |    505.5979 ns |   4,409.82 |    561.68 |  11.8408 |  1.0986 |       - |   74616 B |
| EmitLogEventWith1000Enrich1000ForContext1000LogContext | core31 | .NET Core 3.1 | 1,152,646.960 ns | 13,275.3761 ns | 19,869.9578 ns |  89,709.22 | 11,565.28 | 119.1406 | 54.6875 | 29.2969 |  735963 B |
|                                                        |        |               |                  |                |                |            |           |          |         |         |           |
|                                   EmitLogAIgnoredEvent |  net48 |      .NET 4.8 |        14.798 ns |      0.7436 ns |      1.1130 ns |       1.00 |      0.00 |        - |       - |       - |         - |
|                                           EmitLogEvent |  net48 |      .NET 4.8 |       621.743 ns |      4.9990 ns |      7.4822 ns |      42.25 |      3.27 |   0.0591 |       - |       - |     377 B |
|          EmitLogEventWith1Enrich0ForContext0LogContext |  net48 |      .NET 4.8 |       691.659 ns |      4.3941 ns |      6.5769 ns |      46.99 |      3.53 |   0.0687 |       - |       - |     433 B |
|          EmitLogEventWith0Enrich1ForContext0LogContext |  net48 |      .NET 4.8 |       679.456 ns |      4.7002 ns |      7.0351 ns |      46.15 |      3.32 |   0.0591 |       - |       - |     377 B |
|          EmitLogEventWith0Enrich0ForContext1LogContext |  net48 |      .NET 4.8 |     1,291.364 ns |      8.7341 ns |     13.0728 ns |      87.74 |      6.61 |   0.1602 |       - |       - |    1011 B |
|          EmitLogEventWith1Enrich1ForContext1LogContext |  net48 |      .NET 4.8 |     1,472.631 ns |     10.4095 ns |     15.5804 ns |     100.09 |      7.96 |   0.2079 |       - |       - |    1316 B |
|       EmitLogEventWith10Enrich10ForContext10LogContext |  net48 |      .NET 4.8 |     7,465.573 ns |     74.6539 ns |    111.7384 ns |     506.80 |     31.91 |   1.1826 |  0.0076 |       - |    7486 B |
|    EmitLogEventWith100Enrich100ForContext100LogContext |  net48 |      .NET 4.8 |    68,158.289 ns |    502.7733 ns |    752.5273 ns |   4,632.43 |    367.91 |  11.7188 |  0.8545 |       - |   73988 B |
| EmitLogEventWith1000Enrich1000ForContext1000LogContext |  net48 |      .NET 4.8 | 1,258,606.758 ns |  9,292.5980 ns | 13,908.7232 ns |  85,524.63 |  6,573.31 | 119.1406 | 56.6406 | 29.2969 |  737003 B |
|                                                        |        |               |                  |                |                |            |           |          |         |         |           |
|                                   EmitLogAIgnoredEvent |  net50 | .NET Core 5.0 |         7.732 ns |      0.0704 ns |      0.1054 ns |       1.00 |      0.00 |        - |       - |       - |         - |
|                                           EmitLogEvent |  net50 | .NET Core 5.0 |       351.191 ns |      2.6519 ns |      3.9692 ns |      45.43 |      0.99 |   0.0596 |       - |       - |     376 B |
|          EmitLogEventWith1Enrich0ForContext0LogContext |  net50 | .NET Core 5.0 |       403.629 ns |      2.4226 ns |      3.6261 ns |      52.21 |      0.82 |   0.0687 |       - |       - |     432 B |
|          EmitLogEventWith0Enrich1ForContext0LogContext |  net50 | .NET Core 5.0 |       391.945 ns |      2.7547 ns |      4.1231 ns |      50.70 |      0.95 |   0.0596 |       - |       - |     376 B |
|          EmitLogEventWith0Enrich0ForContext1LogContext |  net50 | .NET Core 5.0 |       800.886 ns |     10.0891 ns |     14.7884 ns |     103.56 |      2.14 |   0.1574 |       - |       - |     992 B |
|          EmitLogEventWith1Enrich1ForContext1LogContext |  net50 | .NET Core 5.0 |       987.795 ns |      8.8097 ns |     13.1860 ns |     127.78 |      2.46 |   0.2060 |       - |       - |    1296 B |
|       EmitLogEventWith10Enrich10ForContext10LogContext |  net50 | .NET Core 5.0 |     5,121.840 ns |     37.7533 ns |     56.5074 ns |     662.52 |      9.83 |   1.2054 |  0.0076 |       - |    7592 B |
|    EmitLogEventWith100Enrich100ForContext100LogContext |  net50 | .NET Core 5.0 |    44,730.038 ns |    373.9866 ns |    559.7656 ns |   5,785.75 |     83.94 |  11.8408 |  0.9766 |       - |   74624 B |
| EmitLogEventWith1000Enrich1000ForContext1000LogContext |  net50 | .NET Core 5.0 | 1,018,136.263 ns | 13,745.6026 ns | 20,573.7707 ns | 131,694.62 |  2,862.34 | 119.1406 | 56.6406 | 29.2969 |  735952 B |
