using Microsoft.EntityFrameworkCore.Migrations;

namespace WelcomeToUniversityLifeAspServer.Migrations
{
    public partial class NavFacUni : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Speciality_FacultyId",
                table: "Speciality",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_Faculty_UniversityId",
                table: "Faculty",
                column: "UniversityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Faculty_University_UniversityId",
                table: "Faculty",
                column: "UniversityId",
                principalTable: "University",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Speciality_Faculty_FacultyId",
                table: "Speciality",
                column: "FacultyId",
                principalTable: "Faculty",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Faculty_University_UniversityId",
                table: "Faculty");

            migrationBuilder.DropForeignKey(
                name: "FK_Speciality_Faculty_FacultyId",
                table: "Speciality");

            migrationBuilder.DropIndex(
                name: "IX_Speciality_FacultyId",
                table: "Speciality");

            migrationBuilder.DropIndex(
                name: "IX_Faculty_UniversityId",
                table: "Faculty");
        }
    }
}
