using System;
using EfCoreSamples.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NodaTime;

namespace EfCoreSamples
{
    public class TestAggregate
    {
        public Guid Id { get; }
        
        public Audit Audit { get; }
        
        public LocalDate Date { get; }

        public TestAggregate(Guid id, Audit audit, LocalDate date)
        {
            Id = id;
            Audit = audit;
            Date = date;
        }

        private TestAggregate()
        {
        }
    }
}
