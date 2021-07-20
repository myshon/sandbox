``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
Intel Core i7-9750HF CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=5.0.104
  [Host]        : .NET Core 3.1.13 (CoreCLR 4.700.21.11102, CoreFX 4.700.21.11602), X64 RyuJIT
  .NET Core 5.0 : .NET Core 5.0.4 (CoreCLR 5.0.421.11614, CoreFX 5.0.421.11614), X64 RyuJIT

Job=.NET Core 5.0  Runtime=.NET Core 5.0  

```
|                      Method |      Mean |     Error |   StdDev |    Median |
|---------------------------- |----------:|----------:|---------:|----------:|
|                     IsArray |  8.906 ns | 0.3526 ns | 1.017 ns |  8.505 ns |
| IEnumerableIsAssignableFrom | 22.746 ns | 0.5304 ns | 1.539 ns | 21.973 ns |
