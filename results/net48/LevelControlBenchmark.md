``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.450 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4200.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8 (4.8.4200.0), X86 LegacyJIT


```
|         Method |     Mean |     Error |    StdDev | Ratio | RatioSD |
|--------------- |---------:|----------:|----------:|------:|--------:|
|            Off | 2.931 ns | 0.0837 ns | 0.1028 ns |  1.00 |    0.00 |
| LevelSwitchOff | 3.420 ns | 0.0731 ns | 0.0684 ns |  1.17 |    0.06 |
| MinimumLevelOn | 9.696 ns | 0.2033 ns | 0.1902 ns |  3.32 |    0.14 |
|  LevelSwitchOn | 9.717 ns | 0.1833 ns | 0.1714 ns |  3.33 |    0.13 |
