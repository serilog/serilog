``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=2.2.402
  [Host]     : .NET Core 2.2.7 (CoreCLR 4.6.28008.02, CoreFX 4.6.28008.03), 64bit RyuJIT
  DefaultJob : .NET Core 2.2.7 (CoreCLR 4.6.28008.02, CoreFX 4.6.28008.03), 64bit RyuJIT


```
|               Method |          Mean |      Error |     StdDev |    Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |--------------:|-----------:|-----------:|---------:|--------:|-------:|------:|------:|----------:|
|             LogEmpty |      7.443 ns |  0.0540 ns |  0.0505 ns |     1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |     50.760 ns |  0.3244 ns |  0.2876 ns |     6.81 |    0.05 | 0.0089 |     - |     - |      56 B |
|               LogMsg |    510.372 ns |  6.4418 ns |  6.0257 ns |    68.58 |    1.07 | 0.0219 |     - |     - |     144 B |
|         LogMsgWithEx |    517.901 ns |  4.8577 ns |  4.5439 ns |    69.59 |    0.73 | 0.0219 |     - |     - |     144 B |
|           LogScalar1 |    618.053 ns |  1.2751 ns |  1.1304 ns |    82.96 |    0.49 | 0.0591 |     - |     - |     376 B |
|           LogScalar2 |    677.111 ns |  4.0981 ns |  3.8334 ns |    90.98 |    0.78 | 0.0668 |     - |     - |     424 B |
|           LogScalar3 |    745.218 ns |  6.9845 ns |  6.5333 ns |   100.13 |    1.33 | 0.0744 |     - |     - |     472 B |
|        LogScalarMany |    811.147 ns |  4.4601 ns |  4.1720 ns |   108.99 |    0.95 | 0.1001 |     - |     - |     632 B |
|     LogScalarStruct1 |    660.102 ns |  3.9093 ns |  3.4655 ns |    88.60 |    0.52 | 0.0629 |     - |     - |     400 B |
|     LogScalarStruct2 |    748.870 ns |  4.0426 ns |  3.7814 ns |   100.62 |    0.89 | 0.0744 |     - |     - |     472 B |
|     LogScalarStruct3 |    866.232 ns |  4.0317 ns |  3.7713 ns |   116.39 |    1.10 | 0.0858 |     - |     - |     544 B |
|  LogScalarStructMany |    936.894 ns |  4.5198 ns |  4.2279 ns |   125.89 |    0.99 | 0.1154 |     - |     - |     728 B |
|   LogScalarBigStruct |    856.488 ns | 10.2737 ns |  9.6100 ns |   115.09 |    1.76 | 0.0725 |     - |     - |     456 B |
|        LogDictionary |  3,330.557 ns | 17.9821 ns | 15.9406 ns |   447.04 |    2.77 | 0.3548 |     - |     - |    2240 B |
|          LogSequence |  1,594.334 ns | 10.9747 ns | 10.2657 ns |   214.23 |    1.99 | 0.1297 |     - |     - |     824 B |
|         LogAnonymous |  6,494.401 ns | 30.9328 ns | 28.9346 ns |   872.63 |    7.54 | 0.5417 |     - |     - |    3440 B |
|              LogMix2 |    700.711 ns |  6.4606 ns |  6.0433 ns |    94.15 |    0.90 | 0.0706 |     - |     - |     448 B |
|              LogMix3 |    824.618 ns | 15.5482 ns | 13.7831 ns |   110.69 |    2.15 | 0.0820 |     - |     - |     520 B |
|              LogMix4 |    874.831 ns |  4.4717 ns |  3.9640 ns |   117.42 |    0.93 | 0.1125 |     - |     - |     712 B |
|              LogMix5 |    962.264 ns | 12.7794 ns | 11.9539 ns |   129.29 |    1.69 | 0.1240 |     - |     - |     784 B |
|           LogMixMany | 12,265.501 ns | 74.9313 ns | 70.0908 ns | 1,648.07 |   14.33 | 1.0376 |     - |     - |    6545 B |
