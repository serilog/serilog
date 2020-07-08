``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT


```
|                         Method |       Mean |    Error |   StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------------------- |-----------:|---------:|---------:|------:|--------:|-------:|------:|------:|----------:|
|                  EmptyTemplate |   158.8 ns |  1.74 ns |  1.63 ns |  1.00 |    0.00 | 0.0288 |     - |     - |     152 B |
|             SimpleTextTemplate |   237.5 ns |  4.44 ns |  4.15 ns |  1.50 |    0.03 | 0.0501 |     - |     - |     264 B |
|    SinglePropertyTokenTemplate |   377.1 ns |  4.93 ns |  4.62 ns |  2.37 |    0.04 | 0.0625 |     - |     - |     328 B |
| SingleTextWithPropertyTemplate |   622.4 ns | 12.29 ns | 11.50 ns |  3.92 |    0.06 | 0.1183 |     - |     - |     625 B |
|      ManyPropertyTokenTemplate |   788.5 ns | 20.45 ns | 27.99 ns |  5.01 |    0.23 | 0.1259 |     - |     - |     665 B |
|         MultipleTokensTemplate | 1,543.6 ns | 29.89 ns | 24.96 ns |  9.73 |    0.18 | 0.2213 |     - |     - |    1166 B |
|   DefaultConsoleOutputTemplate | 2,240.4 ns | 37.80 ns | 33.51 ns | 14.12 |    0.26 | 0.2785 |     - |     - |    1478 B |
|                    BigTemplate | 6,493.3 ns | 93.64 ns | 78.19 ns | 40.92 |    0.38 | 0.7935 |     - |     - |    4182 B |
