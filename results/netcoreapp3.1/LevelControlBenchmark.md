``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.302
  [Host]     : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT
  DefaultJob : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT


```
|         Method |      Mean |     Error |    StdDev | Ratio | RatioSD |
|--------------- |----------:|----------:|----------:|------:|--------:|
|            Off |  2.656 ns | 0.0758 ns | 0.0709 ns |  1.00 |    0.00 |
| LevelSwitchOff |  2.902 ns | 0.0818 ns | 0.0840 ns |  1.09 |    0.06 |
| MinimumLevelOn | 10.218 ns | 0.1644 ns | 0.1538 ns |  3.85 |    0.12 |
|  LevelSwitchOn |  9.901 ns | 0.1899 ns | 0.1683 ns |  3.74 |    0.10 |
