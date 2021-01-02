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
|                                   EmitLogAIgnoredEvent | core31 | .NET Core 3.1 |        13.403 ns |      1.3245 ns |      1.9825 ns |       1.00 |      0.00 |        - |       - |       - |         - |
|                                           EmitLogEvent | core31 | .NET Core 3.1 |       590.343 ns |      5.7015 ns |      8.5338 ns |      44.97 |      6.50 |   0.0582 |       - |       - |     368 B |
|          EmitLogEventWith1Enrich0ForContext0LogContext | core31 | .NET Core 3.1 |       674.385 ns |      9.1947 ns |     12.8896 ns |      50.56 |      7.55 |   0.0668 |       - |       - |     424 B |
|          EmitLogEventWith0Enrich1ForContext0LogContext | core31 | .NET Core 3.1 |       642.768 ns |      6.2563 ns |      9.3642 ns |      48.98 |      7.22 |   0.0582 |       - |       - |     368 B |
|          EmitLogEventWith0Enrich0ForContext1LogContext | core31 | .NET Core 3.1 |     1,122.155 ns |     13.6597 ns |     20.4452 ns |      85.44 |     12.14 |   0.1564 |       - |       - |     984 B |
|          EmitLogEventWith1Enrich1ForContext1LogContext | core31 | .NET Core 3.1 |     1,302.467 ns |     16.4056 ns |     24.5551 ns |      99.12 |     13.65 |   0.2041 |       - |       - |    1288 B |
|       EmitLogEventWith10Enrich10ForContext10LogContext | core31 | .NET Core 3.1 |     6,145.871 ns |     50.0573 ns |     74.9234 ns |     468.66 |     71.02 |   1.2054 |  0.0076 |       - |    7584 B |
|    EmitLogEventWith100Enrich100ForContext100LogContext | core31 | .NET Core 3.1 |    52,654.020 ns |    447.4566 ns |    669.7320 ns |   4,009.81 |    570.96 |  11.8408 |  1.0376 |       - |   74616 B |
| EmitLogEventWith1000Enrich1000ForContext1000LogContext | core31 | .NET Core 3.1 | 1,158,627.206 ns | 49,012.7123 ns | 68,708.9774 ns |  87,345.48 | 16,806.18 | 119.1406 | 52.7344 | 29.2969 |  735963 B |
|                                                        |        |               |                  |                |                |            |           |          |         |         |           |
|                                   EmitLogAIgnoredEvent |  net48 |      .NET 4.8 |        14.247 ns |      0.4418 ns |      0.6476 ns |       1.00 |      0.00 |        - |       - |       - |         - |
|                                           EmitLogEvent |  net48 |      .NET 4.8 |       595.220 ns |      5.4443 ns |      8.1488 ns |      41.84 |      1.89 |   0.0591 |       - |       - |     377 B |
|          EmitLogEventWith1Enrich0ForContext0LogContext |  net48 |      .NET 4.8 |       669.163 ns |      6.5014 ns |      9.7309 ns |      47.07 |      2.37 |   0.0687 |       - |       - |     433 B |
|          EmitLogEventWith0Enrich1ForContext0LogContext |  net48 |      .NET 4.8 |       650.404 ns |      6.4924 ns |      9.7176 ns |      45.70 |      1.90 |   0.0591 |       - |       - |     377 B |
|          EmitLogEventWith0Enrich0ForContext1LogContext |  net48 |      .NET 4.8 |     1,227.189 ns |     12.8174 ns |     19.1845 ns |      86.28 |      3.54 |   0.1602 |       - |       - |    1011 B |
|          EmitLogEventWith1Enrich1ForContext1LogContext |  net48 |      .NET 4.8 |     1,429.590 ns |     12.7333 ns |     19.0587 ns |     100.60 |      4.90 |   0.2079 |       - |       - |    1316 B |
|       EmitLogEventWith10Enrich10ForContext10LogContext |  net48 |      .NET 4.8 |     7,078.887 ns |     43.2392 ns |     64.7184 ns |     497.90 |     22.35 |   1.1826 |  0.0076 |       - |    7486 B |
|    EmitLogEventWith100Enrich100ForContext100LogContext |  net48 |      .NET 4.8 |    65,531.498 ns |  1,029.2566 ns |  1,540.5428 ns |   4,609.83 |    258.88 |  11.7188 |  0.8545 |       - |   73988 B |
| EmitLogEventWith1000Enrich1000ForContext1000LogContext |  net48 |      .NET 4.8 | 1,197,649.681 ns |  9,483.1076 ns | 14,193.8690 ns |  84,274.47 |  3,900.29 | 119.1406 | 56.6406 | 29.2969 |  736999 B |
|                                                        |        |               |                  |                |                |            |           |          |         |         |           |
|                                   EmitLogAIgnoredEvent |  net50 | .NET Core 5.0 |         6.617 ns |      0.0995 ns |      0.1489 ns |       1.00 |      0.00 |        - |       - |       - |         - |
|                                           EmitLogEvent |  net50 | .NET Core 5.0 |       341.410 ns |      4.2974 ns |      6.4322 ns |      51.62 |      1.42 |   0.0596 |       - |       - |     376 B |
|          EmitLogEventWith1Enrich0ForContext0LogContext |  net50 | .NET Core 5.0 |       409.101 ns |      7.8382 ns |     11.4892 ns |      61.82 |      2.35 |   0.0687 |       - |       - |     432 B |
|          EmitLogEventWith0Enrich1ForContext0LogContext |  net50 | .NET Core 5.0 |       393.542 ns |      4.1474 ns |      5.9480 ns |      59.46 |      1.50 |   0.0596 |       - |       - |     376 B |
|          EmitLogEventWith0Enrich0ForContext1LogContext |  net50 | .NET Core 5.0 |       803.459 ns |     11.8803 ns |     17.7819 ns |     121.50 |      4.12 |   0.1574 |       - |       - |     992 B |
|          EmitLogEventWith1Enrich1ForContext1LogContext |  net50 | .NET Core 5.0 |       944.309 ns |     20.1165 ns |     30.1095 ns |     142.78 |      5.35 |   0.2060 |       - |       - |    1296 B |
|       EmitLogEventWith10Enrich10ForContext10LogContext |  net50 | .NET Core 5.0 |     4,830.258 ns |     49.7374 ns |     74.4447 ns |     730.36 |     19.08 |   1.2054 |  0.0076 |       - |    7592 B |
|    EmitLogEventWith100Enrich100ForContext100LogContext |  net50 | .NET Core 5.0 |    42,782.111 ns |    431.5374 ns |    604.9551 ns |   6,462.54 |    179.65 |  11.8408 |  0.9766 |       - |   74624 B |
| EmitLogEventWith1000Enrich1000ForContext1000LogContext |  net50 | .NET Core 5.0 |   974,359.307 ns |  9,174.2896 ns | 13,731.6447 ns | 147,319.74 |  3,402.45 | 121.0938 | 59.5703 | 30.2734 |  735952 B |
