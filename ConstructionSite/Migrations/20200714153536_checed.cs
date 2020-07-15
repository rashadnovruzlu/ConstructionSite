using Microsoft.EntityFrameworkCore.Migrations;

namespace ConstructionSite.Migrations
{
    
    public partial class checed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AboutID",
                table: "Images",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Images_AboutID",
                table: "Images",
                column: "AboutID");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Abouts_AboutID",
                table: "Images",
                column: "AboutID",
                principalTable: "Abouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Abouts_AboutID",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_AboutID",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "AboutID",
                table: "Images");
        }
    }
}
