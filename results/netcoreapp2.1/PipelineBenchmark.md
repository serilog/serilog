``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.450 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.401
  [Host]     : .NET Core 2.1.21 (CoreCLR 4.6.29130.01, CoreFX 4.6.29130.02), X64 RyuJIT
  DefaultJob : .NET Core 2.1.21 (CoreCLR 4.6.29130.01, CoreFX 4.6.29130.02), X64 RyuJIT


```
|                                                 Method |          Mean |        Error |       StdDev |     Ratio | RatioSD |   Gen 0 |  Gen 1 | Gen 2 | Allocated |
|------------------------------------------------------- |--------------:|-------------:|-------------:|----------:|--------:|--------:|-------:|------:|----------:|
|                                   EmitLogAIgnoredEvent |      11.41 ns |     0.204 ns |     0.190 ns |      1.00 |    0.00 |       - |      - |     - |         - |
|                                           EmitLogEvent |     629.41 ns |    12.219 ns |    14.071 ns |     55.16 |    1.68 |  0.0591 |      - |     - |     376 B |
|          EmitLogEventWith1Enrich0ForContext0LogContext |     695.23 ns |    13.017 ns |    13.367 ns |     61.00 |    1.73 |  0.0677 |      - |     - |     432 B |
|          EmitLogEventWith0Enrich1ForContext0LogContext |     677.14 ns |    13.251 ns |    15.259 ns |     59.36 |    1.60 |  0.0591 |      - |     - |     376 B |
|          EmitLogEventWith0Enrich0ForContext1LogContext |   1,344.98 ns |    26.702 ns |    30.750 ns |    118.01 |    3.68 |  0.1583 |      - |     - |    1000 B |
|          EmitLogEventWith1Enrich1ForContext1LogContext |   1,487.35 ns |    28.993 ns |    28.475 ns |    130.53 |    3.56 |  0.1659 |      - |     - |    1056 B |
|       EmitLogEventWith10Enrich10ForContext10LogContext |   6,699.25 ns |   101.233 ns |    94.694 ns |    587.15 |   16.23 |  1.2054 | 0.0076 |     - |    7600 B |
|    EmitLogEventWith100Enrich100ForContext100LogContext |  14,019.43 ns |   226.518 ns |   211.885 ns |  1,228.56 |   27.69 |  2.6245 | 0.0458 |     - |   16576 B |
| EmitLogEventWith1000Enrich1000ForContext1000LogContext | 122,020.99 ns | 1,454.243 ns | 1,289.148 ns | 10,706.11 |  191.51 | 25.1465 | 4.1504 |     - |  159000 B |
