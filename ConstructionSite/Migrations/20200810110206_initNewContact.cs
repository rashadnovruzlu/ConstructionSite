using Microsoft.EntityFrameworkCore.Migrations;

namespace ConstructionSite.Migrations
{
    public partial class initNewContact : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "lat",
                table: "Contacts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "lng",
                table: "Contacts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "lat",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "lng",
                table: "Contacts");
        }
    }
}
