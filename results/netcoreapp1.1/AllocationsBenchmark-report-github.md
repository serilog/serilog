``` ini

BenchmarkDotNet=v0.10.6, OS=Windows 10 Redstone 1 (10.0.14393)
Processor=Intel Core i7-4790 CPU 3.60GHz (Haswell), ProcessorCount=8
Frequency=3507500 Hz, Resolution=285.1033 ns, Timer=TSC
dotnet cli version=2.0.0-preview1-005977
  [Host]     : .NET Core 4.6.25211.01, 64bit RyuJIT
  DefaultJob : .NET Core 4.6.25211.01, 64bit RyuJIT


```
 |               Method |         Mean |      Error |     StdDev | Scaled | ScaledSD |  Gen 0 | Allocated |
 |--------------------- |-------------:|-----------:|-----------:|-------:|---------:|-------:|----------:|
 |             LogEmpty |     8.652 ns |  0.0230 ns |  0.0215 ns |   1.00 |     0.00 |      - |       0 B |
 | LogEmptyWithEnricher |   104.790 ns |  0.4970 ns |  0.4405 ns |  12.11 |     0.06 | 0.0132 |      56 B |
 |            LogScalar |   432.424 ns |  0.6263 ns |  0.5858 ns |  49.98 |     0.14 | 0.1030 |     432 B |
 |        LogDictionary | 3,887.068 ns |  4.4649 ns |  3.7284 ns | 449.26 |     1.16 | 0.5417 |    2296 B |
 |          LogSequence | 1,428.896 ns |  3.6324 ns |  3.2200 ns | 165.15 |     0.53 | 0.2079 |     880 B |
 |         LogAnonymous | 6,694.431 ns | 22.4848 ns | 21.0323 ns | 773.73 |     3.00 | 0.8392 |    3528 B |
