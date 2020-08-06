``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.388 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.302
  [Host]     : .NET Core 2.1.20 (CoreCLR 4.6.29017.01, CoreFX 4.6.29018.12), X64 RyuJIT
  DefaultJob : .NET Core 2.1.20 (CoreCLR 4.6.29017.01, CoreFX 4.6.29018.12), X64 RyuJIT


```
|                         Method |       Mean |    Error |    StdDev | Ratio | RatioSD |  Gen 0 |  Gen 1 | Gen 2 | Allocated |
|------------------------------- |-----------:|---------:|----------:|------:|--------:|-------:|-------:|------:|----------:|
|                  EmptyTemplate |   242.6 ns |  4.67 ns |   5.91 ns |  1.00 |    0.00 | 0.0415 |      - |     - |     264 B |
|             SimpleTextTemplate |   297.9 ns |  5.07 ns |   4.74 ns |  1.23 |    0.04 | 0.0672 |      - |     - |     424 B |
|    SinglePropertyTokenTemplate |   408.5 ns |  6.11 ns |   5.41 ns |  1.69 |    0.05 | 0.0901 |      - |     - |     568 B |
| SingleTextWithPropertyTemplate |   671.1 ns | 13.05 ns |  12.82 ns |  2.77 |    0.11 | 0.1497 |      - |     - |     944 B |
|      ManyPropertyTokenTemplate |   657.5 ns | 11.73 ns |  10.97 ns |  2.72 |    0.10 | 0.1698 |      - |     - |    1072 B |
|         MultipleTokensTemplate | 1,296.7 ns | 25.77 ns |  27.57 ns |  5.36 |    0.14 | 0.2918 |      - |     - |    1840 B |
|   DefaultConsoleOutputTemplate | 1,760.8 ns | 23.83 ns |  22.29 ns |  7.27 |    0.19 | 0.3643 |      - |     - |    2296 B |
|                    BigTemplate | 4,756.0 ns | 82.57 ns | 121.03 ns | 19.64 |    0.59 | 1.0300 | 0.0076 |     - |    6496 B |
