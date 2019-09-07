``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-7700HQ CPU 2.80GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=2.2.401
  [Host]     : .NET Core 2.2.6 (CoreCLR 4.6.27817.03, CoreFX 4.6.27818.02), 64bit RyuJIT
  DefaultJob : .NET Core 2.2.6 (CoreCLR 4.6.27817.03, CoreFX 4.6.27818.02), 64bit RyuJIT


```
|         Method |      Mean |     Error |    StdDev |    Median | Ratio | RatioSD |
|--------------- |----------:|----------:|----------:|----------:|------:|--------:|
|            Off |  2.767 ns | 0.0869 ns | 0.1353 ns |  2.814 ns |  1.00 |    0.00 |
| LevelSwitchOff |  3.305 ns | 0.0971 ns | 0.1297 ns |  3.358 ns |  1.19 |    0.10 |
| MinimumLevelOn | 11.944 ns | 0.2999 ns | 0.7011 ns | 11.702 ns |  4.45 |    0.34 |
|  LevelSwitchOn | 11.260 ns | 0.2509 ns | 0.2889 ns | 11.183 ns |  4.06 |    0.24 |
