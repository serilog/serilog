``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-7700HQ CPU 2.80GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=2.2.401
  [Host]     : .NET Core 2.2.7 (CoreCLR 4.6.28008.02, CoreFX 4.6.28008.03), 64bit RyuJIT
  DefaultJob : .NET Core 2.2.7 (CoreCLR 4.6.28008.02, CoreFX 4.6.28008.03), 64bit RyuJIT


```
|       Method |     Mean |     Error |    StdDev | Ratio | RatioSD |
|------------- |---------:|----------:|----------:|------:|--------:|
|   RootLogger | 11.26 ns | 0.2527 ns | 0.2809 ns |  1.00 |    0.00 |
| NestedLogger | 57.59 ns | 3.1832 ns | 9.3858 ns |  5.67 |    0.52 |
