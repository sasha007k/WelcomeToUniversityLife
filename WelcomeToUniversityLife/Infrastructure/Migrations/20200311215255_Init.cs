using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserInfoID",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "DocumentId",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ZNOId",
                table: "Users",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ZNOId",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "UserInfoID",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
