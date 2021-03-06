﻿using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SubsetAlgorithmsBenchmark
{
    public class IsSubsetTests
    {
        [Params(1000, 10000, 100000)]
        public int SupersetsInIteration { get; set; }

        [Params(10, 50)]
        public int SubsetLength { get; set; }

        [Params(100, 10000)]
        public int Variability { get; set; }

        private int[] subset;
        private int[][] supersets;
        private HashSet<int>[] hash_supersets;

        [GlobalSetup]
        public void Setup()
        {
            var rand = new Random();
            subset = Enumerable.Range(0, SubsetLength)
                .Select(_ => rand.Next(Variability))
                .ToArray();
            supersets = Enumerable
                .Range(0, SupersetsInIteration)
                .Select(_ => 
                    Enumerable
                        .Range(0, rand.Next(SubsetLength * 10))
                        .Select(_ => rand.Next(Variability))
                        .ToArray())
                .ToArray();
            hash_supersets = supersets.Select(x => x.ToHashSet()).ToArray();
        }

        [GlobalCleanup]
        public void Cleanup()
        {
            subset = null;
            for (int i = 0; i < supersets.Length; ++i)
                supersets[i] = null;

            for (int i = 0; i < hash_supersets.Length; ++i)
                hash_supersets[i] = null;

            supersets = null;
            hash_supersets = null;
            GC.Collect(2, GCCollectionMode.Forced, true, true);
        }

        [Benchmark(Description = "Except().Any()")]
        public bool[] Except()
        {
            var result = new bool[SupersetsInIteration];
            for(int i = 0; i < SupersetsInIteration; ++i)
            {
                var superset = supersets[i];
                result[i] = !subset.Except(superset).Any();
            }
            return result;
        }

        [Benchmark(Description = "ToHashSet()")]
        public bool[] Hashsets()
        {
            var result = new bool[SupersetsInIteration];
            var subset_hashset = subset.ToHashSet();
            for (int i = 0; i < SupersetsInIteration; ++i)
            {
                var superset = supersets[i].ToHashSet();                
                result[i] = subset_hashset.IsSubsetOf(superset);
            }
            return result;
        }

        [Benchmark(Description = "prebuilt HashSet")]
        public bool[] PrebuiltHashsets()
        {
            var result = new bool[SupersetsInIteration];
            var subset_hashset = subset.ToHashSet();
            for (int i = 0; i < SupersetsInIteration; ++i)
            {
                var superset = hash_supersets[i];
                result[i] = subset_hashset.IsSubsetOf(superset);
            }
            return result;
        }

        [Benchmark(Description = "All(=>Contains)")]
        public bool[] AllContains()
        {
            var result = new bool[SupersetsInIteration];            
            for (int i = 0; i < SupersetsInIteration; ++i)
            {
                var superset = supersets[i];
                result[i] = subset.All(superset.Contains);
            }
            return result;
        }
    }
}
