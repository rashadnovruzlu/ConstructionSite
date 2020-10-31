using Microsoft.EntityFrameworkCore.Migrations;

namespace ConstructionSite.Migrations
{
    public partial class birikss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GaleryVidoId",
                table: "Galeries",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "GaleryVidos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VidoPath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GaleryVidos", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Galeries_GaleryVidos_GaleryVidoId",
                table: "Galeries");

            migrationBuilder.DropTable(
                name: "GaleryVidos");

            migrationBuilder.DropIndex(
                name: "IX_Galeries_GaleryVidoId",
                table: "Galeries");

            migrationBuilder.DropColumn(
                name: "GaleryVidoId",
                table: "Galeries");
        }
    }
}
