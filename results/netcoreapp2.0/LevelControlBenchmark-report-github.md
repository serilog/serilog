``` ini

BenchmarkDotNet=v0.10.6, OS=Mac OS X 10.12
Processor=Intel Core i7-7567U CPU 3.50GHz (Kaby Lake), ProcessorCount=4
Frequency=1000000000 Hz, Resolution=1.0000 ns, Timer=UNKNOWN
dotnet cli version=2.1.4
  [Host]     : .NET Core 4.6.0.0, 64bit RyuJIT
  DefaultJob : .NET Core 4.6.0.0, 64bit RyuJIT


```
 |         Method |      Mean |     Error |    StdDev | Scaled | ScaledSD |
 |--------------- |----------:|----------:|----------:|-------:|---------:|
 |            Off |  2.931 ns | 0.0893 ns | 0.0877 ns |   1.00 |     0.00 |
 | LevelSwitchOff |  3.636 ns | 0.1048 ns | 0.1029 ns |   1.24 |     0.05 |
 | MinimumLevelOn | 10.497 ns | 0.2462 ns | 0.3370 ns |   3.58 |     0.15 |
 |  LevelSwitchOn | 10.981 ns | 0.3005 ns | 0.3340 ns |   3.75 |     0.15 |
