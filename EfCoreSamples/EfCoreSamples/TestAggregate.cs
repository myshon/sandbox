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

        public TestAggregate(Guid id, Audit audit)
        {
            Id = id;
            Audit = audit;
        }

        private TestAggregate()
        {
        }
    }
}
