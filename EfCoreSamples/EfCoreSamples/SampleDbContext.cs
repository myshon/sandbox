using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NodaTime;
using Npgsql;

namespace EfCoreSamples
{
    public class SampleDbContextFactory : IDesignTimeDbContextFactory<SampleDbContext>
    {
        public SampleDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<SampleDbContext>();
            var connectionStringBuilder = new NpgsqlConnectionStringBuilder()
            {
                Host = "127.0.0.1",
                Port = 5432,
                Database = "tests",
                Username = "postgres",
                Password = "postgres"
            };
            builder.UseNpgsql(connectionStringBuilder.ConnectionString, b => b.UseNodaTime());
            return Activator.CreateInstance(typeof(SampleDbContext), builder.Options) as SampleDbContext;
        }
    }

    public class SampleDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TestAggregateMapping());
        }
        
        public SampleDbContext(DbContextOptions options) : base(options) { }
    }
    
    public class TestAggregateMapping : IEntityTypeConfiguration<TestAggregate>
    {
        public void Configure(EntityTypeBuilder<TestAggregate> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Date).IsRequired();
            builder.OwnsOne(x => x.Audit, o =>
            {
                o.Property(x => x.CreatorId).IsRequired();
                o.Property(x => x.CreationTime).IsRequired().HasDefaultValue(Instant.FromUnixTimeTicks(0L));
                o.Property(x => x.Timestamp).IsRequired().HasDefaultValue(DateTime.UnixEpoch);
                o.Property(x => x.Timestamp2);
                o.Property(x => x.UpdateTime);
            }).Navigation(x => x.Audit).IsRequired();
        }
    }
}
