``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.388 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT


```
|                         Method |       Mean |     Error |    StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------------------- |-----------:|----------:|----------:|------:|--------:|-------:|------:|------:|----------:|
|                  EmptyTemplate |   148.0 ns |   2.88 ns |   3.54 ns |  1.00 |    0.00 | 0.0267 |     - |     - |     140 B |
|             SimpleTextTemplate |   219.1 ns |   3.44 ns |   3.21 ns |  1.48 |    0.05 | 0.0479 |     - |     - |     252 B |
|    SinglePropertyTokenTemplate |   343.0 ns |   6.82 ns |   6.69 ns |  2.32 |    0.08 | 0.0625 |     - |     - |     328 B |
| SingleTextWithPropertyTemplate |   550.1 ns |  10.96 ns |  12.62 ns |  3.72 |    0.08 | 0.1183 |     - |     - |     625 B |
|      ManyPropertyTokenTemplate |   690.2 ns |  13.66 ns |  13.41 ns |  4.66 |    0.16 | 0.1259 |     - |     - |     665 B |
|         MultipleTokensTemplate | 1,359.1 ns |  25.02 ns |  23.40 ns |  9.18 |    0.31 | 0.2213 |     - |     - |    1166 B |
|   DefaultConsoleOutputTemplate | 1,945.0 ns |  25.38 ns |  23.74 ns | 13.14 |    0.43 | 0.2804 |     - |     - |    1478 B |
|                    BigTemplate | 5,322.1 ns | 104.76 ns | 102.89 ns | 35.93 |    1.08 | 0.7935 |     - |     - |    4182 B |
