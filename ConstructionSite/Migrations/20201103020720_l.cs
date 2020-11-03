using Microsoft.EntityFrameworkCore.Migrations;

namespace ConstructionSite.Migrations
{
    public partial class l : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GaleryVido_Galeries_GaleryId",
                table: "GaleryVido");

            migrationBuilder.AddForeignKey(
                name: "FK_GaleryVido_Galeries_GaleryId",
                table: "GaleryVido",
                column: "GaleryId",
                principalTable: "Galeries",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GaleryVido_Galeries_GaleryId",
                table: "GaleryVido");

            migrationBuilder.AddForeignKey(
                name: "FK_GaleryVido_Galeries_GaleryId",
                table: "GaleryVido",
                column: "GaleryId",
                principalTable: "Galeries",
                principalColumn: "Id");
        }
    }
}
