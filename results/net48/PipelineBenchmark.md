``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT


```
|               Method |         Mean |     Error |    StdDev |  Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |-------------:|----------:|----------:|-------:|--------:|-------:|------:|------:|----------:|
| EmitLogAIgnoredEvent |     9.079 ns | 0.0739 ns | 0.0692 ns |   1.00 |    0.00 |      - |     - |     - |         - |
|         EmitLogEvent | 1,639.379 ns | 9.6276 ns | 9.0056 ns | 180.58 |    1.63 | 0.0401 |     - |     - |     216 B |
