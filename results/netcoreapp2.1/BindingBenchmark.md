``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.450 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.401
  [Host]     : .NET Core 2.1.21 (CoreCLR 4.6.29130.01, CoreFX 4.6.29130.02), X64 RyuJIT
  DefaultJob : .NET Core 2.1.21 (CoreCLR 4.6.29130.01, CoreFX 4.6.29130.02), X64 RyuJIT


```
|   Method |      Mean |    Error |    StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------- |----------:|---------:|----------:|------:|--------:|-------:|------:|------:|----------:|
| BindZero |  59.45 ns | 1.213 ns |  2.952 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|  BindOne | 189.18 ns | 3.760 ns |  4.179 ns |  3.16 |    0.18 | 0.0226 |     - |     - |     144 B |
| BindFive | 472.49 ns | 9.378 ns | 13.146 ns |  7.92 |    0.48 | 0.0677 |     - |     - |     432 B |
