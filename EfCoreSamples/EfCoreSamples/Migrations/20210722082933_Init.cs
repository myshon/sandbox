using System;
using Microsoft.EntityFrameworkCore.Migrations;

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
                    Audit_Timestamp = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValue: new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)),
                    Audit_Timestamp2 = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
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
