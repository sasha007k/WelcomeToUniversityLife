using Microsoft.EntityFrameworkCore.Migrations;

namespace WelcomeToUniversityLifeAspServer.Migrations
{
    public partial class initDb2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                "Photo",
                "University",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "Photo",
                "University");
        }
    }
}