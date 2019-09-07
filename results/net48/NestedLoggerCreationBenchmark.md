``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-7700HQ CPU 2.80GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
  [Host]     : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.8.3815.0
  DefaultJob : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.8.3815.0


```
|           Method |      Mean |    Error |   StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|----------------- |----------:|---------:|---------:|-------:|------:|------:|----------:|
|    ForContextInt |  85.56 ns | 1.690 ns | 2.369 ns | 0.0241 |     - |     - |      76 B |
| ForContextString |  50.52 ns | 1.021 ns | 1.431 ns | 0.0203 |     - |     - |      64 B |
|   ForContextType | 115.87 ns | 2.313 ns | 4.619 ns | 0.0203 |     - |     - |      64 B |
