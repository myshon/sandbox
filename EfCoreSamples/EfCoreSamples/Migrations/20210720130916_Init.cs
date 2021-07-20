using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;

namespace EfCoreSamples.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TestAggregate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Audit_CreatorId = table.Column<Guid>(type: "uuid", nullable: false),
                    Audit_CreationTime = table.Column<Instant>(type: "timestamp", nullable: false, defaultValue: NodaTime.Instant.FromUnixTimeTicks(0L)),
                    Audit_UpdateTime = table.Column<Instant>(type: "timestamp", nullable: true),
                    Audit_Timestamp = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValue: new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)),
                    Audit_Timestamp2 = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    Date = table.Column<LocalDate>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestAggregate", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestAggregate");
        }
    }
}
