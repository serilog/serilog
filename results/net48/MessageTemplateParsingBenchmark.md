``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT


```
|                         Method |       Mean |    Error |   StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------------------- |-----------:|---------:|---------:|------:|--------:|-------:|------:|------:|----------:|
|                  EmptyTemplate |   157.5 ns |  1.79 ns |  1.59 ns |  1.00 |    0.00 | 0.0288 |     - |     - |     152 B |
|             SimpleTextTemplate |   229.2 ns |  4.33 ns |  4.64 ns |  1.45 |    0.04 | 0.0503 |     - |     - |     264 B |
|    SinglePropertyTokenTemplate |   348.9 ns |  6.76 ns |  8.30 ns |  2.22 |    0.06 | 0.0625 |     - |     - |     328 B |
| SingleTextWithPropertyTemplate |   604.0 ns | 10.94 ns |  9.13 ns |  3.83 |    0.04 | 0.1183 |     - |     - |     625 B |
|      ManyPropertyTokenTemplate |   749.9 ns | 14.91 ns | 24.07 ns |  4.84 |    0.16 | 0.1259 |     - |     - |     665 B |
|         MultipleTokensTemplate | 1,487.5 ns | 28.56 ns | 30.56 ns |  9.42 |    0.26 | 0.2213 |     - |     - |    1166 B |
|   DefaultConsoleOutputTemplate | 2,125.1 ns | 42.18 ns | 74.97 ns | 13.82 |    0.40 | 0.2785 |     - |     - |    1478 B |
|                    BigTemplate | 5,682.3 ns | 99.39 ns | 92.97 ns | 36.03 |    0.80 | 0.7935 |     - |     - |    4182 B |
