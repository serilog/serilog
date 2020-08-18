``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.450 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.401
  [Host]     : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT
  DefaultJob : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT


```
|   Method |      Mean |    Error |   StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------- |----------:|---------:|---------:|------:|--------:|-------:|------:|------:|----------:|
| BindZero |  43.09 ns | 0.850 ns | 0.795 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|  BindOne | 148.16 ns | 1.642 ns | 1.456 ns |  3.43 |    0.07 | 0.0229 |     - |     - |     144 B |
| BindFive | 418.24 ns | 6.639 ns | 6.210 ns |  9.71 |    0.24 | 0.0687 |     - |     - |     432 B |
