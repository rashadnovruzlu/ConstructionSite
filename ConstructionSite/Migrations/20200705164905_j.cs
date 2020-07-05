using Microsoft.EntityFrameworkCore.Migrations;

namespace ConstructionSite.Migrations
{
    public partial class j : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Description_SubServices_SubServiceId",
                table: "Description");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Description",
                table: "Description");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "Description");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Description");

            migrationBuilder.RenameTable(
                name: "Description",
                newName: "Descriptions");

            migrationBuilder.RenameIndex(
                name: "IX_Description_SubServiceId",
                table: "Descriptions",
                newName: "IX_Descriptions_SubServiceId");

            migrationBuilder.AlterColumn<string>(
                name: "Position",
                table: "CustomerFeedbacks",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "CustomerFeedbacks",
                maxLength: 35,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(35)",
                oldMaxLength: 35);

            migrationBuilder.AddColumn<string>(
                name: "ContentAz",
                table: "Descriptions",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContentEn",
                table: "Descriptions",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContentRu",
                table: "Descriptions",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TittleAz",
                table: "Descriptions",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TittleEn",
                table: "Descriptions",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TittleRu",
                table: "Descriptions",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Descriptions",
                table: "Descriptions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Descriptions_SubServices_SubServiceId",
                table: "Descriptions",
                column: "SubServiceId",
                principalTable: "SubServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Descriptions_SubServices_SubServiceId",
                table: "Descriptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Descriptions",
                table: "Descriptions");

            migrationBuilder.DropColumn(
                name: "ContentAz",
                table: "Descriptions");

            migrationBuilder.DropColumn(
                name: "ContentEn",
                table: "Descriptions");

            migrationBuilder.DropColumn(
                name: "ContentRu",
                table: "Descriptions");

            migrationBuilder.DropColumn(
                name: "TittleAz",
                table: "Descriptions");

            migrationBuilder.DropColumn(
                name: "TittleEn",
                table: "Descriptions");

            migrationBuilder.DropColumn(
                name: "TittleRu",
                table: "Descriptions");

            migrationBuilder.RenameTable(
                name: "Descriptions",
                newName: "Description");

            migrationBuilder.RenameIndex(
                name: "IX_Descriptions_SubServiceId",
                table: "Description",
                newName: "IX_Description_SubServiceId");

            migrationBuilder.AlterColumn<string>(
                name: "Position",
                table: "CustomerFeedbacks",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "CustomerFeedbacks",
                type: "nvarchar(35)",
                maxLength: 35,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 35,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Description",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Description",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Description",
                table: "Description",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Description_SubServices_SubServiceId",
                table: "Description",
                column: "SubServiceId",
                principalTable: "SubServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
