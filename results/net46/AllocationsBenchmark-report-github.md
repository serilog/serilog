``` ini

BenchmarkDotNet=v0.10.6, OS=Windows 10 Redstone 1 (10.0.14393)
Processor=Intel Core i7-4790 CPU 3.60GHz (Haswell), ProcessorCount=8
Frequency=3507500 Hz, Resolution=285.1033 ns, Timer=TSC
  [Host]     : Clr 4.0.30319.42000, 32bit LegacyJIT-v4.7.2053.0
  DefaultJob : Clr 4.0.30319.42000, 32bit LegacyJIT-v4.7.2053.0


```
 |               Method |         Mean |      Error |     StdDev | Scaled | ScaledSD |  Gen 0 | Allocated |
 |--------------------- |-------------:|-----------:|-----------:|-------:|---------:|-------:|----------:|
 |             LogEmpty |     9.749 ns |  0.0346 ns |  0.0324 ns |   1.00 |     0.00 |      - |       0 B |
 | LogEmptyWithEnricher |   103.460 ns |  0.1742 ns |  0.1629 ns |  10.61 |     0.04 | 0.0066 |      28 B |
 |            LogScalar |   478.723 ns |  0.6996 ns |  0.6201 ns |  49.11 |     0.17 | 0.0591 |     248 B |
 |        LogDictionary | 3,867.137 ns | 13.5751 ns | 12.6982 ns | 396.67 |     1.79 | 0.3128 |    1324 B |
 |          LogSequence | 1,309.241 ns |  1.4345 ns |  1.3418 ns | 134.30 |     0.45 | 0.1144 |     484 B |
 |         LogAnonymous | 6,128.421 ns | 11.3529 ns | 10.6195 ns | 628.62 |     2.28 | 0.4654 |    1960 B |
