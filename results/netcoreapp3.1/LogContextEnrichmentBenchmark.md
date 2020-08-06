``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.388 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.302
  [Host]     : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT
  DefaultJob : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT


```
|               Method |       Mean |     Error |    StdDev | Ratio | RatioSD |
|--------------------- |-----------:|----------:|----------:|------:|--------:|
|                 Bare |   9.754 ns | 0.1669 ns | 0.1561 ns |  1.00 |    0.00 |
|         PushProperty |  90.014 ns | 1.5474 ns | 1.4475 ns |  9.23 |    0.24 |
|   PushPropertyNested | 182.868 ns | 2.4971 ns | 2.3357 ns | 18.75 |    0.46 |
| PushPropertyEnriched | 160.593 ns | 3.0469 ns | 3.2602 ns | 16.46 |    0.43 |
