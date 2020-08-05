``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.388 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.302
  [Host]     : .NET Core 2.1.20 (CoreCLR 4.6.29017.01, CoreFX 4.6.29018.12), X64 RyuJIT
  DefaultJob : .NET Core 2.1.20 (CoreCLR 4.6.29017.01, CoreFX 4.6.29018.12), X64 RyuJIT


```
|   Method |      Mean |    Error |   StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------- |----------:|---------:|---------:|------:|--------:|-------:|------:|------:|----------:|
| BindZero |  56.31 ns | 0.506 ns | 0.449 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|  BindOne | 185.75 ns | 3.411 ns | 3.191 ns |  3.29 |    0.06 | 0.0226 |     - |     - |     144 B |
| BindFive | 470.54 ns | 5.395 ns | 5.046 ns |  8.35 |    0.07 | 0.0682 |     - |     - |     432 B |
