``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT


```
|         Method |     Mean |     Error |    StdDev | Ratio | RatioSD |
|--------------- |---------:|----------:|----------:|------:|--------:|
|            Off | 2.718 ns | 0.0481 ns | 0.0427 ns |  1.00 |    0.00 |
| LevelSwitchOff | 2.921 ns | 0.0602 ns | 0.0563 ns |  1.08 |    0.02 |
| MinimumLevelOn | 9.942 ns | 0.0555 ns | 0.0519 ns |  3.66 |    0.06 |
|  LevelSwitchOn | 9.706 ns | 0.0960 ns | 0.0898 ns |  3.57 |    0.06 |
