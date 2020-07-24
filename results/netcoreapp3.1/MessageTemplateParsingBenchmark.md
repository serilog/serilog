``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.302
  [Host]     : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT
  DefaultJob : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT


```
|                         Method |       Mean |    Error |   StdDev | Ratio | RatioSD |  Gen 0 |  Gen 1 | Gen 2 | Allocated |
|------------------------------- |-----------:|---------:|---------:|------:|--------:|-------:|-------:|------:|----------:|
|                  EmptyTemplate |   160.7 ns |  1.76 ns |  1.56 ns |  1.00 |    0.00 | 0.0408 |      - |     - |     256 B |
|             SimpleTextTemplate |   234.2 ns |  2.28 ns |  2.14 ns |  1.46 |    0.02 | 0.0648 |      - |     - |     408 B |
|    SinglePropertyTokenTemplate |   297.6 ns |  8.85 ns | 15.27 ns |  1.87 |    0.08 | 0.0877 |      - |     - |     552 B |
| SingleTextWithPropertyTemplate |   552.1 ns |  8.89 ns |  8.31 ns |  3.44 |    0.06 | 0.1478 |      - |     - |     928 B |
|      ManyPropertyTokenTemplate |   522.9 ns |  9.12 ns |  8.53 ns |  3.26 |    0.06 | 0.1650 |      - |     - |    1040 B |
|         MultipleTokensTemplate | 1,082.3 ns | 18.87 ns | 17.65 ns |  6.74 |    0.13 | 0.2823 |      - |     - |    1776 B |
|   DefaultConsoleOutputTemplate | 1,383.4 ns | 15.44 ns | 14.44 ns |  8.61 |    0.14 | 0.3567 |      - |     - |    2240 B |
|                    BigTemplate | 3,932.5 ns | 55.87 ns | 52.26 ns | 24.48 |    0.42 | 0.9918 | 0.0076 |     - |    6264 B |
