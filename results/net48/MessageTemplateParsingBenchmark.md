``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT


```
|                         Method |       Mean |     Error |    StdDev |     Median | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------------------- |-----------:|----------:|----------:|-----------:|------:|--------:|-------:|------:|------:|----------:|
|                  EmptyTemplate |   149.7 ns |   1.01 ns |   0.94 ns |   149.8 ns |  1.00 |    0.00 | 0.0288 |     - |     - |     152 B |
|             SimpleTextTemplate |   232.8 ns |   1.46 ns |   1.29 ns |   233.0 ns |  1.55 |    0.01 | 0.0503 |     - |     - |     264 B |
|    SinglePropertyTokenTemplate |   362.7 ns |   2.53 ns |   2.36 ns |   363.1 ns |  2.42 |    0.02 | 0.0625 |     - |     - |     328 B |
| SingleTextWithPropertyTemplate |   608.3 ns |   4.61 ns |   4.32 ns |   610.0 ns |  4.07 |    0.04 | 0.1183 |     - |     - |     625 B |
|      ManyPropertyTokenTemplate |   742.1 ns |   6.99 ns |   6.54 ns |   741.8 ns |  4.96 |    0.06 | 0.1259 |     - |     - |     665 B |
|         MultipleTokensTemplate | 1,553.0 ns |  36.49 ns |  91.53 ns | 1,515.7 ns | 10.82 |    1.01 | 0.2213 |     - |     - |    1166 B |
|   DefaultConsoleOutputTemplate | 2,178.9 ns |  22.14 ns |  19.63 ns | 2,175.6 ns | 14.55 |    0.18 | 0.2785 |     - |     - |    1478 B |
|                    BigTemplate | 6,063.7 ns | 120.76 ns | 188.01 ns | 6,029.6 ns | 41.12 |    1.48 | 0.7935 |     - |     - |    4182 B |
