using BenchmarkDotNet.Running;
using System;

namespace SubsetAlgorithmsBenchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            _ = BenchmarkRunner.Run<IsSubsetTests>();
        }
    }
}
