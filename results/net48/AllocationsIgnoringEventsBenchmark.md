``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.450 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4200.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8 (4.8.4200.0), X86 LegacyJIT


```
|               Method |      Mean |     Error |    StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |----------:|----------:|----------:|------:|--------:|-------:|------:|------:|----------:|
|             LogEmpty |  2.937 ns | 0.0824 ns | 0.0916 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |  2.962 ns | 0.0847 ns | 0.1102 ns |  1.01 |    0.06 |      - |     - |     - |         - |
|               LogMsg |  4.798 ns | 0.1043 ns | 0.0976 ns |  1.63 |    0.06 |      - |     - |     - |         - |
|         LogMsgWithEx |  5.051 ns | 0.0790 ns | 0.0739 ns |  1.72 |    0.06 |      - |     - |     - |         - |
|           LogScalar1 |  9.050 ns | 0.1730 ns | 0.1851 ns |  3.09 |    0.12 |      - |     - |     - |         - |
|           LogScalar2 | 10.420 ns | 0.2025 ns | 0.1894 ns |  3.54 |    0.12 |      - |     - |     - |         - |
|           LogScalar3 | 10.560 ns | 0.1801 ns | 0.1684 ns |  3.59 |    0.12 |      - |     - |     - |         - |
|        LogScalarMany | 19.657 ns | 0.3445 ns | 0.3223 ns |  6.68 |    0.26 | 0.0053 |     - |     - |      28 B |
|     LogScalarStruct1 |  6.360 ns | 0.1502 ns | 0.1730 ns |  2.17 |    0.09 |      - |     - |     - |         - |
|     LogScalarStruct2 |  8.952 ns | 0.2040 ns | 0.2095 ns |  3.05 |    0.16 |      - |     - |     - |         - |
|     LogScalarStruct3 |  8.907 ns | 0.1753 ns | 0.1640 ns |  3.03 |    0.13 |      - |     - |     - |         - |
|  LogScalarStructMany | 25.493 ns | 0.3673 ns | 0.3435 ns |  8.66 |    0.28 | 0.0145 |     - |     - |      76 B |
|   LogScalarBigStruct | 20.656 ns | 0.4163 ns | 0.4088 ns |  7.04 |    0.26 |      - |     - |     - |         - |
|        LogDictionary | 10.310 ns | 0.2007 ns | 0.1878 ns |  3.50 |    0.13 | 0.0031 |     - |     - |      16 B |
|          LogSequence | 10.286 ns | 0.1856 ns | 0.1737 ns |  3.50 |    0.12 | 0.0031 |     - |     - |      16 B |
|         LogAnonymous | 10.275 ns | 0.2211 ns | 0.2068 ns |  3.49 |    0.12 | 0.0031 |     - |     - |      16 B |
|              LogMix2 | 10.764 ns | 0.2298 ns | 0.2150 ns |  3.66 |    0.14 |      - |     - |     - |         - |
|              LogMix3 | 11.559 ns | 0.2158 ns | 0.2019 ns |  3.93 |    0.11 |      - |     - |     - |         - |
|              LogMix4 | 26.071 ns | 0.4424 ns | 0.4138 ns |  8.86 |    0.36 | 0.0153 |     - |     - |      80 B |
|              LogMix5 | 30.946 ns | 0.6429 ns | 0.7895 ns | 10.53 |    0.34 | 0.0183 |     - |     - |      96 B |
|           LogMixMany | 57.969 ns | 0.5885 ns | 0.5505 ns | 19.70 |    0.65 | 0.0321 |     - |     - |     168 B |
