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
|                                   EmitLogAIgnoredEvent | core31 | .NET Core 3.1 |        13.204 ns |      1.3559 ns |      2.0295 ns |       1.00 |      0.00 |        - |       - |       - |         - |
|                                           EmitLogEvent | core31 | .NET Core 3.1 |       649.661 ns |     14.2782 ns |     21.3710 ns |      50.16 |      6.54 |   0.0582 |       - |       - |     368 B |
|          EmitLogEventWith1Enrich0ForContext0LogContext | core31 | .NET Core 3.1 |       701.819 ns |      9.5379 ns |     13.9805 ns |      54.83 |      9.03 |   0.0668 |       - |       - |     424 B |
|          EmitLogEventWith0Enrich1ForContext0LogContext | core31 | .NET Core 3.1 |       660.093 ns |      6.8101 ns |     10.1930 ns |      51.14 |      7.77 |   0.0582 |       - |       - |     368 B |
|          EmitLogEventWith0Enrich0ForContext1LogContext | core31 | .NET Core 3.1 |     1,142.579 ns |     14.1205 ns |     21.1348 ns |      88.43 |     12.92 |   0.1564 |       - |       - |     984 B |
|          EmitLogEventWith1Enrich1ForContext1LogContext | core31 | .NET Core 3.1 |     1,356.991 ns |     16.4596 ns |     24.6359 ns |     105.32 |     17.23 |   0.2041 |       - |       - |    1288 B |
|       EmitLogEventWith10Enrich10ForContext10LogContext | core31 | .NET Core 3.1 |     6,290.994 ns |     53.9264 ns |     80.7145 ns |     487.87 |     77.28 |   1.2054 |  0.0076 |       - |    7584 B |
|    EmitLogEventWith100Enrich100ForContext100LogContext | core31 | .NET Core 3.1 |    54,575.927 ns |    579.0997 ns |    866.7692 ns |   4,221.58 |    596.87 |  11.8408 |  1.0376 |       - |   74616 B |
| EmitLogEventWith1000Enrich1000ForContext1000LogContext | core31 | .NET Core 3.1 | 1,120,860.137 ns | 16,855.8757 ns | 25,229.0810 ns |  86,912.43 | 13,787.81 | 119.1406 | 52.7344 | 29.2969 |  735962 B |
|                                                        |        |               |                  |                |                |            |           |          |         |         |           |
|                                   EmitLogAIgnoredEvent |  net48 |      .NET 4.8 |        14.170 ns |      0.1712 ns |      0.2562 ns |       1.00 |      0.00 |        - |       - |       - |         - |
|                                           EmitLogEvent |  net48 |      .NET 4.8 |       610.816 ns |      5.6251 ns |      8.0674 ns |      43.14 |      0.92 |   0.0591 |       - |       - |     377 B |
|          EmitLogEventWith1Enrich0ForContext0LogContext |  net48 |      .NET 4.8 |       685.367 ns |      5.3604 ns |      7.8572 ns |      48.35 |      1.01 |   0.0687 |       - |       - |     433 B |
|          EmitLogEventWith0Enrich1ForContext0LogContext |  net48 |      .NET 4.8 |       666.566 ns |      6.0551 ns |      9.0630 ns |      47.05 |      0.99 |   0.0591 |       - |       - |     377 B |
|          EmitLogEventWith0Enrich0ForContext1LogContext |  net48 |      .NET 4.8 |     1,261.859 ns |     12.2035 ns |     18.2656 ns |      89.08 |      2.01 |   0.1602 |       - |       - |    1011 B |
|          EmitLogEventWith1Enrich1ForContext1LogContext |  net48 |      .NET 4.8 |     1,455.283 ns |     14.2702 ns |     21.3590 ns |     102.73 |      2.41 |   0.2079 |       - |       - |    1316 B |
|       EmitLogEventWith10Enrich10ForContext10LogContext |  net48 |      .NET 4.8 |     7,276.917 ns |     40.2771 ns |     60.2848 ns |     513.70 |     10.94 |   1.1826 |  0.0076 |       - |    7486 B |
|    EmitLogEventWith100Enrich100ForContext100LogContext |  net48 |      .NET 4.8 |    66,664.144 ns |    868.2308 ns |  1,299.5270 ns |   4,705.58 |    111.28 |  11.7188 |  0.8545 |       - |   73988 B |
| EmitLogEventWith1000Enrich1000ForContext1000LogContext |  net48 |      .NET 4.8 | 1,223,477.526 ns | 18,035.8068 ns | 26,995.1464 ns |  86,363.81 |  2,342.89 | 119.1406 | 56.6406 | 29.2969 |  736999 B |
|                                                        |        |               |                  |                |                |            |           |          |         |         |           |
|                                   EmitLogAIgnoredEvent |  net50 | .NET Core 5.0 |         6.914 ns |      0.0972 ns |      0.1455 ns |       1.00 |      0.00 |        - |       - |       - |         - |
|                                           EmitLogEvent |  net50 | .NET Core 5.0 |       349.810 ns |      5.3468 ns |      8.0028 ns |      50.62 |      1.58 |   0.0596 |       - |       - |     376 B |
|          EmitLogEventWith1Enrich0ForContext0LogContext |  net50 | .NET Core 5.0 |       398.966 ns |      3.4258 ns |      5.1276 ns |      57.73 |      1.43 |   0.0687 |       - |       - |     432 B |
|          EmitLogEventWith0Enrich1ForContext0LogContext |  net50 | .NET Core 5.0 |       390.634 ns |      4.4056 ns |      6.5941 ns |      56.52 |      1.44 |   0.0596 |       - |       - |     376 B |
|          EmitLogEventWith0Enrich0ForContext1LogContext |  net50 | .NET Core 5.0 |       787.301 ns |      6.9496 ns |     10.4018 ns |     113.92 |      2.58 |   0.1574 |       - |       - |     992 B |
|          EmitLogEventWith1Enrich1ForContext1LogContext |  net50 | .NET Core 5.0 |       950.620 ns |     12.0235 ns |     17.9962 ns |     137.55 |      3.45 |   0.2060 |       - |       - |    1296 B |
|       EmitLogEventWith10Enrich10ForContext10LogContext |  net50 | .NET Core 5.0 |     4,885.829 ns |     52.9342 ns |     79.2295 ns |     707.00 |     19.01 |   1.2054 |  0.0076 |       - |    7592 B |
|    EmitLogEventWith100Enrich100ForContext100LogContext |  net50 | .NET Core 5.0 |    43,733.077 ns |    399.2905 ns |    585.2748 ns |   6,330.49 |    170.92 |  11.8408 |  0.9766 |       - |   74624 B |
| EmitLogEventWith1000Enrich1000ForContext1000LogContext |  net50 | .NET Core 5.0 |   989,877.783 ns | 11,232.8659 ns | 16,812.8247 ns | 143,238.64 |  3,940.92 | 119.1406 | 56.6406 | 29.2969 |  735952 B |
