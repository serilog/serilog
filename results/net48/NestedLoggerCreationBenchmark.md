``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.450 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4200.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8 (4.8.4200.0), X86 LegacyJIT


```
|           Method |      Mean |    Error |   StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|----------------- |----------:|---------:|---------:|-------:|------:|------:|----------:|
|    ForContextInt |  76.88 ns | 1.560 ns | 1.734 ns | 0.0144 |     - |     - |      76 B |
| ForContextString |  49.00 ns | 0.872 ns | 0.816 ns | 0.0122 |     - |     - |      64 B |
|   ForContextType | 101.76 ns | 1.492 ns | 1.396 ns | 0.0122 |     - |     - |      64 B |
