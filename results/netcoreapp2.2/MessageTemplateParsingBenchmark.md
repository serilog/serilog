``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=2.2.402
  [Host]     : .NET Core 2.2.7 (CoreCLR 4.6.28008.02, CoreFX 4.6.28008.03), 64bit RyuJIT
  DefaultJob : .NET Core 2.2.7 (CoreCLR 4.6.28008.02, CoreFX 4.6.28008.03), 64bit RyuJIT


```
|                       Method |       Mean |     Error |    StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|----------------------------- |-----------:|----------:|----------:|------:|--------:|-------:|------:|------:|----------:|
|                EmptyTemplate |   244.0 ns |  1.340 ns |  1.253 ns |  1.00 |    0.00 | 0.0415 |     - |     - |     264 B |
|           SimpleTextTemplate |   301.9 ns |  1.870 ns |  1.658 ns |  1.24 |    0.01 | 0.0672 |     - |     - |     424 B |
|  SinglePropertyTokenTemplate |   418.2 ns |  2.045 ns |  1.708 ns |  1.71 |    0.01 | 0.0901 |     - |     - |     568 B |
|    ManyPropertyTokenTemplate |   659.2 ns | 12.797 ns | 11.970 ns |  2.70 |    0.05 | 0.1698 |     - |     - |    1072 B |
|       MultipleTokensTemplate | 1,466.7 ns |  7.384 ns |  6.166 ns |  6.01 |    0.05 | 0.2918 |     - |     - |    1840 B |
| DefaultConsoleOutputTemplate | 1,695.5 ns |  8.924 ns |  7.911 ns |  6.95 |    0.04 | 0.3643 |     - |     - |    2296 B |
