using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JAMBAPI.Migrations
{
    public partial class newesst : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Lecturers_LecturerId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "Explanation",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "IsCorrect",
                table: "Options");

            migrationBuilder.AlterColumn<int>(
                name: "LecturerId",
                table: "Questions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CorrectOptionIndex",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "OptionLetter",
                table: "Options",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Lecturers_LecturerId",
                table: "Questions",
                column: "LecturerId",
                principalTable: "Lecturers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Lecturers_LecturerId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "CorrectOptionIndex",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "OptionLetter",
                table: "Options");

            migrationBuilder.AlterColumn<int>(
                name: "LecturerId",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Explanation",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsCorrect",
                table: "Options",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Lecturers_LecturerId",
                table: "Questions",
                column: "LecturerId",
                principalTable: "Lecturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
