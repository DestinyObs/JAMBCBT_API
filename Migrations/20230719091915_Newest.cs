using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JAMBAPI.Migrations
{
    public partial class Newest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Users",
                newName: "VerificationCode");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "Users",
                newName: "FullName");

            migrationBuilder.AddColumn<bool>(
                name: "IsVerified",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVerified",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "VerificationCode",
                table: "Users",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Users",
                newName: "Role");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
