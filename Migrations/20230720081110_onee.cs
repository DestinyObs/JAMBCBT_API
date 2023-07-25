using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JAMBAPI.Migrations
{
    public partial class onee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lecturers_Users_StudentId",
                table: "Lecturers");

            migrationBuilder.DropIndex(
                name: "IX_Lecturers_StudentId",
                table: "Lecturers");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Lecturers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Lecturers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Lecturers_StudentId",
                table: "Lecturers",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lecturers_Users_StudentId",
                table: "Lecturers",
                column: "StudentId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
