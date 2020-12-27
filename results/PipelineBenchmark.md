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
|                                                 Method |    Job |       Runtime |             Mean |          Error |         StdDev |           Median |      Ratio |   RatioSD |    Gen 0 |   Gen 1 |   Gen 2 | Allocated |
|------------------------------------------------------- |------- |-------------- |-----------------:|---------------:|---------------:|-----------------:|-----------:|----------:|---------:|--------:|--------:|----------:|
|                                   EmitLogAIgnoredEvent | core31 | .NET Core 3.1 |        12.584 ns |      1.0815 ns |      1.6188 ns |        12.583 ns |       1.00 |      0.00 |        - |       - |       - |         - |
|                                           EmitLogEvent | core31 | .NET Core 3.1 |       580.862 ns |      7.6457 ns |     11.2070 ns |       580.484 ns |      47.13 |      6.12 |   0.0582 |       - |       - |     368 B |
|          EmitLogEventWith1Enrich0ForContext0LogContext | core31 | .NET Core 3.1 |       641.133 ns |      6.0475 ns |      8.8643 ns |       640.768 ns |      51.98 |      6.48 |   0.0668 |       - |       - |     424 B |
|          EmitLogEventWith0Enrich1ForContext0LogContext | core31 | .NET Core 3.1 |       625.907 ns |      5.6979 ns |      8.5283 ns |       625.950 ns |      50.54 |      6.53 |   0.0582 |       - |       - |     368 B |
|          EmitLogEventWith0Enrich0ForContext1LogContext | core31 | .NET Core 3.1 |     1,132.837 ns |     10.6186 ns |     15.5646 ns |     1,133.630 ns |      91.95 |     12.21 |   0.1564 |       - |       - |     984 B |
|          EmitLogEventWith1Enrich1ForContext1LogContext | core31 | .NET Core 3.1 |     1,295.973 ns |     12.6819 ns |     18.1880 ns |     1,293.422 ns |     105.64 |     13.46 |   0.2041 |       - |       - |    1288 B |
|       EmitLogEventWith10Enrich10ForContext10LogContext | core31 | .NET Core 3.1 |     6,273.114 ns |     81.1146 ns |    118.8967 ns |     6,267.264 ns |     509.97 |     73.81 |   1.2054 |  0.0076 |       - |    7584 B |
|    EmitLogEventWith100Enrich100ForContext100LogContext | core31 | .NET Core 3.1 |    52,929.976 ns |    385.2930 ns |    576.6884 ns |    52,878.961 ns |   4,272.74 |    537.41 |  11.8408 |  1.0986 |       - |   74616 B |
| EmitLogEventWith1000Enrich1000ForContext1000LogContext | core31 | .NET Core 3.1 | 1,105,042.855 ns | 11,294.6725 ns | 16,905.3340 ns | 1,107,422.314 ns |  89,301.00 | 12,029.51 | 119.1406 | 54.6875 | 29.2969 |  735955 B |
|                                                        |        |               |                  |                |                |                  |            |           |          |         |         |           |
|                                   EmitLogAIgnoredEvent |  net48 |      .NET 4.8 |        13.894 ns |      0.5988 ns |      0.8778 ns |        13.273 ns |       1.00 |      0.00 |        - |       - |       - |         - |
|                                           EmitLogEvent |  net48 |      .NET 4.8 |       593.746 ns |      6.1391 ns |      9.1887 ns |       595.492 ns |      42.85 |      2.66 |   0.0591 |       - |       - |     377 B |
|          EmitLogEventWith1Enrich0ForContext0LogContext |  net48 |      .NET 4.8 |       669.168 ns |      5.5512 ns |      8.3087 ns |       669.494 ns |      48.36 |      3.05 |   0.0687 |       - |       - |     433 B |
|          EmitLogEventWith0Enrich1ForContext0LogContext |  net48 |      .NET 4.8 |       650.335 ns |      7.7208 ns |     11.5562 ns |       647.816 ns |      47.00 |      2.86 |   0.0591 |       - |       - |     377 B |
|          EmitLogEventWith0Enrich0ForContext1LogContext |  net48 |      .NET 4.8 |     1,263.354 ns |     11.4536 ns |     17.1431 ns |     1,266.766 ns |      91.23 |      5.60 |   0.1602 |       - |       - |    1011 B |
|          EmitLogEventWith1Enrich1ForContext1LogContext |  net48 |      .NET 4.8 |     1,468.031 ns |     13.7585 ns |     20.5930 ns |     1,467.502 ns |     105.98 |      6.31 |   0.2079 |       - |       - |    1316 B |
|       EmitLogEventWith10Enrich10ForContext10LogContext |  net48 |      .NET 4.8 |     7,224.696 ns |     52.1141 ns |     78.0019 ns |     7,209.886 ns |     522.27 |     37.00 |   1.1826 |  0.0076 |       - |    7486 B |
|    EmitLogEventWith100Enrich100ForContext100LogContext |  net48 |      .NET 4.8 |    66,507.662 ns |    775.6741 ns |  1,160.9925 ns |    66,820.581 ns |   4,810.10 |    323.90 |  11.7188 |  0.8545 |       - |   73988 B |
| EmitLogEventWith1000Enrich1000ForContext1000LogContext |  net48 |      .NET 4.8 | 1,212,636.536 ns | 13,493.1395 ns | 20,195.8959 ns | 1,212,454.004 ns |  87,529.49 |  5,760.68 | 119.1406 | 56.6406 | 29.2969 |  737005 B |
|                                                        |        |               |                  |                |                |                  |            |           |          |         |         |           |
|                                   EmitLogAIgnoredEvent |  net50 | .NET Core 5.0 |         7.265 ns |      0.0820 ns |      0.1228 ns |         7.277 ns |       1.00 |      0.00 |        - |       - |       - |         - |
|                                           EmitLogEvent |  net50 | .NET Core 5.0 |       339.371 ns |      4.7406 ns |      6.7988 ns |       337.669 ns |      46.75 |      1.25 |   0.0596 |       - |       - |     376 B |
|          EmitLogEventWith1Enrich0ForContext0LogContext |  net50 | .NET Core 5.0 |       387.879 ns |      2.9553 ns |      4.4234 ns |       388.635 ns |      53.41 |      1.17 |   0.0687 |       - |       - |     432 B |
|          EmitLogEventWith0Enrich1ForContext0LogContext |  net50 | .NET Core 5.0 |       386.622 ns |      5.1225 ns |      7.6671 ns |       386.515 ns |      53.23 |      1.26 |   0.0596 |       - |       - |     376 B |
|          EmitLogEventWith0Enrich0ForContext1LogContext |  net50 | .NET Core 5.0 |       807.886 ns |      7.9821 ns |     11.9473 ns |       808.194 ns |     111.24 |      2.68 |   0.1574 |       - |       - |     992 B |
|          EmitLogEventWith1Enrich1ForContext1LogContext |  net50 | .NET Core 5.0 |       954.627 ns |      9.7892 ns |     14.0393 ns |       953.488 ns |     131.50 |      2.61 |   0.2060 |       - |       - |    1296 B |
|       EmitLogEventWith10Enrich10ForContext10LogContext |  net50 | .NET Core 5.0 |     4,788.535 ns |     42.7017 ns |     63.9138 ns |     4,782.414 ns |     659.30 |     14.20 |   1.2054 |  0.0076 |       - |    7592 B |
|    EmitLogEventWith100Enrich100ForContext100LogContext |  net50 | .NET Core 5.0 |    42,602.376 ns |    381.7599 ns |    571.4002 ns |    42,698.965 ns |   5,865.63 |    125.43 |  11.8408 |  0.9766 |       - |   74624 B |
| EmitLogEventWith1000Enrich1000ForContext1000LogContext |  net50 | .NET Core 5.0 |   910,799.089 ns | 42,625.8690 ns | 63,800.3937 ns |   923,986.035 ns | 125,397.36 |  8,966.61 | 119.1406 | 44.9219 | 29.2969 |  735952 B |
