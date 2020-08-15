``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.450 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4200.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8 (4.8.4200.0), X86 LegacyJIT


```
|               Method |         Mean |      Error |     StdDev |  Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |-------------:|-----------:|-----------:|-------:|--------:|-------:|------:|------:|----------:|
| EmitLogAIgnoredEvent |     9.090 ns |  0.1998 ns |  0.2301 ns |   1.00 |    0.00 |      - |     - |     - |         - |
|         EmitLogEvent | 1,534.171 ns | 23.2809 ns | 21.7770 ns | 169.40 |    5.33 | 0.0401 |     - |     - |     216 B |
