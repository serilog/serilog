``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.388 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.302
  [Host]     : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT
  DefaultJob : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT


```
|   Method |      Mean |    Error |   StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------- |----------:|---------:|---------:|------:|--------:|-------:|------:|------:|----------:|
| BindZero |  42.47 ns | 0.705 ns | 0.659 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|  BindOne | 150.99 ns | 3.057 ns | 4.848 ns |  3.57 |    0.15 | 0.0229 |     - |     - |     144 B |
| BindFive | 422.89 ns | 5.209 ns | 4.873 ns |  9.96 |    0.16 | 0.0687 |     - |     - |     432 B |
