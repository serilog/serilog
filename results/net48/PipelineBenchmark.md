``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.8.4010.0
  DefaultJob : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.8.4010.0


```
|               Method |         Mean |     Error |    StdDev |  Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |-------------:|----------:|----------:|-------:|--------:|-------:|------:|------:|----------:|
| EmitLogAIgnoredEvent |     8.943 ns | 0.0541 ns | 0.0506 ns |   1.00 |    0.00 |      - |     - |     - |         - |
|         EmitLogEvent | 1,478.114 ns | 9.4690 ns | 8.8573 ns | 165.29 |    1.17 | 0.0401 |     - |     - |     216 B |
