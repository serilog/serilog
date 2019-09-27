``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=2.2.402
  [Host]     : .NET Core 2.2.7 (CoreCLR 4.6.28008.02, CoreFX 4.6.28008.03), 64bit RyuJIT
  DefaultJob : .NET Core 2.2.7 (CoreCLR 4.6.28008.02, CoreFX 4.6.28008.03), 64bit RyuJIT


```
|   Method |      Mean |     Error |    StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------- |----------:|----------:|----------:|------:|--------:|-------:|------:|------:|----------:|
| BindZero |  51.91 ns | 0.1285 ns | 0.1139 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|  BindOne | 168.36 ns | 0.8081 ns | 0.7559 ns |  3.24 |    0.02 | 0.0226 |     - |     - |     144 B |
| BindFive | 445.17 ns | 2.7667 ns | 2.5879 ns |  8.58 |    0.04 | 0.0682 |     - |     - |     432 B |
