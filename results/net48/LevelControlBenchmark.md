``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-7700HQ CPU 2.80GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
  [Host]     : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.8.3815.0
  DefaultJob : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.8.3815.0


```
|         Method |      Mean |     Error |    StdDev | Ratio | RatioSD |
|--------------- |----------:|----------:|----------:|------:|--------:|
|            Off |  3.133 ns | 0.0934 ns | 0.1508 ns |  1.00 |    0.00 |
| LevelSwitchOff |  3.294 ns | 0.0984 ns | 0.1503 ns |  1.06 |    0.07 |
| MinimumLevelOn | 11.365 ns | 0.2552 ns | 0.3038 ns |  3.67 |    0.18 |
|  LevelSwitchOn | 10.809 ns | 0.2224 ns | 0.2080 ns |  3.51 |    0.17 |
