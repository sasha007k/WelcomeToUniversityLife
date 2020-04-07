using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class InitialMigration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "University");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "University");

            migrationBuilder.AddColumn<string>(
                name: "LocationLink",
                table: "University",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocationLink",
                table: "University");

            migrationBuilder.AddColumn<string>(
                name: "Latitude",
                table: "University",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Longitude",
                table: "University",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
