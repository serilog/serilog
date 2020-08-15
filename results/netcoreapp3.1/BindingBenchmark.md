``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.450 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.401
  [Host]     : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT
  DefaultJob : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT


```
|   Method |      Mean |    Error |   StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------- |----------:|---------:|---------:|------:|--------:|-------:|------:|------:|----------:|
| BindZero |  43.90 ns | 0.793 ns | 0.741 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|  BindOne | 160.74 ns | 3.115 ns | 2.914 ns |  3.66 |    0.10 | 0.0229 |     - |     - |     144 B |
| BindFive | 456.73 ns | 5.265 ns | 4.925 ns | 10.41 |    0.20 | 0.0687 |     - |     - |     432 B |
