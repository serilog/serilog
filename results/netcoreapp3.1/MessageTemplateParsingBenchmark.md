``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.450 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.401
  [Host]     : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT
  DefaultJob : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT


```
|                         Method |       Mean |    Error |   StdDev | Ratio | RatioSD |  Gen 0 |  Gen 1 | Gen 2 | Allocated |
|------------------------------- |-----------:|---------:|---------:|------:|--------:|-------:|-------:|------:|----------:|
|                  EmptyTemplate |   151.9 ns |  2.90 ns |  3.57 ns |  1.00 |    0.00 | 0.0408 |      - |     - |     256 B |
|             SimpleTextTemplate |   223.5 ns |  3.28 ns |  3.07 ns |  1.47 |    0.04 | 0.0648 |      - |     - |     408 B |
|    SinglePropertyTokenTemplate |   277.5 ns |  5.41 ns |  6.43 ns |  1.83 |    0.08 | 0.0877 |      - |     - |     552 B |
| SingleTextWithPropertyTemplate |   534.0 ns | 10.52 ns | 12.91 ns |  3.52 |    0.12 | 0.1478 |      - |     - |     928 B |
|      ManyPropertyTokenTemplate |   506.4 ns |  9.97 ns | 12.61 ns |  3.33 |    0.12 | 0.1650 |      - |     - |    1040 B |
|         MultipleTokensTemplate | 1,028.0 ns | 20.30 ns | 26.40 ns |  6.78 |    0.24 | 0.2823 |      - |     - |    1776 B |
|   DefaultConsoleOutputTemplate | 1,293.7 ns | 25.50 ns | 29.36 ns |  8.53 |    0.33 | 0.3567 |      - |     - |    2240 B |
|                    BigTemplate | 3,689.6 ns | 72.24 ns | 85.99 ns | 24.30 |    0.73 | 0.9918 | 0.0076 |     - |    6264 B |
