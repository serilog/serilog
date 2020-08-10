``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.388 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.302
  [Host]     : .NET Core 2.1.20 (CoreCLR 4.6.29017.01, CoreFX 4.6.29018.12), X64 RyuJIT
  DefaultJob : .NET Core 2.1.20 (CoreCLR 4.6.29017.01, CoreFX 4.6.29018.12), X64 RyuJIT


```
|   Method |      Mean |     Error |   StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------- |----------:|----------:|---------:|------:|--------:|-------:|------:|------:|----------:|
| BindZero |  57.88 ns |  1.114 ns | 0.931 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|  BindOne | 191.22 ns |  3.773 ns | 3.529 ns |  3.30 |    0.08 | 0.0226 |     - |     - |     144 B |
| BindFive | 511.03 ns | 10.129 ns | 9.475 ns |  8.83 |    0.20 | 0.0677 |     - |     - |     432 B |
