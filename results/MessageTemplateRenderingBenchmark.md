``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.450 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.401
  [Host]          : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT
  core31 RyuJit   : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT
  net48 LegacyJit : .NET Framework 4.8 (4.8.4200.0), X64 RyuJIT
  net48 RyuJit    : .NET Framework 4.8 (4.8.4200.0), X64 RyuJIT

IterationCount=15  LaunchCount=2  WarmupCount=10  

```
|                       Method |             Job |       Jit |       Runtime |       Mean |     Error |    StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|----------------------------- |---------------- |---------- |-------------- |-----------:|----------:|----------:|-------:|------:|------:|----------:|
|     TemplateWithNoProperties |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   3.908 ns | 0.3465 ns | 0.5186 ns |      - |     - |     - |         - |
| TemplateWithVariedProperties |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 298.268 ns | 3.3369 ns | 4.9945 ns | 0.0153 |     - |     - |      96 B |
|     TemplateWithNoProperties | net48 LegacyJit | LegacyJit |      .NET 4.8 |   4.174 ns | 0.0488 ns | 0.0731 ns |      - |     - |     - |         - |
| TemplateWithVariedProperties | net48 LegacyJit | LegacyJit |      .NET 4.8 | 379.530 ns | 2.5731 ns | 3.7717 ns | 0.0153 |     - |     - |      96 B |
|     TemplateWithNoProperties |    net48 RyuJit |    RyuJit |      .NET 4.8 |   4.174 ns | 0.0541 ns | 0.0809 ns |      - |     - |     - |         - |
| TemplateWithVariedProperties |    net48 RyuJit |    RyuJit |      .NET 4.8 | 378.676 ns | 3.0587 ns | 4.5781 ns | 0.0153 |     - |     - |      96 B |
