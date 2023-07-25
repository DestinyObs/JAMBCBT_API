using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JAMBAPI.Migrations
{
    public partial class newer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVerified",
                table: "Lecturers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsVerified",
                table: "Lecturers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
