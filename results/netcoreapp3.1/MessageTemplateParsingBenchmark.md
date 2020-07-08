``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.301
  [Host]     : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT
  DefaultJob : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT


```
|                         Method |       Mean |    Error |   StdDev | Ratio | RatioSD |  Gen 0 |  Gen 1 | Gen 2 | Allocated |
|------------------------------- |-----------:|---------:|---------:|------:|--------:|-------:|-------:|------:|----------:|
|                  EmptyTemplate |   167.7 ns |  3.34 ns |  3.43 ns |  1.00 |    0.00 | 0.0408 |      - |     - |     256 B |
|             SimpleTextTemplate |   244.1 ns |  5.94 ns |  6.61 ns |  1.46 |    0.04 | 0.0648 |      - |     - |     408 B |
|    SinglePropertyTokenTemplate |   302.0 ns |  4.85 ns |  4.54 ns |  1.80 |    0.05 | 0.0877 |      - |     - |     552 B |
| SingleTextWithPropertyTemplate |   585.2 ns |  7.41 ns |  6.57 ns |  3.48 |    0.08 | 0.1478 |      - |     - |     928 B |
|      ManyPropertyTokenTemplate |   568.1 ns |  8.81 ns |  8.24 ns |  3.38 |    0.08 | 0.1650 |      - |     - |    1040 B |
|         MultipleTokensTemplate | 1,146.1 ns | 20.65 ns | 19.32 ns |  6.83 |    0.20 | 0.2823 |      - |     - |    1776 B |
|   DefaultConsoleOutputTemplate | 1,507.6 ns | 29.94 ns | 63.15 ns |  9.05 |    0.45 | 0.3567 |      - |     - |    2240 B |
|                    BigTemplate | 4,120.8 ns | 78.73 ns | 73.65 ns | 24.54 |    0.55 | 0.9918 | 0.0076 |     - |    6264 B |
