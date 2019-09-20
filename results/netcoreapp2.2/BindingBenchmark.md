``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-7700HQ CPU 2.80GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=2.2.401
  [Host]     : .NET Core 2.2.7 (CoreCLR 4.6.28008.02, CoreFX 4.6.28008.03), 64bit RyuJIT
  DefaultJob : .NET Core 2.2.7 (CoreCLR 4.6.28008.02, CoreFX 4.6.28008.03), 64bit RyuJIT


```
|   Method |      Mean |      Error |     StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------- |----------:|-----------:|-----------:|------:|--------:|-------:|------:|------:|----------:|
| BindZero |  68.46 ns |  1.4471 ns |  3.3826 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|  BindOne | 204.54 ns |  0.9218 ns |  0.8622 ns |  2.85 |    0.10 | 0.0455 |     - |     - |     144 B |
| BindFive | 542.71 ns | 12.0323 ns | 19.0845 ns |  7.85 |    0.45 | 0.1364 |     - |     - |     432 B |
