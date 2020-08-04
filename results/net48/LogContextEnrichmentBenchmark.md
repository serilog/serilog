``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT


```
|               Method |       Mean |     Error |    StdDev | Ratio | RatioSD |
|--------------------- |-----------:|----------:|----------:|------:|--------:|
|                 Bare |   9.504 ns | 0.1937 ns | 0.1812 ns |  1.00 |    0.00 |
|         PushProperty |  76.474 ns | 1.4893 ns | 1.5935 ns |  8.05 |    0.20 |
|   PushPropertyNested | 153.467 ns | 3.0313 ns | 2.9771 ns | 16.14 |    0.35 |
| PushPropertyEnriched | 159.110 ns | 2.4516 ns | 2.2932 ns | 16.75 |    0.38 |
