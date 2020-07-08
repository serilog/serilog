``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.301
  [Host]     : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT
  DefaultJob : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT


```
|   Method |      Mean |    Error |   StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------- |----------:|---------:|---------:|------:|--------:|-------:|------:|------:|----------:|
| BindZero |  45.31 ns | 0.575 ns | 0.538 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|  BindOne | 165.28 ns | 2.203 ns | 2.061 ns |  3.65 |    0.08 | 0.0229 |     - |     - |     144 B |
| BindFive | 458.25 ns | 5.807 ns | 5.148 ns | 10.12 |    0.17 | 0.0687 |     - |     - |     432 B |
