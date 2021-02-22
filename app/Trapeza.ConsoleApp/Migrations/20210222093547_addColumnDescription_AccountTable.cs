using Microsoft.EntityFrameworkCore.Migrations;

namespace Trapeza.ConsoleApp.Migrations
{
    public partial class addColumnDescription_AccountTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Account",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Account");
        }
    }
}
