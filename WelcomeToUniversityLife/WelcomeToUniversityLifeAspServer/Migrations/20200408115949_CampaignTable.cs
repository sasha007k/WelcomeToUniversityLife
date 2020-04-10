using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WelcomeToUniversityLifeAspServer.Migrations
{
    public partial class CampaignTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Campaigns",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Start = table.Column<DateTime>(),
                    End = table.Column<DateTime>(),
                    Status = table.Column<int>()
                },
                constraints: table => { table.PrimaryKey("PK_Campaigns", x => x.Id); });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Campaigns");
        }
    }
}