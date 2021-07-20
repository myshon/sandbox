``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
Intel Core i7-9750HF CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=5.0.102
  [Host]        : .NET Core 3.1.11 (CoreCLR 4.700.20.56602, CoreFX 4.700.20.56604), X64 RyuJIT
  .NET Core 3.1 : .NET Core 3.1.11 (CoreCLR 4.700.20.56602, CoreFX 4.700.20.56604), X64 RyuJIT
  .NET Core 5.0 : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT


```
|                             Method |           Job |       Runtime |        Mean |     Error |    StdDev |
|----------------------------------- |-------------- |-------------- |------------:|----------:|----------:|
|         ShortText_ReplaceWithRegex | .NET Core 3.1 | .NET Core 3.1 |    868.9 ns |   6.35 ns |   5.94 ns |
| ShortText_ReplaceWithStringBuilder | .NET Core 3.1 | .NET Core 3.1 |  1,673.0 ns |  11.25 ns |   9.39 ns |
|        ShortText_ReplaceWithString | .NET Core 3.1 | .NET Core 3.1 |    658.7 ns |   3.61 ns |   3.38 ns |
|          LongText_ReplaceWithRegex | .NET Core 3.1 | .NET Core 3.1 |  2,509.8 ns |  21.93 ns |  20.52 ns |
|  LongText_ReplaceWithStringBuilder | .NET Core 3.1 | .NET Core 3.1 | 10,640.4 ns | 155.71 ns | 138.03 ns |
|         LongText_ReplaceWithString | .NET Core 3.1 | .NET Core 3.1 |  3,527.2 ns |  59.02 ns |  55.20 ns |
|         ShortText_ReplaceWithRegex | .NET Core 5.0 | .NET Core 5.0 |    707.8 ns |  12.74 ns |  17.01 ns |
| ShortText_ReplaceWithStringBuilder | .NET Core 5.0 | .NET Core 5.0 |  1,779.1 ns |  33.45 ns |  31.29 ns |
|        ShortText_ReplaceWithString | .NET Core 5.0 | .NET Core 5.0 |    637.4 ns |  12.56 ns |  22.65 ns |
|          LongText_ReplaceWithRegex | .NET Core 5.0 | .NET Core 5.0 |  1,923.6 ns |  36.92 ns |  34.53 ns |
|  LongText_ReplaceWithStringBuilder | .NET Core 5.0 | .NET Core 5.0 |  9,978.3 ns | 125.58 ns | 111.32 ns |
|         LongText_ReplaceWithString | .NET Core 5.0 | .NET Core 5.0 |  3,375.1 ns |  65.36 ns |  75.26 ns |
