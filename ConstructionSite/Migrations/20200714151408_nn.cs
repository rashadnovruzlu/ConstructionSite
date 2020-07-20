using Microsoft.EntityFrameworkCore.Migrations;

namespace ConstructionSite.Migrations
{
    public partial class nn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Descriptions_SubServiceId",
                table: "Descriptions");

            migrationBuilder.AddColumn<int>(
                name: "SubServiceId",
                table: "Images",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_SubServiceId",
                table: "Images",
                column: "SubServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Descriptions_SubServiceId",
                table: "Descriptions",
                column: "SubServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_SubServices_SubServiceId",
                table: "Images",
                column: "SubServiceId",
                principalTable: "SubServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_SubServices_SubServiceId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_SubServiceId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Descriptions_SubServiceId",
                table: "Descriptions");

            migrationBuilder.DropColumn(
                name: "SubServiceId",
                table: "Images");

            migrationBuilder.CreateIndex(
                name: "IX_Descriptions_SubServiceId",
                table: "Descriptions",
                column: "SubServiceId",
                unique: true);
        }
    }
}
