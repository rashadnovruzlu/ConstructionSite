using Microsoft.EntityFrameworkCore.Migrations;

namespace ConstructionSite.Migrations
{
    public partial class initialsed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Descriptions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Descriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContentAz = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ContentEn = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ContentRu = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SubServiceId = table.Column<int>(type: "int", nullable: false),
                    TittleAz = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    TittleEn = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    TittleRu = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Descriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Descriptions_SubServices_SubServiceId",
                        column: x => x.SubServiceId,
                        principalTable: "SubServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Descriptions_SubServiceId",
                table: "Descriptions",
                column: "SubServiceId");
        }
    }
}
