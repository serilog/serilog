``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=2.2.402
  [Host]     : .NET Core 2.2.7 (CoreCLR 4.6.28008.02, CoreFX 4.6.28008.03), 64bit RyuJIT
  DefaultJob : .NET Core 2.2.7 (CoreCLR 4.6.28008.02, CoreFX 4.6.28008.03), 64bit RyuJIT


```
|       Method |     Mean |     Error |    StdDev | Ratio | RatioSD |
|------------- |---------:|----------:|----------:|------:|--------:|
|   RootLogger | 10.15 ns | 0.1141 ns | 0.1067 ns |  1.00 |    0.00 |
| NestedLogger | 38.61 ns | 0.5911 ns | 0.5529 ns |  3.81 |    0.07 |
