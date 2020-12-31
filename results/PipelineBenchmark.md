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
|                                                 Method |    Job |       Runtime |             Mean |          Error |         StdDev |      Ratio |  RatioSD |    Gen 0 |   Gen 1 |   Gen 2 | Allocated |
|------------------------------------------------------- |------- |-------------- |-----------------:|---------------:|---------------:|-----------:|---------:|---------:|--------:|--------:|----------:|
|                                   EmitLogAIgnoredEvent | core31 | .NET Core 3.1 |        12.084 ns |      0.3601 ns |      0.5279 ns |       1.00 |     0.00 |        - |       - |       - |         - |
|                                           EmitLogEvent | core31 | .NET Core 3.1 |       582.759 ns |      5.1146 ns |      7.6553 ns |      48.30 |     2.49 |   0.0582 |       - |       - |     368 B |
|          EmitLogEventWith1Enrich0ForContext0LogContext | core31 | .NET Core 3.1 |       641.654 ns |      7.9846 ns |     11.9509 ns |      53.17 |     2.68 |   0.0668 |       - |       - |     424 B |
|          EmitLogEventWith0Enrich1ForContext0LogContext | core31 | .NET Core 3.1 |       625.514 ns |      8.5867 ns |     12.8522 ns |      51.89 |     2.45 |   0.0582 |       - |       - |     368 B |
|          EmitLogEventWith0Enrich0ForContext1LogContext | core31 | .NET Core 3.1 |     1,131.576 ns |     14.9728 ns |     22.4106 ns |      93.89 |     4.39 |   0.1564 |       - |       - |     984 B |
|          EmitLogEventWith1Enrich1ForContext1LogContext | core31 | .NET Core 3.1 |     1,295.524 ns |     16.7367 ns |     25.0507 ns |     107.41 |     5.36 |   0.2041 |       - |       - |    1288 B |
|       EmitLogEventWith10Enrich10ForContext10LogContext | core31 | .NET Core 3.1 |     6,188.166 ns |     71.1904 ns |    106.5544 ns |     512.95 |    23.67 |   1.2054 |  0.0076 |       - |    7584 B |
|    EmitLogEventWith100Enrich100ForContext100LogContext | core31 | .NET Core 3.1 |    53,145.070 ns |    535.5804 ns |    801.6316 ns |   4,404.96 |   171.86 |  11.8408 |  1.0986 |       - |   74617 B |
| EmitLogEventWith1000Enrich1000ForContext1000LogContext | core31 | .NET Core 3.1 | 1,117,415.072 ns | 17,679.8365 ns | 26,462.3468 ns |  92,563.08 | 4,404.43 | 119.1406 | 54.6875 | 29.2969 |  735963 B |
|                                                        |        |               |                  |                |                |            |          |          |         |         |           |
|                                   EmitLogAIgnoredEvent |  net48 |      .NET 4.8 |        13.101 ns |      0.2233 ns |      0.3342 ns |       1.00 |     0.00 |        - |       - |       - |         - |
|                                           EmitLogEvent |  net48 |      .NET 4.8 |       593.578 ns |      4.5743 ns |      6.8465 ns |      45.33 |     1.21 |   0.0591 |       - |       - |     377 B |
|          EmitLogEventWith1Enrich0ForContext0LogContext |  net48 |      .NET 4.8 |       666.522 ns |      8.5205 ns |     12.7531 ns |      50.91 |     1.64 |   0.0687 |       - |       - |     433 B |
|          EmitLogEventWith0Enrich1ForContext0LogContext |  net48 |      .NET 4.8 |       652.439 ns |      7.6315 ns |     11.4225 ns |      49.84 |     1.69 |   0.0591 |       - |       - |     377 B |
|          EmitLogEventWith0Enrich0ForContext1LogContext |  net48 |      .NET 4.8 |     1,253.051 ns |     17.1275 ns |     25.6357 ns |      95.71 |     3.24 |   0.1602 |       - |       - |    1011 B |
|          EmitLogEventWith1Enrich1ForContext1LogContext |  net48 |      .NET 4.8 |     1,462.696 ns |     18.4863 ns |     27.6695 ns |     111.71 |     3.40 |   0.2079 |       - |       - |    1316 B |
|       EmitLogEventWith10Enrich10ForContext10LogContext |  net48 |      .NET 4.8 |     7,173.767 ns |     56.2704 ns |     84.2229 ns |     547.90 |    15.03 |   1.1826 |  0.0076 |       - |    7486 B |
|    EmitLogEventWith100Enrich100ForContext100LogContext |  net48 |      .NET 4.8 |    66,432.848 ns |    971.8239 ns |  1,454.5803 ns |   5,074.37 |   182.40 |  11.7188 |  0.8545 |       - |   73988 B |
| EmitLogEventWith1000Enrich1000ForContext1000LogContext |  net48 |      .NET 4.8 | 1,214,281.523 ns | 18,806.3354 ns | 28,148.4373 ns |  92,743.53 | 3,195.35 | 119.1406 | 56.6406 | 29.2969 |  737005 B |
|                                                        |        |               |                  |                |                |            |          |          |         |         |           |
|                                   EmitLogAIgnoredEvent |  net50 | .NET Core 5.0 |         6.908 ns |      0.1530 ns |      0.2291 ns |       1.00 |     0.00 |        - |       - |       - |         - |
|                                           EmitLogEvent |  net50 | .NET Core 5.0 |       335.311 ns |      3.9529 ns |      5.9166 ns |      48.59 |     1.86 |   0.0596 |       - |       - |     376 B |
|          EmitLogEventWith1Enrich0ForContext0LogContext |  net50 | .NET Core 5.0 |       388.472 ns |      5.4924 ns |      8.2208 ns |      56.30 |     2.20 |   0.0687 |       - |       - |     432 B |
|          EmitLogEventWith0Enrich1ForContext0LogContext |  net50 | .NET Core 5.0 |       384.778 ns |      4.6453 ns |      6.9529 ns |      55.76 |     2.03 |   0.0596 |       - |       - |     376 B |
|          EmitLogEventWith0Enrich0ForContext1LogContext |  net50 | .NET Core 5.0 |       763.634 ns |     10.1292 ns |     15.1609 ns |     110.68 |     4.54 |   0.1574 |       - |       - |     992 B |
|          EmitLogEventWith1Enrich1ForContext1LogContext |  net50 | .NET Core 5.0 |       940.021 ns |     15.5983 ns |     23.3468 ns |     136.20 |     5.06 |   0.2060 |       - |       - |    1296 B |
|       EmitLogEventWith10Enrich10ForContext10LogContext |  net50 | .NET Core 5.0 |     4,840.504 ns |     59.7291 ns |     89.3997 ns |     701.66 |    31.15 |   1.2054 |  0.0076 |       - |    7592 B |
|    EmitLogEventWith100Enrich100ForContext100LogContext |  net50 | .NET Core 5.0 |    42,558.709 ns |    560.3027 ns |    838.6347 ns |   6,169.97 |   296.23 |  11.8408 |  0.9766 |       - |   74624 B |
| EmitLogEventWith1000Enrich1000ForContext1000LogContext |  net50 | .NET Core 5.0 |   983,028.652 ns |  6,413.5926 ns |  9,599.5635 ns | 142,459.93 | 4,959.38 | 121.0938 | 59.5703 | 30.2734 |  735952 B |
