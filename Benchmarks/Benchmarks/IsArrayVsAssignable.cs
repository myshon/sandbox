using System;
using System.Collections;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace Benchmarks
{
    // [SimpleJob(RuntimeMoniker.NetCoreApp31)]
    [SimpleJob(RuntimeMoniker.NetCoreApp50)]
    [RPlotExporter]
    public class IsArrayVsAssignable
    {
        private Type[] array1, array2;
        
        [GlobalSetup]
        public void Setup()
        {
            array1 = new[] { typeof(string[]), typeof(object[])};
            array2 = new[] { typeof(string[]), typeof(object[])};
        }

        private static bool IsEnumerableFast(Type type) => type.IsArray || typeof(IEnumerable).IsAssignableFrom(type);
        
        private static bool IsEnumerable(Type type) => typeof(IEnumerable).IsAssignableFrom(type);
        
        [Benchmark]
        public void IsArray()
        {
            for (var i = 0; i < array1.Length; i++)
            {
                IsEnumerableFast(array1[i]);
            }
        }

        [Benchmark]
        public void IEnumerableIsAssignableFrom()
        {
            for (var i = 0; i < array1.Length; i++)
            {
                IsEnumerable(array1[i]);
            }
        }
    }

}
