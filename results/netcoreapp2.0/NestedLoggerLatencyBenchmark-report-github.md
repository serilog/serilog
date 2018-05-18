``` ini

BenchmarkDotNet=v0.10.6, OS=Mac OS X 10.12
Processor=Intel Core i7-7567U CPU 3.50GHz (Kaby Lake), ProcessorCount=4
Frequency=1000000000 Hz, Resolution=1.0000 ns, Timer=UNKNOWN
dotnet cli version=2.1.4
  [Host]     : .NET Core 4.6.0.0, 64bit RyuJIT
  DefaultJob : .NET Core 4.6.0.0, 64bit RyuJIT


```
 |       Method |     Mean |     Error |    StdDev | Scaled | ScaledSD |
 |------------- |---------:|----------:|----------:|-------:|---------:|
 |   RootLogger | 11.78 ns | 0.1157 ns | 0.1082 ns |   1.00 |     0.00 |
 | NestedLogger | 41.70 ns | 0.1993 ns | 0.1556 ns |   3.54 |     0.03 |
