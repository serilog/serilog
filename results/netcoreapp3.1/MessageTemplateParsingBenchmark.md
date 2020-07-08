``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.301
  [Host]     : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT
  DefaultJob : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT


```
|                         Method |       Mean |    Error |   StdDev | Ratio | RatioSD |  Gen 0 |  Gen 1 | Gen 2 | Allocated |
|------------------------------- |-----------:|---------:|---------:|------:|--------:|-------:|-------:|------:|----------:|
|                  EmptyTemplate |   165.1 ns |  3.32 ns |  5.82 ns |  1.00 |    0.00 | 0.0408 |      - |     - |     256 B |
|             SimpleTextTemplate |   229.9 ns |  2.53 ns |  2.11 ns |  1.37 |    0.06 | 0.0648 |      - |     - |     408 B |
|    SinglePropertyTokenTemplate |   284.2 ns |  4.59 ns |  4.29 ns |  1.69 |    0.06 | 0.0877 |      - |     - |     552 B |
| SingleTextWithPropertyTemplate |   550.8 ns | 10.91 ns | 13.40 ns |  3.30 |    0.15 | 0.1478 |      - |     - |     928 B |
|      ManyPropertyTokenTemplate |   530.5 ns | 10.51 ns | 10.32 ns |  3.16 |    0.14 | 0.1650 |      - |     - |    1040 B |
|         MultipleTokensTemplate | 1,108.5 ns | 21.16 ns | 27.52 ns |  6.63 |    0.28 | 0.2823 |      - |     - |    1776 B |
|   DefaultConsoleOutputTemplate | 1,425.0 ns | 27.57 ns | 25.78 ns |  8.49 |    0.24 | 0.3567 |      - |     - |    2240 B |
|                    BigTemplate | 3,956.2 ns | 78.29 ns | 90.16 ns | 23.73 |    0.99 | 0.9918 | 0.0076 |     - |    6264 B |
