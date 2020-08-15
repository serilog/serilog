``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.450 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.401
  [Host]     : .NET Core 2.1.21 (CoreCLR 4.6.29130.01, CoreFX 4.6.29130.02), X64 RyuJIT
  DefaultJob : .NET Core 2.1.21 (CoreCLR 4.6.29130.01, CoreFX 4.6.29130.02), X64 RyuJIT


```
|                         Method |       Mean |    Error |   StdDev | Ratio | RatioSD |  Gen 0 |  Gen 1 | Gen 2 | Allocated |
|------------------------------- |-----------:|---------:|---------:|------:|--------:|-------:|-------:|------:|----------:|
|                  EmptyTemplate |   244.3 ns |  4.90 ns |  9.08 ns |  1.00 |    0.00 | 0.0415 |      - |     - |     264 B |
|             SimpleTextTemplate |   307.0 ns |  6.14 ns |  9.00 ns |  1.25 |    0.07 | 0.0672 |      - |     - |     424 B |
|    SinglePropertyTokenTemplate |   414.8 ns |  8.31 ns |  7.78 ns |  1.69 |    0.08 | 0.0901 |      - |     - |     568 B |
| SingleTextWithPropertyTemplate |   640.1 ns | 12.40 ns | 13.78 ns |  2.61 |    0.11 | 0.1497 |      - |     - |     944 B |
|      ManyPropertyTokenTemplate |   679.7 ns | 13.08 ns | 14.54 ns |  2.77 |    0.09 | 0.1698 |      - |     - |    1072 B |
|         MultipleTokensTemplate | 1,456.9 ns | 28.08 ns | 30.04 ns |  5.92 |    0.29 | 0.2918 |      - |     - |    1840 B |
|   DefaultConsoleOutputTemplate | 1,742.9 ns | 22.40 ns | 20.95 ns |  7.08 |    0.28 | 0.3643 |      - |     - |    2296 B |
|                    BigTemplate | 4,773.0 ns | 72.42 ns | 67.74 ns | 19.39 |    0.69 | 1.0300 | 0.0076 |     - |    6496 B |
