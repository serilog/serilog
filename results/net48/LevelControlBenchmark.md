``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT


```
|         Method |      Mean |     Error |    StdDev | Ratio | RatioSD |
|--------------- |----------:|----------:|----------:|------:|--------:|
|            Off |  2.650 ns | 0.0543 ns | 0.0481 ns |  1.00 |    0.00 |
| LevelSwitchOff |  3.099 ns | 0.0899 ns | 0.1290 ns |  1.17 |    0.04 |
| MinimumLevelOn | 10.312 ns | 0.2342 ns | 0.2300 ns |  3.89 |    0.14 |
|  LevelSwitchOn |  9.940 ns | 0.2267 ns | 0.2226 ns |  3.75 |    0.09 |
