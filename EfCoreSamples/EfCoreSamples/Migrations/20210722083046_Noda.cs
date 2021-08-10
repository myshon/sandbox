using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;

namespace EfCoreSamples.Migrations
{
    public partial class Noda : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Instant>(
                name: "Audit_CreationTime",
                table: "TestAggregate",
                type: "timestamp",
                nullable: false,
                defaultValue: NodaTime.Instant.FromUnixTimeTicks(0L));

            migrationBuilder.AddColumn<Instant>(
                name: "Audit_UpdateTime",
                table: "TestAggregate",
                type: "timestamp",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Audit_CreationTime",
                table: "TestAggregate");

            migrationBuilder.DropColumn(
                name: "Audit_UpdateTime",
                table: "TestAggregate");
        }
    }
}
