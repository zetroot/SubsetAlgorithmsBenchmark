# Subsets methods benchmarking
Here goes the results of 3 subset methods benchmarking.

The tested methods are:
## Except().Any()
``` csharp
for(int i = 0; i < SupersetsInIteration; ++i)
{
    var superset = supersets[i];
    result[i] = !subset.Except(superset).Any();
}
```
## HashSet
``` csharp
var subset_hashset = subset.ToHashSet();
for (int i = 0; i < SupersetsInIteration; ++i)
{
    var superset = supersets[i].ToHashSet();                
    result[i] = subset_hashset.IsSubsetOf(superset);
}
```
## prebuilt HashSet
This test runs over a copy of supersets - prebuilt HashSets array.
``` csharp
for (int i = 0; i < SupersetsInIteration; ++i)
{
    var superset = hash_supersets[i];
    result[i] = subset_hashset.IsSubsetOf(superset);
}
```

## All(=>Contains)
``` csharp
for (int i = 0; i < SupersetsInIteration; ++i)
{
    var superset = supersets[i];
    result[i] = subset.All(superset.Contains);
}
```

# Result report from benchmarkdotnet
``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.572 (2004/?/20H1)
AMD Ryzen 5 2400G with Radeon Vega Graphics, 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.1.403
  [Host]     : .NET Core 3.1.9 (CoreCLR 4.700.20.47201, CoreFX 4.700.20.47203), X64 RyuJIT  [AttachedDebugger]
  DefaultJob : .NET Core 3.1.9 (CoreCLR 4.700.20.47201, CoreFX 4.700.20.47203), X64 RyuJIT


