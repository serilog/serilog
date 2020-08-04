``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.302
  [Host]     : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT
  DefaultJob : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT


```
|                         Method |       Mean |    Error |   StdDev | Ratio | RatioSD |  Gen 0 |  Gen 1 | Gen 2 | Allocated |
|------------------------------- |-----------:|---------:|---------:|------:|--------:|-------:|-------:|------:|----------:|
|                  EmptyTemplate |   152.3 ns |  3.04 ns |  3.12 ns |  1.00 |    0.00 | 0.0408 |      - |     - |     256 B |
|             SimpleTextTemplate |   224.9 ns |  3.15 ns |  2.95 ns |  1.48 |    0.04 | 0.0648 |      - |     - |     408 B |
|    SinglePropertyTokenTemplate |   275.1 ns |  5.38 ns |  5.76 ns |  1.80 |    0.06 | 0.0877 |      - |     - |     552 B |
| SingleTextWithPropertyTemplate |   533.7 ns | 10.59 ns | 11.34 ns |  3.51 |    0.12 | 0.1478 |      - |     - |     928 B |
|      ManyPropertyTokenTemplate |   510.2 ns | 10.01 ns | 13.70 ns |  3.36 |    0.13 | 0.1650 |      - |     - |    1040 B |
|         MultipleTokensTemplate | 1,055.4 ns | 20.79 ns | 24.75 ns |  6.93 |    0.26 | 0.2823 |      - |     - |    1776 B |
|   DefaultConsoleOutputTemplate | 1,322.1 ns | 19.88 ns | 18.59 ns |  8.67 |    0.23 | 0.3567 |      - |     - |    2240 B |
|                    BigTemplate | 3,711.8 ns | 73.63 ns | 81.84 ns | 24.39 |    0.69 | 0.9918 | 0.0076 |     - |    6264 B |
