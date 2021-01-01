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
|                                                 Method |    Job |       Runtime |             Mean |         Error |         StdDev |           Median |      Ratio |  RatioSD |    Gen 0 |   Gen 1 |   Gen 2 | Allocated |
|------------------------------------------------------- |------- |-------------- |-----------------:|--------------:|---------------:|-----------------:|-----------:|---------:|---------:|--------:|--------:|----------:|
|                                   EmitLogAIgnoredEvent | core31 | .NET Core 3.1 |        11.660 ns |     0.6066 ns |      0.8700 ns |        11.941 ns |       1.00 |     0.00 |        - |       - |       - |         - |
|                                           EmitLogEvent | core31 | .NET Core 3.1 |       575.859 ns |     3.0560 ns |      4.4795 ns |       575.423 ns |      49.66 |     3.56 |   0.0582 |       - |       - |     368 B |
|          EmitLogEventWith1Enrich0ForContext0LogContext | core31 | .NET Core 3.1 |       651.179 ns |     3.5345 ns |      5.1809 ns |       652.062 ns |      56.11 |     3.95 |   0.0668 |       - |       - |     424 B |
|          EmitLogEventWith0Enrich1ForContext0LogContext | core31 | .NET Core 3.1 |       614.342 ns |     5.9985 ns |      8.9783 ns |       613.488 ns |      52.96 |     4.59 |   0.0582 |       - |       - |     368 B |
|          EmitLogEventWith0Enrich0ForContext1LogContext | core31 | .NET Core 3.1 |     1,105.328 ns |    13.5206 ns |     20.2370 ns |     1,106.380 ns |      95.31 |     8.76 |   0.1564 |       - |       - |     984 B |
|          EmitLogEventWith1Enrich1ForContext1LogContext | core31 | .NET Core 3.1 |     1,307.203 ns |    10.8556 ns |     15.9120 ns |     1,301.117 ns |     112.68 |     7.31 |   0.2041 |       - |       - |    1288 B |
|       EmitLogEventWith10Enrich10ForContext10LogContext | core31 | .NET Core 3.1 |     6,044.906 ns |    18.8235 ns |     26.9961 ns |     6,041.246 ns |     521.25 |    39.06 |   1.2054 |  0.0076 |       - |    7584 B |
|    EmitLogEventWith100Enrich100ForContext100LogContext | core31 | .NET Core 3.1 |    51,969.105 ns |   130.3059 ns |    178.3641 ns |    51,937.241 ns |   4,455.78 |   331.12 |  11.8408 |  1.0986 |       - |   74616 B |
| EmitLogEventWith1000Enrich1000ForContext1000LogContext | core31 | .NET Core 3.1 | 1,088,703.866 ns | 5,374.4707 ns |  7,877.8294 ns | 1,091,017.188 ns |  93,873.77 | 6,992.79 | 119.1406 | 52.7344 | 29.2969 |  735963 B |
|                                                        |        |               |                  |               |                |                  |            |          |          |         |         |           |
|                                   EmitLogAIgnoredEvent |  net48 |      .NET 4.8 |        13.466 ns |     0.4322 ns |      0.6469 ns |        13.390 ns |       1.00 |     0.00 |        - |       - |       - |         - |
|                                           EmitLogEvent |  net48 |      .NET 4.8 |       579.782 ns |     2.8500 ns |      4.1775 ns |       579.191 ns |      43.20 |     1.98 |   0.0591 |       - |       - |     377 B |
|          EmitLogEventWith1Enrich0ForContext0LogContext |  net48 |      .NET 4.8 |       653.614 ns |     2.7932 ns |      4.1808 ns |       653.622 ns |      48.65 |     2.50 |   0.0687 |       - |       - |     433 B |
|          EmitLogEventWith0Enrich1ForContext0LogContext |  net48 |      .NET 4.8 |       635.441 ns |     3.8579 ns |      5.6549 ns |       636.380 ns |      47.34 |     2.01 |   0.0591 |       - |       - |     377 B |
|          EmitLogEventWith0Enrich0ForContext1LogContext |  net48 |      .NET 4.8 |     1,251.351 ns |    13.8792 ns |     20.7737 ns |     1,246.084 ns |      93.11 |     4.23 |   0.1602 |       - |       - |    1011 B |
|          EmitLogEventWith1Enrich1ForContext1LogContext |  net48 |      .NET 4.8 |     1,441.149 ns |    12.2858 ns |     18.0084 ns |     1,438.817 ns |     107.45 |     6.25 |   0.2079 |       - |       - |    1316 B |
|       EmitLogEventWith10Enrich10ForContext10LogContext |  net48 |      .NET 4.8 |     7,045.152 ns |    29.8630 ns |     42.8286 ns |     7,030.769 ns |     525.90 |    26.84 |   1.1826 |  0.0076 |       - |    7486 B |
|    EmitLogEventWith100Enrich100ForContext100LogContext |  net48 |      .NET 4.8 |    64,634.200 ns |   214.5412 ns |    300.7568 ns |    64,622.412 ns |   4,832.13 |   238.66 |  11.7188 |  0.8545 |       - |   73988 B |
| EmitLogEventWith1000Enrich1000ForContext1000LogContext |  net48 |      .NET 4.8 | 1,183,970.814 ns | 7,660.6429 ns | 11,466.0896 ns | 1,183,139.551 ns |  88,130.29 | 4,599.46 | 119.1406 | 56.6406 | 29.2969 |  737005 B |
|                                                        |        |               |                  |               |                |                  |            |          |          |         |         |           |
|                                   EmitLogAIgnoredEvent |  net50 | .NET Core 5.0 |         6.695 ns |     0.0314 ns |      0.0460 ns |         6.691 ns |       1.00 |     0.00 |        - |       - |       - |         - |
|                                           EmitLogEvent |  net50 | .NET Core 5.0 |       365.872 ns |     1.5724 ns |      2.3535 ns |       365.918 ns |      54.67 |     0.46 |   0.0596 |       - |       - |     376 B |
|          EmitLogEventWith1Enrich0ForContext0LogContext |  net50 | .NET Core 5.0 |       409.407 ns |     2.2434 ns |      3.1449 ns |       409.697 ns |      61.18 |     0.66 |   0.0687 |       - |       - |     432 B |
|          EmitLogEventWith0Enrich1ForContext0LogContext |  net50 | .NET Core 5.0 |       408.544 ns |    10.3458 ns |     15.1648 ns |       397.482 ns |      61.02 |     2.34 |   0.0596 |       - |       - |     376 B |
|          EmitLogEventWith0Enrich0ForContext1LogContext |  net50 | .NET Core 5.0 |       781.006 ns |     7.3629 ns |     10.5597 ns |       781.248 ns |     116.64 |     1.64 |   0.1574 |       - |       - |     992 B |
|          EmitLogEventWith1Enrich1ForContext1LogContext |  net50 | .NET Core 5.0 |       971.404 ns |    13.4271 ns |     20.0970 ns |       975.463 ns |     145.12 |     3.25 |   0.2060 |       - |       - |    1296 B |
|       EmitLogEventWith10Enrich10ForContext10LogContext |  net50 | .NET Core 5.0 |     4,766.462 ns |    26.5254 ns |     38.0419 ns |     4,765.738 ns |     711.90 |     8.48 |   1.2054 |  0.0076 |       - |    7592 B |
|    EmitLogEventWith100Enrich100ForContext100LogContext |  net50 | .NET Core 5.0 |    41,209.884 ns |   239.7791 ns |    351.4651 ns |    41,254.620 ns |   6,155.45 |    74.23 |  11.8408 |  0.9766 |       - |   74624 B |
| EmitLogEventWith1000Enrich1000ForContext1000LogContext |  net50 | .NET Core 5.0 |   946,254.426 ns | 3,135.3560 ns |  4,496.6339 ns |   945,030.957 ns | 141,325.89 | 1,123.77 | 121.0938 | 45.8984 | 30.2734 |  735952 B |
