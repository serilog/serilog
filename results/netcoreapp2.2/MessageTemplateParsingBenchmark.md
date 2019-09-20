``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-7700HQ CPU 2.80GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=2.2.401
  [Host]     : .NET Core 2.2.7 (CoreCLR 4.6.28008.02, CoreFX 4.6.28008.03), 64bit RyuJIT
  DefaultJob : .NET Core 2.2.7 (CoreCLR 4.6.28008.02, CoreFX 4.6.28008.03), 64bit RyuJIT


```
|                       Method |       Mean |     Error |     StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|----------------------------- |-----------:|----------:|-----------:|------:|--------:|-------:|------:|------:|----------:|
|                EmptyTemplate |   281.4 ns |  5.711 ns | 10.0025 ns |  1.00 |    0.00 | 0.0834 |     - |     - |     264 B |
|           SimpleTextTemplate |   343.8 ns |  2.817 ns |  2.6348 ns |  1.18 |    0.04 | 0.1345 |     - |     - |     424 B |
|  SinglePropertyTokenTemplate |   465.4 ns |  1.096 ns |  0.9153 ns |  1.59 |    0.05 | 0.1802 |     - |     - |     568 B |
|    ManyPropertyTokenTemplate |   752.2 ns |  3.918 ns |  3.6647 ns |  2.59 |    0.09 | 0.3405 |     - |     - |    1072 B |
|       MultipleTokensTemplate | 1,466.1 ns | 10.242 ns |  9.5805 ns |  5.04 |    0.17 | 0.5836 |     - |     - |    1840 B |
| DefaultConsoleOutputTemplate | 1,941.6 ns | 16.359 ns | 15.3021 ns |  6.67 |    0.24 | 0.7286 |     - |     - |    2296 B |
