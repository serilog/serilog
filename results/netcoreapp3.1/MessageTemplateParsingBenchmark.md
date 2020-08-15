``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.450 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.401
  [Host]     : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT
  DefaultJob : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT


```
|                         Method |       Mean |    Error |   StdDev | Ratio | RatioSD |  Gen 0 |  Gen 1 | Gen 2 | Allocated |
|------------------------------- |-----------:|---------:|---------:|------:|--------:|-------:|-------:|------:|----------:|
|                  EmptyTemplate |   140.7 ns |  2.77 ns |  3.30 ns |  1.00 |    0.00 | 0.0408 |      - |     - |     256 B |
|             SimpleTextTemplate |   206.2 ns |  3.84 ns |  3.60 ns |  1.46 |    0.05 | 0.0648 |      - |     - |     408 B |
|    SinglePropertyTokenTemplate |   269.9 ns |  5.25 ns |  6.83 ns |  1.92 |    0.06 | 0.0877 |      - |     - |     552 B |
| SingleTextWithPropertyTemplate |   523.2 ns | 10.44 ns | 12.03 ns |  3.72 |    0.12 | 0.1478 |      - |     - |     928 B |
|      ManyPropertyTokenTemplate |   487.6 ns |  9.74 ns | 12.66 ns |  3.48 |    0.14 | 0.1650 |      - |     - |    1040 B |
|         MultipleTokensTemplate | 1,000.8 ns | 19.39 ns | 23.82 ns |  7.11 |    0.25 | 0.2823 |      - |     - |    1776 B |
|   DefaultConsoleOutputTemplate | 1,270.5 ns | 24.58 ns | 24.14 ns |  9.03 |    0.20 | 0.3567 |      - |     - |    2240 B |
|                    BigTemplate | 3,536.3 ns | 47.79 ns | 42.36 ns | 25.13 |    0.69 | 0.9956 | 0.0114 |     - |    6264 B |
