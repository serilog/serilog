``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.450 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.401
  [Host]     : .NET Core 2.1.21 (CoreCLR 4.6.29130.01, CoreFX 4.6.29130.02), X64 RyuJIT
  DefaultJob : .NET Core 2.1.21 (CoreCLR 4.6.29130.01, CoreFX 4.6.29130.02), X64 RyuJIT


```
|         Method |      Mean |     Error |    StdDev | Ratio | RatioSD |
|--------------- |----------:|----------:|----------:|------:|--------:|
|            Off |  2.581 ns | 0.0780 ns | 0.0898 ns |  1.00 |    0.00 |
| LevelSwitchOff |  2.844 ns | 0.0836 ns | 0.0895 ns |  1.10 |    0.05 |
| MinimumLevelOn | 10.952 ns | 0.2422 ns | 0.2378 ns |  4.25 |    0.18 |
|  LevelSwitchOn | 10.750 ns | 0.2387 ns | 0.2233 ns |  4.16 |    0.18 |
