using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Dynamic.Core;
using NodaTime;
using NodaTime.Text;
using Xunit;

namespace DynamicLinqSample
{
    public class DynamicLinqTests
    {
        private class Entity
        {
            public Guid Id { get; set; }
            public string FirstName { get; set; }
            public LocalDate BirthDate { get; set; }
        }

        private readonly IQueryable<Entity> entities;
        
        public DynamicLinqTests()
        {
            entities = new List<Entity>()
            {
                new() { Id = Guid.NewGuid(), FirstName = "Paul", BirthDate = new LocalDate(1987,10,12) },
                new() { Id = Guid.NewGuid(), FirstName = "Abigail", BirthDate = new LocalDate(1970,02,13) },
                new() { Id = Guid.NewGuid(), FirstName = "Sophia", BirthDate = new LocalDate(1983,05,01) }
            }.AsQueryable();
        }
        
        [Fact]
        public void FilterByName()
        {
            var result = entities.AsQueryable().Where("FirstName == @0", "Paul").ToList();
            Assert.Single(result);
        }

        [Fact]
        public void FilterByDate_WithTypeConverter()
        {
            // comment this line then test will fail
            TypeDescriptor.AddAttributes(typeof(LocalDate), new TypeConverterAttribute(typeof(LocalDateConverter)));
            
            var result = entities.AsQueryable().Where("BirthDate == @0", "1987-10-12").ToList();
            Assert.Single(result);
        }
        
        [Fact]
        public void FilterByDate_WithDynamicExpressionParser()
        {
            // comment this line then test will fail
            TypeDescriptor.AddAttributes(typeof(LocalDate), new TypeConverterAttribute(typeof(LocalDateConverter)));

            var config = new ParsingConfig();
            var expr = DynamicExpressionParser.ParseLambda<Entity, bool>(config, false, "BirthDate == @0", "1987-10-12");
            
            var result = entities.AsQueryable().Where(expr).ToList();
            Assert.Single(result);
        }

        public class LocalDateConverter : TypeConverter
        {
            public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) => sourceType == typeof(string);
            
            public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
            {
                var result = LocalDatePattern.Iso.Parse((string)value);
                return result.Success
                    ? result.Value
                    : throw new FormatException(value?.ToString());
            }
            
            protected ParseResult<LocalDate> Convert(object value) => LocalDatePattern.Iso.Parse((string)value);
        }
    }
}
