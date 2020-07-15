using Microsoft.EntityFrameworkCore.Migrations;

namespace ConstructionSite.Migrations
{
    public partial class added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Abouts_AboutID",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_SubServices_SubServiceID",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_AboutID",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_SubServiceID",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "AboutID",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "SubServiceID",
                table: "Images");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AboutID",
                table: "Images",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SubServiceID",
                table: "Images",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Images_AboutID",
                table: "Images",
                column: "AboutID");

            migrationBuilder.CreateIndex(
                name: "IX_Images_SubServiceID",
                table: "Images",
                column: "SubServiceID");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Abouts_AboutID",
                table: "Images",
                column: "AboutID",
                principalTable: "Abouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_SubServices_SubServiceID",
                table: "Images",
                column: "SubServiceID",
                principalTable: "SubServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
