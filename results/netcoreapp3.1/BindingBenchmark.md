``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.302
  [Host]     : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT
  DefaultJob : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT


```
|   Method |      Mean |    Error |   StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------- |----------:|---------:|---------:|------:|--------:|-------:|------:|------:|----------:|
| BindZero |  42.45 ns | 0.611 ns | 0.571 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|  BindOne | 149.93 ns | 1.069 ns | 1.000 ns |  3.53 |    0.05 | 0.0229 |     - |     - |     144 B |
| BindFive | 420.51 ns | 7.102 ns | 6.643 ns |  9.91 |    0.22 | 0.0687 |     - |     - |     432 B |
