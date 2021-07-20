using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace Benchmarks
{
    [SimpleJob(RuntimeMoniker.NetCoreApp31)]
    [SimpleJob(RuntimeMoniker.NetCoreApp50)]
    [RPlotExporter]
    public class ReplaceVsRegex
    {
        private IReadOnlyDictionary<string, string> shortTextPlaceholders;
        private IReadOnlyDictionary<string, string> longTextPlaceholders;
        private string shortText;
        private string longText;

        [GlobalSetup]
        public void Setup()
        {
            shortTextPlaceholders = new Dictionary<string, string>()
            {
                {"entityType", "room"},
                {"id", Guid.NewGuid().ToString()},
                {"zoneId", Guid.NewGuid().ToString()},
                {"code", "DELUXE"}
            };
            shortText = "Entity {entityType} of code {code} has been created with {id} successfully";

            longTextPlaceholders = new Dictionary<string, string>()
            {
                {"entityType", "room"},
                {"id", Guid.NewGuid().ToString()},
                {"zoneId", Guid.NewGuid().ToString()},
                {"code", "DELUXE"},
                {"name1", "value1"},
                {"name2", "value2"},
                {"name3", "value3"},
                {"name4", "value4"},
                {"name5", "value5"},
                {"name6", "value6"},
            };
            longText = "Entity {entityType} of code {code} has been created with {id} successfully with param {name1} " +
                       "and another param {name2} and yet another param {name3} and blablablabla {name4} {name5} ans also {name6}";
        }

        [Benchmark]
        public void ShortText_ReplaceWithRegex()
        {
            var result = RegexReplace(shortText, shortTextPlaceholders);
        }

        [Benchmark]
        public void ShortText_ReplaceWithStringBuilder()
        {
            var result = ReplaceBuilder(shortText, shortTextPlaceholders);
        }

        [Benchmark]
        public void ShortText_ReplaceWithString()
        {
            var result = ReplaceNaive(shortText, shortTextPlaceholders);
        }

        [Benchmark]
        public void LongText_ReplaceWithRegex()
        {
            var result = RegexReplace(longText, longTextPlaceholders);
        }

        [Benchmark]
        public void LongText_ReplaceWithStringBuilder()
        {
            var result = ReplaceBuilder(longText, longTextPlaceholders);
        }

        [Benchmark]
        public void LongText_ReplaceWithString()
        {
            var result = ReplaceNaive(longText, longTextPlaceholders);
        }

        private static readonly Regex patternReplaceRegex = new Regex(@"\{([^\}]+)\}", RegexOptions.Compiled);

        private static string RegexReplace(string value, IReadOnlyDictionary<string, string> placeholderValues)
        {
            return patternReplaceRegex.Replace(value, match => placeholderValues[match.Groups[1].Value]!);
        }

        private static string ReplaceBuilder(string value, IReadOnlyDictionary<string, string> placeholderValues)
        {
            return Replace(new StringBuilder(value), placeholderValues).ToString();
        }

        private static string ReplaceNaive(string value, IReadOnlyDictionary<string, string> placeholderValues)
        {
            foreach (var field in placeholderValues)
                value = value.Replace(GetPlaceholder(field.Key), field.Value);

            return value;
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