```
|             Method | Supersets In Iteration | Subset Length | Variability |          Mean |        Error |       StdDev |        Median |
|------------------- |--------------------- |------------- |------------ |--------------:|-------------:|-------------:|--------------:|
|     **Except().Any()** |                 **1000** |           **10** |         **100** |   **1,485.89 μs** |     **7.363 μs** |     **6.887 μs** |   **1,488.36 μs** |
|        ToHashSet() |                 1000 |           10 |         100 |   1,015.59 μs |     5.099 μs |     4.770 μs |   1,015.43 μs |
| &#39;prebuilt HashSet&#39; |                 1000 |           10 |         100 |      38.76 μs |     0.065 μs |     0.054 μs |      38.78 μs |
|    All(=&gt;Contains) |                 1000 |           10 |         100 |     105.46 μs |     0.320 μs |     0.267 μs |     105.38 μs |
|     **Except().Any()** |                 **1000** |           **10** |       **10000** |   **1,912.17 μs** |    **38.180 μs** |    **87.725 μs** |   **1,890.72 μs** |
|        ToHashSet() |                 1000 |           10 |       10000 |   1,038.70 μs |    20.028 μs |    40.459 μs |   1,019.35 μs |
| &#39;prebuilt HashSet&#39; |                 1000 |           10 |       10000 |      28.22 μs |     0.165 μs |     0.155 μs |      28.24 μs |
|    All(=&gt;Contains) |                 1000 |           10 |       10000 |      81.47 μs |     0.117 μs |     0.109 μs |      81.45 μs |
|     **Except().Any()** |                 **1000** |           **50** |         **100** |   **4,888.22 μs** |    **81.268 μs** |    **76.019 μs** |   **4,854.42 μs** |
|        ToHashSet() |                 1000 |           50 |         100 |   4,323.23 μs |    21.424 μs |    18.992 μs |   4,315.16 μs |
| &#39;prebuilt HashSet&#39; |                 1000 |           50 |         100 |     186.53 μs |     1.257 μs |     1.176 μs |     186.35 μs |
|    All(=&gt;Contains) |                 1000 |           50 |         100 |   1,173.37 μs |     2.667 μs |     2.227 μs |   1,173.08 μs |
|     **Except().Any()** |                 **1000** |           **50** |       **10000** |   **7,148.22 μs** |    **20.545 μs** |    **19.218 μs** |   **7,138.22 μs** |
|        ToHashSet() |                 1000 |           50 |       10000 |   4,576.69 μs |    20.955 μs |    17.499 μs |   4,574.34 μs |
| &#39;prebuilt HashSet&#39; |                 1000 |           50 |       10000 |      33.87 μs |     0.160 μs |     0.142 μs |      33.85 μs |
|    All(=&gt;Contains) |                 1000 |           50 |       10000 |     131.34 μs |     0.569 μs |     0.475 μs |     131.24 μs |
|     **Except().Any()** |                **10000** |           **10** |         **100** |  **14,798.42 μs** |   **120.423 μs** |   **112.643 μs** |  **14,775.43 μs** |
|        ToHashSet() |                10000 |           10 |         100 |  10,263.52 μs |    64.082 μs |    59.942 μs |  10,265.58 μs |
| &#39;prebuilt HashSet&#39; |                10000 |           10 |         100 |   1,241.19 μs |     4.248 μs |     3.973 μs |   1,241.75 μs |
|    All(=&gt;Contains) |                10000 |           10 |         100 |   1,058.41 μs |     6.766 μs |     6.329 μs |   1,059.22 μs |
|     **Except().Any()** |                **10000** |           **10** |       **10000** |  **16,318.65 μs** |    **97.878 μs** |    **91.555 μs** |  **16,310.02 μs** |
|        ToHashSet() |                10000 |           10 |       10000 |  10,393.23 μs |    68.236 μs |    63.828 μs |  10,386.27 μs |
| &#39;prebuilt HashSet&#39; |                10000 |           10 |       10000 |   1,087.21 μs |     2.812 μs |     2.631 μs |   1,085.89 μs |
|    All(=&gt;Contains) |                10000 |           10 |       10000 |     847.88 μs |     1.536 μs |     1.436 μs |     847.34 μs |
|     **Except().Any()** |                **10000** |           **50** |         **100** |  **48,257.76 μs** |   **232.573 μs** |   **181.578 μs** |  **48,236.31 μs** |
|        ToHashSet() |                10000 |           50 |         100 |  43,938.46 μs |   994.200 μs | 2,687.877 μs |  42,877.97 μs |
| &#39;prebuilt HashSet&#39; |                10000 |           50 |         100 |   4,634.98 μs |    16.757 μs |    15.675 μs |   4,643.17 μs |
|    All(=&gt;Contains) |                10000 |           50 |         100 |  10,256.62 μs |    26.440 μs |    24.732 μs |  10,243.34 μs |
|     **Except().Any()** |                **10000** |           **50** |       **10000** |  **73,192.15 μs** |   **479.584 μs** |   **425.139 μs** |  **73,077.26 μs** |
|        ToHashSet() |                10000 |           50 |       10000 |  45,880.72 μs |   141.497 μs |   125.433 μs |  45,860.50 μs |
| &#39;prebuilt HashSet&#39; |                10000 |           50 |       10000 |   1,620.61 μs |     3.507 μs |     3.280 μs |   1,620.52 μs |
|    All(=&gt;Contains) |                10000 |           50 |       10000 |   1,460.01 μs |     1.819 μs |     1.702 μs |   1,459.49 μs |
|     **Except().Any()** |               **100000** |           **10** |         **100** | **149,047.91 μs** | **1,696.388 μs** | **1,586.803 μs** | **149,063.20 μs** |
|        ToHashSet() |               100000 |           10 |         100 | 100,657.74 μs |   150.890 μs |   117.805 μs | 100,654.39 μs |
| &#39;prebuilt HashSet&#39; |               100000 |           10 |         100 |  12,753.33 μs |    17.257 μs |    15.298 μs |  12,749.85 μs |
|    All(=&gt;Contains) |               100000 |           10 |         100 |  11,238.79 μs |    54.228 μs |    50.725 μs |  11,247.03 μs |
|     **Except().Any()** |               **100000** |           **10** |       **10000** | **163,277.55 μs** | **1,096.107 μs** | **1,025.299 μs** | **163,556.98 μs** |
|        ToHashSet() |               100000 |           10 |       10000 |  99,927.78 μs |   403.811 μs |   337.201 μs |  99,812.12 μs |
| &#39;prebuilt HashSet&#39; |               100000 |           10 |       10000 |  11,671.99 μs |     6.753 μs |     5.986 μs |  11,672.28 μs |
|    All(=&gt;Contains) |               100000 |           10 |       10000 |   8,217.51 μs |    67.959 μs |    56.749 μs |   8,225.85 μs |
|     **Except().Any()** |               **100000** |           **50** |         **100** | **493,925.76 μs** | **2,169.048 μs** | **1,922.805 μs** | **493,386.70 μs** |
|        ToHashSet() |               100000 |           50 |         100 | 432,214.15 μs | 1,261.673 μs | 1,180.169 μs | 431,624.50 μs |
| &#39;prebuilt HashSet&#39; |               100000 |           50 |         100 |  49,593.29 μs |    75.300 μs |    66.751 μs |  49,598.45 μs |
|    All(=&gt;Contains) |               100000 |           50 |         100 |  98,662.71 μs |   119.057 μs |   111.366 μs |  98,656.00 μs |
|     **Except().Any()** |               **100000** |           **50** |       **10000** | **733,526.81 μs** | **8,728.516 μs** | **8,164.659 μs** | **733,455.20 μs** |
|        ToHashSet() |               100000 |           50 |       10000 | 460,166.27 μs | 7,227.011 μs | 6,760.150 μs | 457,359.70 μs |
| &#39;prebuilt HashSet&#39; |               100000 |           50 |       10000 |  17,443.96 μs |    10.839 μs |     9.608 μs |  17,443.40 μs |
|    All(=&gt;Contains) |               100000 |           50 |       10000 |  14,222.31 μs |    47.090 μs |    44.048 μs |  14,217.94 μs |
