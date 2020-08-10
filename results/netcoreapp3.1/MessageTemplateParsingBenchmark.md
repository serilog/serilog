``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.388 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.302
  [Host]     : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT
  DefaultJob : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT


```
|                         Method |       Mean |    Error |   StdDev | Ratio | RatioSD |  Gen 0 |  Gen 1 | Gen 2 | Allocated |
|------------------------------- |-----------:|---------:|---------:|------:|--------:|-------:|-------:|------:|----------:|
|                  EmptyTemplate |   170.6 ns |  2.74 ns |  2.56 ns |  1.00 |    0.00 | 0.0408 |      - |     - |     256 B |
|             SimpleTextTemplate |   234.5 ns |  3.25 ns |  2.88 ns |  1.38 |    0.03 | 0.0648 |      - |     - |     408 B |
|    SinglePropertyTokenTemplate |   311.7 ns |  5.95 ns |  6.61 ns |  1.83 |    0.04 | 0.0877 |      - |     - |     552 B |
| SingleTextWithPropertyTemplate |   545.9 ns | 10.66 ns | 13.48 ns |  3.19 |    0.09 | 0.1478 |      - |     - |     928 B |
|      ManyPropertyTokenTemplate |   517.1 ns | 10.30 ns | 13.40 ns |  3.03 |    0.08 | 0.1650 |      - |     - |    1040 B |
|         MultipleTokensTemplate | 1,029.2 ns | 20.29 ns | 19.93 ns |  6.04 |    0.08 | 0.2823 |      - |     - |    1776 B |
|   DefaultConsoleOutputTemplate | 1,332.4 ns | 26.23 ns | 24.54 ns |  7.81 |    0.12 | 0.3567 |      - |     - |    2240 B |
|                    BigTemplate | 3,600.9 ns | 37.36 ns | 34.94 ns | 21.11 |    0.40 | 0.9956 | 0.0114 |     - |    6264 B |
