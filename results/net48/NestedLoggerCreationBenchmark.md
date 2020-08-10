``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.388 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT


```
|           Method |      Mean |    Error |   StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|----------------- |----------:|---------:|---------:|-------:|------:|------:|----------:|
|    ForContextInt |  77.02 ns | 1.498 ns | 1.603 ns | 0.0144 |     - |     - |      76 B |
| ForContextString |  49.50 ns | 0.757 ns | 0.708 ns | 0.0122 |     - |     - |      64 B |
|   ForContextType | 101.74 ns | 1.918 ns | 1.970 ns | 0.0122 |     - |     - |      64 B |
