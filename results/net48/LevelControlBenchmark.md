``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-7700HQ CPU 2.80GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
  [Host]     : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.8.4010.0
  DefaultJob : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.8.4010.0


```
|         Method |      Mean |     Error |    StdDev | Ratio | RatioSD |
|--------------- |----------:|----------:|----------:|------:|--------:|
|            Off |  2.927 ns | 0.0152 ns | 0.0135 ns |  1.00 |    0.00 |
| LevelSwitchOff |  3.229 ns | 0.1210 ns | 0.1189 ns |  1.10 |    0.04 |
| MinimumLevelOn | 10.834 ns | 0.0585 ns | 0.0547 ns |  3.70 |    0.03 |
|  LevelSwitchOn | 10.361 ns | 0.0636 ns | 0.0564 ns |  3.54 |    0.03 |
