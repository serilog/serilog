``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.301
  [Host]     : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT
  DefaultJob : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT


```
|   Method |      Mean |     Error |    StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------- |----------:|----------:|----------:|------:|--------:|-------:|------:|------:|----------:|
| BindZero |  47.04 ns |  1.510 ns |  1.413 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|  BindOne | 163.09 ns |  3.270 ns |  3.893 ns |  3.49 |    0.09 | 0.0229 |     - |     - |     144 B |
| BindFive | 458.03 ns | 10.283 ns | 13.005 ns |  9.83 |    0.43 | 0.0687 |     - |     - |     432 B |
