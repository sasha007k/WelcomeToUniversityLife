using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class InitialMigration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "Latitude",
                "University");

            migrationBuilder.DropColumn(
                "Longitude",
                "University");

            migrationBuilder.AddColumn<string>(
                "LocationLink",
                "University",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "LocationLink",
                "University");

            migrationBuilder.AddColumn<string>(
                "Latitude",
                "University",
                "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                "Longitude",
                "University",
                "nvarchar(max)",
                nullable: true);
        }
    }
}