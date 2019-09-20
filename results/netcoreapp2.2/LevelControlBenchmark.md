``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-7700HQ CPU 2.80GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=2.2.401
  [Host]     : .NET Core 2.2.7 (CoreCLR 4.6.28008.02, CoreFX 4.6.28008.03), 64bit RyuJIT
  DefaultJob : .NET Core 2.2.7 (CoreCLR 4.6.28008.02, CoreFX 4.6.28008.03), 64bit RyuJIT


```
|         Method |      Mean |     Error |    StdDev | Ratio | RatioSD |
|--------------- |----------:|----------:|----------:|------:|--------:|
|            Off |  2.591 ns | 0.0320 ns | 0.0299 ns |  1.00 |    0.00 |
| LevelSwitchOff |  3.166 ns | 0.0227 ns | 0.0212 ns |  1.22 |    0.02 |
| MinimumLevelOn | 11.046 ns | 0.0694 ns | 0.0580 ns |  4.26 |    0.05 |
|  LevelSwitchOn | 10.693 ns | 0.0361 ns | 0.0320 ns |  4.13 |    0.06 |
