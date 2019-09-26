``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.8.4010.0
  DefaultJob : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.8.4010.0


```
|         Method |     Mean |     Error |    StdDev | Ratio | RatioSD |
|--------------- |---------:|----------:|----------:|------:|--------:|
|            Off | 2.525 ns | 0.0283 ns | 0.0265 ns |  1.00 |    0.00 |
| LevelSwitchOff | 4.468 ns | 0.1415 ns | 0.1573 ns |  1.77 |    0.07 |
| MinimumLevelOn | 9.301 ns | 0.0720 ns | 0.0673 ns |  3.68 |    0.05 |
|  LevelSwitchOn | 9.125 ns | 0.1512 ns | 0.1415 ns |  3.61 |    0.06 |
