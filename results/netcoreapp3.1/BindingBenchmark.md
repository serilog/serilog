``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.301
  [Host]     : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT
  DefaultJob : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT


```
|   Method |      Mean |    Error |   StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------- |----------:|---------:|---------:|------:|--------:|-------:|------:|------:|----------:|
| BindZero |  42.30 ns | 0.738 ns | 0.690 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|  BindOne | 149.52 ns | 3.005 ns | 3.691 ns |  3.54 |    0.12 | 0.0229 |     - |     - |     144 B |
| BindFive | 417.51 ns | 5.779 ns | 5.405 ns |  9.87 |    0.25 | 0.0687 |     - |     - |     432 B |
