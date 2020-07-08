``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.301
  [Host]     : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT
  DefaultJob : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT


```
|         Method |      Mean |     Error |    StdDev | Ratio | RatioSD |
|--------------- |----------:|----------:|----------:|------:|--------:|
|            Off |  2.359 ns | 0.0726 ns | 0.0679 ns |  1.00 |    0.00 |
| LevelSwitchOff |  3.457 ns | 0.0952 ns | 0.1170 ns |  1.46 |    0.08 |
| MinimumLevelOn | 10.290 ns | 0.1835 ns | 0.1716 ns |  4.36 |    0.12 |
|  LevelSwitchOn | 10.065 ns | 0.2019 ns | 0.1888 ns |  4.27 |    0.16 |
