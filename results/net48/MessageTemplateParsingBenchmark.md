``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.450 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4200.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8 (4.8.4200.0), X86 LegacyJIT


```
|                         Method |       Mean |     Error |   StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------------------- |-----------:|----------:|---------:|------:|--------:|-------:|------:|------:|----------:|
|                  EmptyTemplate |   122.4 ns |   2.45 ns |  3.01 ns |  1.00 |    0.00 | 0.0267 |     - |     - |     140 B |
|             SimpleTextTemplate |   185.3 ns |   3.63 ns |  3.40 ns |  1.51 |    0.05 | 0.0479 |     - |     - |     252 B |
|    SinglePropertyTokenTemplate |   310.5 ns |   6.22 ns |  7.17 ns |  2.54 |    0.09 | 0.0625 |     - |     - |     328 B |
| SingleTextWithPropertyTemplate |   535.0 ns |  10.53 ns | 12.54 ns |  4.38 |    0.16 | 0.1183 |     - |     - |     625 B |
|      ManyPropertyTokenTemplate |   651.9 ns |  12.93 ns | 13.28 ns |  5.33 |    0.21 | 0.1259 |     - |     - |     665 B |
|         MultipleTokensTemplate | 1,322.3 ns |  25.31 ns | 23.68 ns | 10.79 |    0.23 | 0.2213 |     - |     - |    1166 B |
|   DefaultConsoleOutputTemplate | 1,889.3 ns |  37.23 ns | 47.08 ns | 15.47 |    0.57 | 0.2785 |     - |     - |    1478 B |
|                    BigTemplate | 5,205.2 ns | 104.10 ns | 97.37 ns | 42.47 |    1.20 | 0.7935 |     - |     - |    4182 B |
