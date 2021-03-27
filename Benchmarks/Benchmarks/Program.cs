using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

namespace Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<ReplaceVsRegex>();
        }

        [SimpleJob(RuntimeMoniker.NetCoreApp31)]
        [SimpleJob(RuntimeMoniker.NetCoreApp50)]
        [RPlotExporter]
        public class ReplaceVsRegex
        {
            private IReadOnlyDictionary<string, string> placeholders;
            private string text;

            [GlobalSetup]
            public void Setup()
            {
                placeholders = new Dictionary<string, string>()
                {
                    {"entityType", "room"},
                    {"id", Guid.NewGuid().ToString()},
                    {"zoneId", Guid.NewGuid().ToString()},
                    {"code", "DELUXE"},
                };
                text = "Entity {entityType} of code {code} has been created with {id} successfully";
            }

            [Benchmark]
            public void ReplaceWithRegex()
            {
                var result = RegexReplace(text, placeholders);
            }

            [Benchmark]
            public void ReplaceWithStringBuilder()
            {
                var result = ReplaceBuilder(text, placeholders);
            }

            static readonly Regex patternReplaceRegex = new(@"\{([^\}]+)\}", RegexOptions.Compiled);

            private static string RegexReplace(string value, IReadOnlyDictionary<string, string> placeholderValues)
            {
                return patternReplaceRegex.Replace(value, match => placeholderValues[match.Groups[1].Value]!);
            }

            private static string ReplaceBuilder(string value, IReadOnlyDictionary<string, string> placeholderValues)
            {
                return Replace(new StringBuilder(value), placeholderValues).ToString();
            }

            private static StringBuilder Replace(StringBuilder valueBuilder, IReadOnlyDictionary<string, string> placeholderValues)
            {
                foreach (var field in placeholderValues)
                    valueBuilder.Replace(GetPlaceholder(field.Key), field.Value);

                return valueBuilder;
            }

            private static string GetPlaceholder(string key) => $"{{{key}}}";
        }
    }
}
