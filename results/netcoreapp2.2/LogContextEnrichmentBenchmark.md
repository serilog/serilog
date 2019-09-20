``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-7700HQ CPU 2.80GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=2.2.401
  [Host]     : .NET Core 2.2.7 (CoreCLR 4.6.28008.02, CoreFX 4.6.28008.03), 64bit RyuJIT
  DefaultJob : .NET Core 2.2.7 (CoreCLR 4.6.28008.02, CoreFX 4.6.28008.03), 64bit RyuJIT


```
|               Method |      Mean |     Error |    StdDev |    Median | Ratio | RatioSD |
|--------------------- |----------:|----------:|----------:|----------:|------:|--------:|
|                 Bare |  11.02 ns | 0.0582 ns | 0.0515 ns |  11.03 ns |  1.00 |    0.00 |
|         PushProperty | 101.25 ns | 0.8756 ns | 0.7762 ns | 101.28 ns |  9.19 |    0.07 |
|   PushPropertyNested | 215.39 ns | 4.3243 ns | 7.9072 ns | 210.98 ns | 20.01 |    0.89 |
| PushPropertyEnriched | 181.38 ns | 1.1945 ns | 1.1174 ns | 181.80 ns | 16.45 |    0.15 |
