using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JAMBAPI.Migrations
{
    public partial class one : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Lecturers",
                newName: "Password");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Lecturers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Lecturers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Lecturers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVerified",
                table: "Lecturers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Lecturers");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Lecturers");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Lecturers");

            migrationBuilder.DropColumn(
                name: "IsVerified",
                table: "Lecturers");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Lecturers",
                newName: "Name");
        }
    }
}
