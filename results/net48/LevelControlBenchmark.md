``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT


```
|         Method |     Mean |     Error |    StdDev | Ratio | RatioSD |
|--------------- |---------:|----------:|----------:|------:|--------:|
|            Off | 2.551 ns | 0.0776 ns | 0.0726 ns |  1.00 |    0.00 |
| LevelSwitchOff | 2.835 ns | 0.0833 ns | 0.0926 ns |  1.11 |    0.04 |
| MinimumLevelOn | 9.731 ns | 0.2218 ns | 0.2723 ns |  3.85 |    0.16 |
|  LevelSwitchOn | 9.847 ns | 0.2146 ns | 0.2472 ns |  3.89 |    0.17 |
