``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.388 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT


```
|                         Method |       Mean |     Error |    StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------------------- |-----------:|----------:|----------:|------:|--------:|-------:|------:|------:|----------:|
|                  EmptyTemplate |   142.5 ns |   2.87 ns |   3.42 ns |  1.00 |    0.00 | 0.0288 |     - |     - |     152 B |
|             SimpleTextTemplate |   207.8 ns |   3.23 ns |   3.02 ns |  1.46 |    0.04 | 0.0503 |     - |     - |     264 B |
|    SinglePropertyTokenTemplate |   323.0 ns |   5.10 ns |   4.77 ns |  2.27 |    0.08 | 0.0625 |     - |     - |     328 B |
| SingleTextWithPropertyTemplate |   538.8 ns |   9.98 ns |   9.34 ns |  3.78 |    0.15 | 0.1183 |     - |     - |     625 B |
|      ManyPropertyTokenTemplate |   690.1 ns |  12.06 ns |  11.28 ns |  4.84 |    0.12 | 0.1259 |     - |     - |     665 B |
|         MultipleTokensTemplate | 1,381.7 ns |  21.24 ns |  19.87 ns |  9.69 |    0.27 | 0.2213 |     - |     - |    1166 B |
|   DefaultConsoleOutputTemplate | 2,026.9 ns |  39.53 ns |  52.77 ns | 14.24 |    0.46 | 0.2785 |     - |     - |    1478 B |
|                    BigTemplate | 5,393.4 ns | 106.68 ns | 104.77 ns | 37.92 |    1.04 | 0.7935 |     - |     - |    4182 B |
