``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-7700HQ CPU 2.80GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
  [Host]     : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.8.4010.0
  DefaultJob : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.8.4010.0


```
|                       Method |       Mean |     Error |     StdDev |     Median | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|----------------------------- |-----------:|----------:|-----------:|-----------:|------:|--------:|-------:|------:|------:|----------:|
|                EmptyTemplate |   162.9 ns |  1.159 ns |  0.9679 ns |   162.3 ns |  1.00 |    0.00 | 0.0482 |     - |     - |     152 B |
|           SimpleTextTemplate |   241.0 ns |  4.842 ns |  7.6800 ns |   237.9 ns |  1.50 |    0.06 | 0.0839 |     - |     - |     264 B |
|  SinglePropertyTokenTemplate |   370.9 ns |  2.988 ns |  2.7948 ns |   371.6 ns |  2.28 |    0.02 | 0.1040 |     - |     - |     328 B |
|    ManyPropertyTokenTemplate |   808.5 ns | 16.030 ns | 27.2196 ns |   789.8 ns |  4.93 |    0.17 | 0.2108 |     - |     - |     665 B |
|       MultipleTokensTemplate | 1,620.6 ns | 13.624 ns | 12.0775 ns | 1,618.5 ns |  9.95 |    0.10 | 0.3700 |     - |     - |    1166 B |
| DefaultConsoleOutputTemplate | 2,230.4 ns | 14.438 ns | 13.5057 ns | 2,229.7 ns | 13.69 |    0.12 | 0.4692 |     - |     - |    1478 B |
