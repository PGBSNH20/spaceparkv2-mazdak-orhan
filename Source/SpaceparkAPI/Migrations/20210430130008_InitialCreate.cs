using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SpaceparkAPI.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Parkings",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Traveller = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StarShip = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalSum = table.Column<decimal>(type: "decimal(18,4)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parkings", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SpacePorts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpacePorts", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Parkings");

            migrationBuilder.DropTable(
                name: "SpacePorts");
        }
    }
}
