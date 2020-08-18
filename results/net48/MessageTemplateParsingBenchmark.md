``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.450 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4200.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8 (4.8.4200.0), X86 LegacyJIT


```
|                         Method |       Mean |     Error |    StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------------------- |-----------:|----------:|----------:|------:|--------:|-------:|------:|------:|----------:|
|                  EmptyTemplate |   146.4 ns |   2.27 ns |   2.12 ns |  1.00 |    0.00 | 0.0288 |     - |     - |     152 B |
|             SimpleTextTemplate |   220.0 ns |   2.76 ns |   2.58 ns |  1.50 |    0.02 | 0.0503 |     - |     - |     264 B |
|    SinglePropertyTokenTemplate |   324.4 ns |   6.05 ns |   6.21 ns |  2.22 |    0.05 | 0.0625 |     - |     - |     328 B |
| SingleTextWithPropertyTemplate |   542.6 ns |  10.40 ns |  12.38 ns |  3.71 |    0.08 | 0.1183 |     - |     - |     625 B |
|      ManyPropertyTokenTemplate |   684.9 ns |  13.03 ns |  12.19 ns |  4.68 |    0.09 | 0.1259 |     - |     - |     665 B |
|         MultipleTokensTemplate | 1,377.9 ns |  24.28 ns |  22.71 ns |  9.41 |    0.19 | 0.2213 |     - |     - |    1166 B |
|   DefaultConsoleOutputTemplate | 1,936.7 ns |  38.33 ns |  54.96 ns | 13.22 |    0.31 | 0.2785 |     - |     - |    1478 B |
|                    BigTemplate | 5,354.9 ns | 103.03 ns | 110.24 ns | 36.65 |    0.87 | 0.7935 |     - |     - |    4182 B |
