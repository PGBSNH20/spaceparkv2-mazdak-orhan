using Microsoft.EntityFrameworkCore.Migrations;

namespace SpaceparkAPI.Migrations
{
    public partial class AddedSpacePortId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SpacePortId",
                table: "Parkings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Parkings_SpacePortId",
                table: "Parkings",
                column: "SpacePortId");

            migrationBuilder.AddForeignKey(
                name: "FK_Parkings_SpacePorts_SpacePortId",
                table: "Parkings",
                column: "SpacePortId",
                principalTable: "SpacePorts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parkings_SpacePorts_SpacePortId",
                table: "Parkings");

            migrationBuilder.DropIndex(
                name: "IX_Parkings_SpacePortId",
                table: "Parkings");

            migrationBuilder.DropColumn(
                name: "SpacePortId",
                table: "Parkings");
        }
    }
}
