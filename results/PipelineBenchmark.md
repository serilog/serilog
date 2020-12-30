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
|                                   EmitLogAIgnoredEvent | core31 | .NET Core 3.1 |        14.409 ns |      1.5621 ns |      2.3380 ns |       1.00 |      0.00 |        - |       - |       - |         - |
|                                           EmitLogEvent | core31 | .NET Core 3.1 |       597.460 ns |      9.7495 ns |     14.5926 ns |      42.45 |      6.37 |   0.0582 |       - |       - |     368 B |
|          EmitLogEventWith1Enrich0ForContext0LogContext | core31 | .NET Core 3.1 |       650.154 ns |      7.7825 ns |     11.6485 ns |      46.28 |      7.42 |   0.0668 |       - |       - |     424 B |
|          EmitLogEventWith0Enrich1ForContext0LogContext | core31 | .NET Core 3.1 |       636.575 ns |      9.9776 ns |     14.6250 ns |      45.60 |      7.29 |   0.0582 |       - |       - |     368 B |
|          EmitLogEventWith0Enrich0ForContext1LogContext | core31 | .NET Core 3.1 |     1,137.109 ns |     22.6508 ns |     33.9026 ns |      81.13 |     14.23 |   0.1564 |       - |       - |     984 B |
|          EmitLogEventWith1Enrich1ForContext1LogContext | core31 | .NET Core 3.1 |     1,298.727 ns |     19.3276 ns |     28.9286 ns |      92.46 |     14.95 |   0.2041 |       - |       - |    1288 B |
|       EmitLogEventWith10Enrich10ForContext10LogContext | core31 | .NET Core 3.1 |     6,209.886 ns |     75.2470 ns |    112.6262 ns |     442.61 |     74.71 |   1.2054 |  0.0076 |       - |    7584 B |
|    EmitLogEventWith100Enrich100ForContext100LogContext | core31 | .NET Core 3.1 |    53,329.303 ns |    517.3334 ns |    774.3202 ns |   3,801.18 |    640.82 |  11.8408 |  1.0376 |       - |   74616 B |
| EmitLogEventWith1000Enrich1000ForContext1000LogContext | core31 | .NET Core 3.1 | 1,114,925.365 ns | 19,807.3025 ns | 29,646.6378 ns |  79,466.41 | 13,532.08 | 119.1406 | 52.7344 | 29.2969 |  735962 B |
|                                                        |        |               |                  |                |                |            |           |          |         |         |           |
|                                   EmitLogAIgnoredEvent |  net48 |      .NET 4.8 |        13.321 ns |      0.1008 ns |      0.1509 ns |       1.00 |      0.00 |        - |       - |       - |         - |
|                                           EmitLogEvent |  net48 |      .NET 4.8 |       598.396 ns |      8.4140 ns |     12.5937 ns |      44.93 |      1.11 |   0.0591 |       - |       - |     377 B |
|          EmitLogEventWith1Enrich0ForContext0LogContext |  net48 |      .NET 4.8 |       672.035 ns |      9.7063 ns |     14.5280 ns |      50.46 |      1.35 |   0.0687 |       - |       - |     433 B |
|          EmitLogEventWith0Enrich1ForContext0LogContext |  net48 |      .NET 4.8 |       652.797 ns |      9.2513 ns |     13.8469 ns |      49.01 |      1.18 |   0.0591 |       - |       - |     377 B |
|          EmitLogEventWith0Enrich0ForContext1LogContext |  net48 |      .NET 4.8 |     1,241.015 ns |     22.1048 ns |     33.0854 ns |      93.18 |      2.79 |   0.1602 |       - |       - |    1011 B |
|          EmitLogEventWith1Enrich1ForContext1LogContext |  net48 |      .NET 4.8 |     1,450.328 ns |     19.0920 ns |     28.5760 ns |     108.89 |      2.64 |   0.2079 |       - |       - |    1316 B |
|       EmitLogEventWith10Enrich10ForContext10LogContext |  net48 |      .NET 4.8 |     7,154.232 ns |     51.4528 ns |     77.0121 ns |     537.12 |      7.94 |   1.1826 |  0.0076 |       - |    7486 B |
|    EmitLogEventWith100Enrich100ForContext100LogContext |  net48 |      .NET 4.8 |    66,443.853 ns |    857.3812 ns |  1,283.2879 ns |   4,988.67 |    119.81 |  11.7188 |  0.8545 |       - |   73988 B |
| EmitLogEventWith1000Enrich1000ForContext1000LogContext |  net48 |      .NET 4.8 | 1,215,565.898 ns | 21,361.3804 ns | 31,972.7083 ns |  91,262.91 |  2,622.35 | 119.1406 | 56.6406 | 29.2969 |  737003 B |
|                                                        |        |               |                  |                |                |            |           |          |         |         |           |
|                                   EmitLogAIgnoredEvent |  net50 | .NET Core 5.0 |         7.483 ns |      0.4570 ns |      0.6840 ns |       1.00 |      0.00 |        - |       - |       - |         - |
|                                           EmitLogEvent |  net50 | .NET Core 5.0 |       352.968 ns |      4.1097 ns |      6.1513 ns |      47.57 |      4.55 |   0.0596 |       - |       - |     376 B |
|          EmitLogEventWith1Enrich0ForContext0LogContext |  net50 | .NET Core 5.0 |       388.100 ns |      4.2474 ns |      6.3573 ns |      52.30 |      5.03 |   0.0687 |       - |       - |     432 B |
|          EmitLogEventWith0Enrich1ForContext0LogContext |  net50 | .NET Core 5.0 |       387.186 ns |      3.8580 ns |      5.7745 ns |      52.17 |      4.92 |   0.0596 |       - |       - |     376 B |
|          EmitLogEventWith0Enrich0ForContext1LogContext |  net50 | .NET Core 5.0 |       790.188 ns |     10.4581 ns |     15.6532 ns |     106.56 |     11.07 |   0.1574 |       - |       - |     992 B |
|          EmitLogEventWith1Enrich1ForContext1LogContext |  net50 | .NET Core 5.0 |       952.876 ns |     16.5995 ns |     24.8453 ns |     128.21 |     10.14 |   0.2060 |       - |       - |    1296 B |
|       EmitLogEventWith10Enrich10ForContext10LogContext |  net50 | .NET Core 5.0 |     4,896.389 ns |     90.5332 ns |    135.5058 ns |     659.09 |     56.55 |   1.2054 |  0.0076 |       - |    7592 B |
|    EmitLogEventWith100Enrich100ForContext100LogContext |  net50 | .NET Core 5.0 |    43,053.616 ns |    515.6354 ns |    771.7788 ns |   5,802.21 |    558.55 |  11.8408 |  0.9766 |       - |   74624 B |
| EmitLogEventWith1000Enrich1000ForContext1000LogContext |  net50 | .NET Core 5.0 |   987,366.439 ns | 18,889.4138 ns | 28,272.7852 ns | 133,014.42 | 12,579.97 | 119.1406 | 56.6406 | 29.2969 |  735952 B |
