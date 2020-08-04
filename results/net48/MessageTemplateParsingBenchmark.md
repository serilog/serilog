``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT


```
|                         Method |       Mean |     Error |    StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------------------- |-----------:|----------:|----------:|------:|--------:|-------:|------:|------:|----------:|
|                  EmptyTemplate |   139.7 ns |   2.79 ns |   3.32 ns |  1.00 |    0.00 | 0.0288 |     - |     - |     152 B |
|             SimpleTextTemplate |   219.1 ns |   2.95 ns |   2.75 ns |  1.57 |    0.04 | 0.0503 |     - |     - |     264 B |
|    SinglePropertyTokenTemplate |   327.4 ns |   6.38 ns |   6.26 ns |  2.34 |    0.08 | 0.0625 |     - |     - |     328 B |
| SingleTextWithPropertyTemplate |   561.2 ns |  10.96 ns |  11.25 ns |  4.01 |    0.06 | 0.1183 |     - |     - |     625 B |
|      ManyPropertyTokenTemplate |   697.0 ns |  13.04 ns |  12.20 ns |  4.98 |    0.16 | 0.1259 |     - |     - |     665 B |
|         MultipleTokensTemplate | 1,388.8 ns |  22.45 ns |  21.00 ns |  9.93 |    0.34 | 0.2213 |     - |     - |    1166 B |
|   DefaultConsoleOutputTemplate | 1,985.6 ns |  38.19 ns |  45.47 ns | 14.22 |    0.46 | 0.2785 |     - |     - |    1478 B |
|                    BigTemplate | 5,446.4 ns | 101.90 ns | 100.08 ns | 38.97 |    1.25 | 0.7935 |     - |     - |    4182 B |
