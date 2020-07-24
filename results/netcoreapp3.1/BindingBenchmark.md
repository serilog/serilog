``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.302
  [Host]     : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT
  DefaultJob : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT


```
|   Method |      Mean |    Error |   StdDev |    Median | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------- |----------:|---------:|---------:|----------:|------:|--------:|-------:|------:|------:|----------:|
| BindZero |  45.71 ns | 0.936 ns | 1.217 ns |  45.73 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|  BindOne | 166.84 ns | 4.325 ns | 8.332 ns | 163.40 ns |  3.73 |    0.22 | 0.0229 |     - |     - |     144 B |
| BindFive | 464.56 ns | 6.333 ns | 5.924 ns | 463.83 ns | 10.21 |    0.36 | 0.0687 |     - |     - |     432 B |
