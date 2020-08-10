``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.388 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.302
  [Host]     : .NET Core 2.1.20 (CoreCLR 4.6.29017.01, CoreFX 4.6.29018.12), X64 RyuJIT
  DefaultJob : .NET Core 2.1.20 (CoreCLR 4.6.29017.01, CoreFX 4.6.29018.12), X64 RyuJIT


```
|                         Method |       Mean |    Error |   StdDev | Ratio | RatioSD |  Gen 0 |  Gen 1 | Gen 2 | Allocated |
|------------------------------- |-----------:|---------:|---------:|------:|--------:|-------:|-------:|------:|----------:|
|                  EmptyTemplate |   268.1 ns |  5.08 ns |  4.99 ns |  1.00 |    0.00 | 0.0415 |      - |     - |     264 B |
|             SimpleTextTemplate |   323.8 ns |  4.50 ns |  4.21 ns |  1.21 |    0.02 | 0.0672 |      - |     - |     424 B |
|    SinglePropertyTokenTemplate |   439.8 ns |  5.80 ns |  5.43 ns |  1.64 |    0.02 | 0.0901 |      - |     - |     568 B |
| SingleTextWithPropertyTemplate |   663.0 ns | 11.55 ns | 10.80 ns |  2.48 |    0.07 | 0.1497 |      - |     - |     944 B |
|      ManyPropertyTokenTemplate |   670.2 ns | 11.26 ns | 10.53 ns |  2.50 |    0.06 | 0.1698 |      - |     - |    1072 B |
|         MultipleTokensTemplate | 1,311.4 ns | 19.87 ns | 18.59 ns |  4.90 |    0.15 | 0.2918 |      - |     - |    1840 B |
|   DefaultConsoleOutputTemplate | 1,750.4 ns | 16.96 ns | 15.86 ns |  6.53 |    0.11 | 0.3643 |      - |     - |    2296 B |
|                    BigTemplate | 4,708.9 ns | 29.32 ns | 25.99 ns | 17.57 |    0.40 | 1.0300 | 0.0076 |     - |    6496 B |
