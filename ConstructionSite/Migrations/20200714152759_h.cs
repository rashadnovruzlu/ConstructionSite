using Microsoft.EntityFrameworkCore.Migrations;

namespace ConstructionSite.Migrations
{
    public partial class h : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_SubServices_SubServiceId",
                table: "Images");

            migrationBuilder.RenameColumn(
                name: "SubServiceId",
                table: "Images",
                newName: "SubServiceID");

            migrationBuilder.RenameIndex(
                name: "IX_Images_SubServiceId",
                table: "Images",
                newName: "IX_Images_SubServiceID");

            migrationBuilder.AlterColumn<int>(
                name: "SubServiceID",
                table: "Images",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_SubServices_SubServiceID",
                table: "Images",
                column: "SubServiceID",
                principalTable: "SubServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_SubServices_SubServiceID",
                table: "Images");

            migrationBuilder.RenameColumn(
                name: "SubServiceID",
                table: "Images",
                newName: "SubServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Images_SubServiceID",
                table: "Images",
                newName: "IX_Images_SubServiceId");

            migrationBuilder.AlterColumn<int>(
                name: "SubServiceId",
                table: "Images",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Images_SubServices_SubServiceId",
                table: "Images",
                column: "SubServiceId",
                principalTable: "SubServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
