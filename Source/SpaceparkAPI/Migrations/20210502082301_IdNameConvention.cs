using Microsoft.EntityFrameworkCore.Migrations;

namespace SpaceparkAPI.Migrations
{
    public partial class IdNameConvention : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Parkings",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Parkings",
                newName: "ID");
        }
    }
}
