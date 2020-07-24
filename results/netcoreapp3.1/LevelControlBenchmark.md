``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.302
  [Host]     : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT
  DefaultJob : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT


```
|         Method |      Mean |     Error |    StdDev | Ratio | RatioSD |
|--------------- |----------:|----------:|----------:|------:|--------:|
|            Off |  2.372 ns | 0.0258 ns | 0.0216 ns |  1.00 |    0.00 |
| LevelSwitchOff |  3.382 ns | 0.0336 ns | 0.0314 ns |  1.43 |    0.02 |
| MinimumLevelOn | 10.126 ns | 0.0938 ns | 0.0877 ns |  4.27 |    0.06 |
|  LevelSwitchOn | 10.338 ns | 0.0847 ns | 0.0751 ns |  4.36 |    0.06 |
