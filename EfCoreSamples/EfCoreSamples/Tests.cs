using System;
using System.Threading.Tasks;
using EfCoreSamples.Contracts;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using Npgsql;
using Xunit;

namespace EfCoreSamples
{
    public class Tests
    {
        public Tests()
        {
            
        }

        [Fact]
        public async Task InsertTypeWithDateTime()
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
            var options = builder.Options;

            await using var ctx = new SampleDbContext(options);
            await ctx.Database.EnsureDeletedAsync();
            await ctx.Database.EnsureCreatedAsync();
            //await ctx.Database.MigrateAsync();
                await ctx.Set<TestAggregate>().AddAsync(new TestAggregate(Guid.Empty,
                new Audit(Guid.Empty, Instant.FromUnixTimeTicks(0L), null, DateTimeOffset.Now, DateTime.Now),
                new LocalDate(2021, 12, 1)));
            await ctx.SaveChangesAsync();
        }
    }
}
