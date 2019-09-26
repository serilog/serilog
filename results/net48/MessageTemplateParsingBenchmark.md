``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.8.4010.0
  DefaultJob : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.8.4010.0


```
|                       Method |       Mean |     Error |    StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|----------------------------- |-----------:|----------:|----------:|------:|--------:|-------:|------:|------:|----------:|
|                EmptyTemplate |   137.3 ns |  1.071 ns |  1.002 ns |  1.00 |    0.00 | 0.0288 |     - |     - |     152 B |
|           SimpleTextTemplate |   200.5 ns |  1.110 ns |  1.039 ns |  1.46 |    0.01 | 0.0503 |     - |     - |     264 B |
|  SinglePropertyTokenTemplate |   324.2 ns |  3.736 ns |  3.495 ns |  2.36 |    0.04 | 0.0625 |     - |     - |     328 B |
|    ManyPropertyTokenTemplate |   707.5 ns | 12.266 ns | 10.874 ns |  5.16 |    0.08 | 0.1259 |     - |     - |     665 B |
|       MultipleTokensTemplate | 1,413.5 ns |  8.378 ns |  7.837 ns | 10.29 |    0.09 | 0.2213 |     - |     - |    1166 B |
| DefaultConsoleOutputTemplate | 1,969.3 ns |  7.846 ns |  7.340 ns | 14.34 |    0.11 | 0.2785 |     - |     - |    1478 B |
