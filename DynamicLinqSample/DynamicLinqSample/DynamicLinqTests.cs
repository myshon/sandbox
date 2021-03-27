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
            public LocalDate? BirthDateNullable { get; set; }
            public DateTime? EmployedAt { get; set; }
            public Duration StayLength { get; set; }
        }

        private readonly IQueryable<Entity> entities;
        
        public DynamicLinqTests()
        {
            var pattern = DurationPattern.CreateWithInvariantCulture("-H:mm:ss.FFFFFFFFF");
            entities = new List<Entity>()
            {
                new() { Id = Guid.NewGuid(), FirstName = "Paul", BirthDate = new LocalDate(1987,10,12), BirthDateNullable = new LocalDate(1987,10,12), StayLength = pattern.Parse("9:30:00").Value, EmployedAt = DateTime.Now},
                new() { Id = Guid.NewGuid(), FirstName = "Abigail", BirthDate = new LocalDate(1970,02,13), StayLength = pattern.Parse("12:00:00").Value },
                new() { Id = Guid.NewGuid(), FirstName = "Sophia", BirthDate = new LocalDate(1983,05,01), StayLength = pattern.Parse("10:30:00").Value }
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
           // TypeDescriptor.AddAttributes(typeof(LocalDate), new TypeConverterAttribute(typeof(LocalDateConverter)));
            
            var result = entities.AsQueryable().Where("BirthDate == @0", "1987-10-12").ToList();
            Assert.Single(result);
        }

        [Fact] 
        public void FilterByNullableDate_WithTypeConverter()
        {
            // comment this line then test will fail
            // TypeDescriptor.AddAttributes(typeof(LocalDate), new TypeConverterAttribute(typeof(LocalDateConverter)));
            
            var result = entities.AsQueryable().Where("BirthDateNullable == @0", "1987-10-12").ToList();
            Assert.Single(result);
        }
        
        [Fact]
        public void FilterByDate_WithDynamicExpressionParser()
        {
            // comment this line then test will fail

            var config = new ParsingConfig()
            {
                TypeConverters = new Dictionary<Type, TypeConverter>()
                {
                    { typeof(LocalDate), new LocalDateConverter() }
                }
            };
            var expr = DynamicExpressionParser.ParseLambda<Entity, bool>(config, false, "BirthDate == @0", "1987-10-12");
            var result = entities.AsQueryable().Where(expr).ToList();
            Assert.Single(result);
            
            expr = DynamicExpressionParser.ParseLambda<Entity, bool>(config, false, "BirthDateNullable == @0", "1987-10-12");
            result = entities.AsQueryable().Where(expr).ToList();
            Assert.Single(result);
            
            expr = DynamicExpressionParser.ParseLambda<Entity, bool>(config, false, "EmployedAt != null");
            result = entities.AsQueryable().Where(expr).ToList();
            Assert.Single(result);
            
            expr = DynamicExpressionParser.ParseLambda<Entity, bool>(config, false, "BirthDateNullable != null");
            result = entities.AsQueryable().Where(expr).ToList();
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
        //
        // public class DurationCustomConverter : TypeConverter
        // {
        //     public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) => sourceType == typeof(string);
        //     
        //     public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        //     {
        //         var result = DurationPattern.CreateWithInvariantCulture("-H:mm:ss.FFFFFFFFF").Parse((string)value);
        //         return result.Success
        //             ? result.Value
        //             : throw new FormatException(value?.ToString());
        //     }
        //     
        //     protected ParseResult<LocalDate> Convert(object value) => LocalDatePattern.Iso.Parse((string)value);
        // }
    }
}
