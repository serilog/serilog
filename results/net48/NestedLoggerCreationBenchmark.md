``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-7700HQ CPU 2.80GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
  [Host]     : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.8.4010.0
  DefaultJob : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.8.4010.0


```
|           Method |      Mean |     Error |    StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|----------------- |----------:|----------:|----------:|-------:|------:|------:|----------:|
|    ForContextInt |  83.02 ns | 2.2227 ns | 3.1159 ns | 0.0241 |     - |     - |      76 B |
| ForContextString |  47.88 ns | 0.2826 ns | 0.2505 ns | 0.0203 |     - |     - |      64 B |
|   ForContextType | 118.37 ns | 2.3494 ns | 3.4437 ns | 0.0203 |     - |     - |      64 B |
