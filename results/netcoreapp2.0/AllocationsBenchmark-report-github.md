``` ini

BenchmarkDotNet=v0.10.6, OS=Mac OS X 10.12
Processor=Intel Core i7-7567U CPU 3.50GHz (Kaby Lake), ProcessorCount=4
Frequency=1000000000 Hz, Resolution=1.0000 ns, Timer=UNKNOWN
dotnet cli version=2.1.4
  [Host]     : .NET Core 4.6.0.0, 64bit RyuJIT
  DefaultJob : .NET Core 4.6.0.0, 64bit RyuJIT


```
 |               Method |          Mean |       Error |        StdDev |        Median |   Scaled | ScaledSD |  Gen 0 | Allocated |
 |--------------------- |--------------:|------------:|--------------:|--------------:|---------:|---------:|-------:|----------:|
 |             LogEmpty |      8.295 ns |   0.1916 ns |     0.1882 ns |      8.251 ns |     1.00 |     0.00 |      - |       0 B |
 | LogEmptyWithEnricher |    129.205 ns |   4.7752 ns |    13.5464 ns |    126.329 ns |    15.58 |     1.66 | 0.0265 |      56 B |
 |            LogScalar |    998.006 ns |  19.7947 ns |    48.5568 ns |    993.968 ns |   120.37 |     6.37 | 0.2060 |     432 B |
 |        LogDictionary |  6,418.543 ns | 142.5459 ns |   418.0626 ns |  6,304.087 ns |   774.13 |    52.90 | 1.0910 |    2296 B |
 |          LogSequence |  2,664.592 ns |  52.9768 ns |   145.9136 ns |  2,655.944 ns |   321.37 |    18.83 | 0.4158 |     880 B |
 |         LogAnonymous | 12,776.209 ns | 378.7175 ns | 1,110.7127 ns | 12,563.375 ns | 1,540.91 |   137.42 | 1.6785 |    3528 B |
