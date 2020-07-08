``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18363
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.8.4121.0
  DefaultJob : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.8.4121.0


```
|                              Method |     Mean |     Error |    StdDev |   Median | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------------------------ |---------:|----------:|----------:|---------:|------:|--------:|-------:|------:|------:|----------:|
|               MessageTemplateParser | 5.313 us | 0.0823 us | 0.0643 us | 5.295 us |  1.00 |    0.00 | 0.7935 |     - |     - |   4.08 KB |
|  MessageTemplateParserWithSmallMods | 4.975 us | 0.0960 us | 0.0898 us | 4.978 us |  0.94 |    0.02 | 0.7858 |     - |     - |   4.05 KB |
|   MessageTemplateParserWithMoreMods | 4.691 us | 0.0911 us | 0.0808 us | 4.672 us |  0.88 |    0.01 | 0.5951 |     - |     - |   3.07 KB |
|        MessageTemplateParserSpanArr | 6.262 us | 0.0661 us | 0.0619 us | 6.271 us |  1.18 |    0.02 | 0.5417 |     - |     - |    2.8 KB |
|    MessageTemplateParserSpanArrNoIn | 6.481 us | 0.1164 us | 0.0972 us | 6.487 us |  1.22 |    0.03 | 0.5417 |     - |     - |    2.8 KB |
| MessageTemplateParserMemoryIterator | 7.876 us | 0.1587 us | 0.3740 us | 7.750 us |  1.46 |    0.09 | 0.5493 |     - |     - |   2.83 KB |
|          MessageTemplateParserLexer | 4.143 us | 0.0782 us | 0.0694 us | 4.136 us |  0.78 |    0.02 | 0.1984 |     - |     - |   1.05 KB |
