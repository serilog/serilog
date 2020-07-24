``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT


```
|                         Method |       Mean |    Error |   StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------------------- |-----------:|---------:|---------:|------:|--------:|-------:|------:|------:|----------:|
|                  EmptyTemplate |   151.3 ns |  1.84 ns |  1.63 ns |  1.00 |    0.00 | 0.0288 |     - |     - |     152 B |
|             SimpleTextTemplate |   223.7 ns |  2.03 ns |  1.90 ns |  1.48 |    0.02 | 0.0503 |     - |     - |     264 B |
|    SinglePropertyTokenTemplate |   341.1 ns |  5.26 ns |  4.92 ns |  2.26 |    0.03 | 0.0625 |     - |     - |     328 B |
| SingleTextWithPropertyTemplate |   589.2 ns |  6.63 ns |  5.54 ns |  3.89 |    0.03 | 0.1183 |     - |     - |     625 B |
|      ManyPropertyTokenTemplate |   723.1 ns |  6.29 ns |  5.89 ns |  4.77 |    0.05 | 0.1259 |     - |     - |     665 B |
|         MultipleTokensTemplate | 1,451.5 ns | 13.16 ns | 12.31 ns |  9.59 |    0.08 | 0.2213 |     - |     - |    1166 B |
|   DefaultConsoleOutputTemplate | 2,126.3 ns | 50.29 ns | 61.76 ns | 14.14 |    0.47 | 0.2785 |     - |     - |    1478 B |
|                    BigTemplate | 5,793.0 ns | 73.51 ns | 61.38 ns | 38.26 |    0.73 | 0.7935 |     - |     - |    4182 B |
