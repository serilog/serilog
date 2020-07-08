``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT


```
|           Method |      Mean |    Error |   StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|----------------- |----------:|---------:|---------:|-------:|------:|------:|----------:|
|    ForContextInt |  77.94 ns | 0.961 ns | 0.899 ns | 0.0144 |     - |     - |      76 B |
| ForContextString |  46.64 ns | 0.620 ns | 0.580 ns | 0.0122 |     - |     - |      64 B |
|   ForContextType | 100.74 ns | 1.161 ns | 1.086 ns | 0.0122 |     - |     - |      64 B |
