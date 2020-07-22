``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.301
  [Host]     : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT
  DefaultJob : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT


```
|                         Method |       Mean |    Error |   StdDev | Ratio | RatioSD |  Gen 0 |  Gen 1 | Gen 2 | Allocated |
|------------------------------- |-----------:|---------:|---------:|------:|--------:|-------:|-------:|------:|----------:|
|                  EmptyTemplate |   150.8 ns |  2.89 ns |  3.33 ns |  1.00 |    0.00 | 0.0408 |      - |     - |     256 B |
|             SimpleTextTemplate |   226.4 ns | 10.43 ns |  9.76 ns |  1.50 |    0.08 | 0.0648 |      - |     - |     408 B |
|    SinglePropertyTokenTemplate |   277.0 ns |  5.52 ns |  6.13 ns |  1.84 |    0.04 | 0.0877 |      - |     - |     552 B |
| SingleTextWithPropertyTemplate |   513.5 ns |  9.73 ns | 10.81 ns |  3.40 |    0.11 | 0.1478 |      - |     - |     928 B |
|      ManyPropertyTokenTemplate |   507.2 ns | 10.10 ns | 12.02 ns |  3.37 |    0.09 | 0.1650 |      - |     - |    1040 B |
|         MultipleTokensTemplate | 1,048.3 ns | 20.23 ns | 22.48 ns |  6.95 |    0.22 | 0.2823 |      - |     - |    1776 B |
|   DefaultConsoleOutputTemplate | 1,347.0 ns | 21.22 ns | 19.85 ns |  8.94 |    0.23 | 0.3567 |      - |     - |    2240 B |
|                    BigTemplate | 3,711.4 ns | 29.37 ns | 26.03 ns | 24.66 |    0.65 | 0.9956 | 0.0114 |     - |    6264 B |
