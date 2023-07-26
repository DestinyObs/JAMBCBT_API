using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JAMBAPI.Migrations
{
    public partial class newesstttttttt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Lecturers_LecturerId",
                table: "Questions");

            migrationBuilder.AlterColumn<int>(
                name: "LecturerId",
                table: "Questions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "QuizQuestionId",
                table: "Options",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Options_QuizQuestionId",
                table: "Options",
                column: "QuizQuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Options_QuizQuestions_QuizQuestionId",
                table: "Options",
                column: "QuizQuestionId",
                principalTable: "QuizQuestions",
                principalColumn: "Id");

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
                name: "FK_Options_QuizQuestions_QuizQuestionId",
                table: "Options");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Lecturers_LecturerId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Options_QuizQuestionId",
                table: "Options");

            migrationBuilder.DropColumn(
                name: "QuizQuestionId",
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
