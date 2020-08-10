``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.388 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.302
  [Host]     : .NET Core 2.1.20 (CoreCLR 4.6.29017.01, CoreFX 4.6.29018.12), X64 RyuJIT
  DefaultJob : .NET Core 2.1.20 (CoreCLR 4.6.29017.01, CoreFX 4.6.29018.12), X64 RyuJIT


```
|         Method |      Mean |     Error |    StdDev | Ratio | RatioSD |
|--------------- |----------:|----------:|----------:|------:|--------:|
|            Off |  2.648 ns | 0.0653 ns | 0.0611 ns |  1.00 |    0.00 |
| LevelSwitchOff |  2.896 ns | 0.0714 ns | 0.0668 ns |  1.09 |    0.04 |
| MinimumLevelOn | 11.043 ns | 0.2435 ns | 0.2392 ns |  4.18 |    0.16 |
|  LevelSwitchOn | 10.875 ns | 0.1100 ns | 0.0975 ns |  4.12 |    0.10 |
