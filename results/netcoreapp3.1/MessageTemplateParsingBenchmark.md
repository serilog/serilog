``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.301
  [Host]     : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT
  DefaultJob : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT


```
|                         Method |       Mean |    Error |   StdDev |     Median | Ratio | RatioSD |  Gen 0 |  Gen 1 | Gen 2 | Allocated |
|------------------------------- |-----------:|---------:|---------:|-----------:|------:|--------:|-------:|-------:|------:|----------:|
|                  EmptyTemplate |   168.0 ns |  3.39 ns |  6.69 ns |   165.3 ns |  1.00 |    0.00 | 0.0408 |      - |     - |     256 B |
|             SimpleTextTemplate |   249.7 ns |  5.29 ns | 13.07 ns |   246.1 ns |  1.51 |    0.10 | 0.0648 |      - |     - |     408 B |
|    SinglePropertyTokenTemplate |   302.1 ns |  5.05 ns |  4.47 ns |   302.2 ns |  1.82 |    0.04 | 0.0877 |      - |     - |     552 B |
| SingleTextWithPropertyTemplate |   590.9 ns | 12.95 ns | 19.77 ns |   584.0 ns |  3.53 |    0.16 | 0.1478 |      - |     - |     928 B |
|      ManyPropertyTokenTemplate |   555.5 ns |  8.67 ns |  8.11 ns |   557.2 ns |  3.34 |    0.08 | 0.1650 |      - |     - |    1040 B |
|         MultipleTokensTemplate | 1,154.1 ns | 22.11 ns | 21.72 ns | 1,149.7 ns |  6.90 |    0.21 | 0.2823 |      - |     - |    1776 B |
|   DefaultConsoleOutputTemplate | 1,493.5 ns | 15.46 ns | 14.46 ns | 1,498.5 ns |  8.98 |    0.19 | 0.3567 |      - |     - |    2240 B |
|                    BigTemplate | 4,176.3 ns | 71.91 ns | 67.26 ns | 4,193.3 ns | 25.10 |    0.47 | 0.9918 | 0.0076 |     - |    6264 B |
