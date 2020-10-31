using Microsoft.EntityFrameworkCore.Migrations;

namespace ConstructionSite.Migrations
{
    public partial class biriksghl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Galeries_GaleryVidos_GaleryVidoId",
                table: "Galeries");

            migrationBuilder.DropIndex(
                name: "IX_Galeries_GaleryVidoId",
                table: "Galeries");

            migrationBuilder.DropColumn(
                name: "GaleryVidoId",
                table: "Galeries");

            migrationBuilder.AddColumn<int>(
                name: "GaleryId",
                table: "GaleryVidos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_GaleryVidos_GaleryId",
                table: "GaleryVidos",
                column: "GaleryId");

            migrationBuilder.AddForeignKey(
                name: "FK_GaleryVidos_Galeries_GaleryId",
                table: "GaleryVidos",
                column: "GaleryId",
                principalTable: "Galeries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GaleryVidos_Galeries_GaleryId",
                table: "GaleryVidos");

            migrationBuilder.DropIndex(
                name: "IX_GaleryVidos_GaleryId",
                table: "GaleryVidos");

            migrationBuilder.DropColumn(
                name: "GaleryId",
                table: "GaleryVidos");

            migrationBuilder.AddColumn<int>(
                name: "GaleryVidoId",
                table: "Galeries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Galeries_GaleryVidoId",
                table: "Galeries",
                column: "GaleryVidoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Galeries_GaleryVidos_GaleryVidoId",
                table: "Galeries",
                column: "GaleryVidoId",
                principalTable: "GaleryVidos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
