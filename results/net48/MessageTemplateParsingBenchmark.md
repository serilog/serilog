``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT


```
|                         Method |       Mean |     Error |    StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------------------- |-----------:|----------:|----------:|------:|--------:|-------:|------:|------:|----------:|
|                  EmptyTemplate |   156.4 ns |   3.06 ns |   4.29 ns |  1.00 |    0.00 | 0.0288 |     - |     - |     152 B |
|             SimpleTextTemplate |   240.5 ns |   3.75 ns |   3.51 ns |  1.52 |    0.06 | 0.0501 |     - |     - |     264 B |
|    SinglePropertyTokenTemplate |   364.5 ns |   7.23 ns |  11.87 ns |  2.35 |    0.08 | 0.0625 |     - |     - |     328 B |
| SingleTextWithPropertyTemplate |   618.3 ns |  12.01 ns |  11.80 ns |  3.93 |    0.15 | 0.1183 |     - |     - |     625 B |
|      ManyPropertyTokenTemplate |   754.0 ns |  13.63 ns |  12.08 ns |  4.77 |    0.15 | 0.1259 |     - |     - |     665 B |
|         MultipleTokensTemplate | 1,500.7 ns |  17.29 ns |  16.17 ns |  9.51 |    0.30 | 0.2213 |     - |     - |    1166 B |
|   DefaultConsoleOutputTemplate | 2,199.8 ns |  42.38 ns |  55.11 ns | 14.05 |    0.45 | 0.2785 |     - |     - |    1478 B |
|                    BigTemplate | 5,942.7 ns | 113.26 ns | 116.31 ns | 37.76 |    1.26 | 0.7935 |     - |     - |    4182 B |
