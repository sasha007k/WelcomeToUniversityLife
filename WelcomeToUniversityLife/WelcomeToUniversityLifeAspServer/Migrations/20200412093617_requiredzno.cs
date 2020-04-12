using Microsoft.EntityFrameworkCore.Migrations;

namespace WelcomeToUniversityLifeAspServer.Migrations
{
    public partial class requiredzno : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RequiredZNO3",
                table: "Speciality",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "RequiredZNO4",
                table: "Speciality",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequiredZNO4",
                table: "Speciality");

            migrationBuilder.AlterColumn<string>(
                name: "RequiredZNO3",
                table: "Speciality",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
