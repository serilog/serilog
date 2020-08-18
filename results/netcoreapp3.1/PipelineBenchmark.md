``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.450 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.401
  [Host]     : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT
  DefaultJob : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT


```
|                                                 Method |          Mean |        Error |       StdDev |    Ratio | RatioSD |   Gen 0 |  Gen 1 | Gen 2 | Allocated |
|------------------------------------------------------- |--------------:|-------------:|-------------:|---------:|--------:|--------:|-------:|------:|----------:|
|                                   EmitLogAIgnoredEvent |      13.29 ns |     0.290 ns |     0.443 ns |     1.00 |    0.00 |       - |      - |     - |         - |
|                                           EmitLogEvent |     621.94 ns |    12.377 ns |    11.578 ns |    47.53 |    2.38 |  0.0582 |      - |     - |     368 B |
|          EmitLogEventWith1Enrich0ForContext0LogContext |     674.86 ns |    11.932 ns |    11.161 ns |    51.57 |    2.50 |  0.0668 |      - |     - |     424 B |
|          EmitLogEventWith0Enrich1ForContext0LogContext |     670.51 ns |    12.771 ns |    13.665 ns |    50.94 |    2.43 |  0.0582 |      - |     - |     368 B |
|          EmitLogEventWith0Enrich0ForContext1LogContext |   1,172.76 ns |    22.709 ns |    29.528 ns |    88.64 |    4.94 |  0.1564 |      - |     - |     984 B |
|          EmitLogEventWith1Enrich1ForContext1LogContext |   1,330.54 ns |    26.040 ns |    27.863 ns |   101.10 |    5.06 |  0.1640 |      - |     - |    1040 B |
|       EmitLogEventWith10Enrich10ForContext10LogContext |   6,352.91 ns |   110.007 ns |   102.900 ns |   485.44 |   22.35 |  1.2054 | 0.0076 |     - |    7584 B |
|    EmitLogEventWith100Enrich100ForContext100LogContext |  12,524.90 ns |   224.647 ns |   210.135 ns |   957.31 |   50.46 |  2.6245 | 0.0458 |     - |   16560 B |
| EmitLogEventWith1000Enrich1000ForContext1000LogContext | 116,829.37 ns | 1,472.616 ns | 1,377.486 ns | 8,927.28 |  401.48 | 25.2686 | 4.1504 |     - |  158985 B |
