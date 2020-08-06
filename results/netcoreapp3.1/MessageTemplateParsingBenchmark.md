``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.388 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.302
  [Host]     : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT
  DefaultJob : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT


```
|                         Method |       Mean |    Error |    StdDev | Ratio | RatioSD |  Gen 0 |  Gen 1 | Gen 2 | Allocated |
|------------------------------- |-----------:|---------:|----------:|------:|--------:|-------:|-------:|------:|----------:|
|                  EmptyTemplate |   151.7 ns |  2.73 ns |   2.55 ns |  1.00 |    0.00 | 0.0408 |      - |     - |     256 B |
|             SimpleTextTemplate |   217.6 ns |  4.32 ns |   3.61 ns |  1.43 |    0.04 | 0.0648 |      - |     - |     408 B |
|    SinglePropertyTokenTemplate |   274.5 ns |  5.42 ns |   5.56 ns |  1.81 |    0.02 | 0.0877 |      - |     - |     552 B |
| SingleTextWithPropertyTemplate |   543.0 ns | 10.23 ns |  18.71 ns |  3.60 |    0.15 | 0.1478 |      - |     - |     928 B |
|      ManyPropertyTokenTemplate |   512.2 ns | 10.25 ns |  11.81 ns |  3.38 |    0.12 | 0.1650 |      - |     - |    1040 B |
|         MultipleTokensTemplate | 1,041.9 ns | 20.51 ns |  25.93 ns |  6.89 |    0.26 | 0.2823 |      - |     - |    1776 B |
|   DefaultConsoleOutputTemplate | 1,352.9 ns | 22.69 ns |  21.23 ns |  8.92 |    0.24 | 0.3567 |      - |     - |    2240 B |
|                    BigTemplate | 3,800.7 ns | 75.58 ns | 115.41 ns | 25.06 |    0.97 | 0.9918 | 0.0076 |     - |    6264 B |
