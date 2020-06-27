using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ConstructionSite.Migrations
{
    public partial class intials : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Abouts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TittleAz = table.Column<string>(maxLength: 250, nullable: false),
                    TittleRu = table.Column<string>(maxLength: 250, nullable: true),
                    TittleEn = table.Column<string>(maxLength: 250, nullable: true),
                    ContentAz = table.Column<string>(nullable: false),
                    ContentRu = table.Column<string>(nullable: true),
                    ContentEn = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abouts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TittleAz = table.Column<string>(maxLength: 150, nullable: false),
                    TittleEn = table.Column<string>(maxLength: 150, nullable: true),
                    TittleRu = table.Column<string>(maxLength: 150, nullable: true),
                    ContentAz = table.Column<string>(nullable: false),
                    ContentEn = table.Column<string>(nullable: true),
                    ContentRu = table.Column<string>(nullable: true),
                    Address = table.Column<string>(maxLength: 150, nullable: false),
                    Email = table.Column<string>(maxLength: 150, nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerFeedbacks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContentAz = table.Column<string>(maxLength: 500, nullable: false),
                    ContentEn = table.Column<string>(maxLength: 500, nullable: true),
                    ContentRu = table.Column<string>(maxLength: 500, nullable: true),
                    FullName = table.Column<string>(maxLength: 35, nullable: false),
                    Position = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerFeedbacks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 75, nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    Subject = table.Column<string>(maxLength: 150, nullable: false),
                    UserMessage = table.Column<string>(maxLength: 2000, nullable: false),
                    SendDate = table.Column<DateTime>(nullable: false),
                    IsAnswerd = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TittleAz = table.Column<string>(maxLength: 150, nullable: false),
                    TittleEn = table.Column<string>(maxLength: 150, nullable: true),
                    TittleRu = table.Column<string>(maxLength: 150, nullable: true),
                    ContentAz = table.Column<string>(nullable: false),
                    ContentEn = table.Column<string>(nullable: true),
                    ContentRu = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Portfolios",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAz = table.Column<string>(maxLength: 75, nullable: false),
                    NameEn = table.Column<string>(maxLength: 75, nullable: true),
                    NameRu = table.Column<string>(maxLength: 75, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Portfolios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAz = table.Column<string>(maxLength: 75, nullable: false),
                    NameEn = table.Column<string>(maxLength: 75, nullable: true),
                    NameRu = table.Column<string>(maxLength: 75, nullable: true),
                    TittleAz = table.Column<string>(nullable: false),
                    TittleEn = table.Column<string>(nullable: true),
                    TittleRu = table.Column<string>(nullable: true),
                    ImageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StaticFields",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(maxLength: 25, nullable: false),
                    Value = table.Column<string>(maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaticFields", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAz = table.Column<string>(maxLength: 75, nullable: false),
                    NameEn = table.Column<string>(maxLength: 75, nullable: true),
                    NameRu = table.Column<string>(maxLength: 75, nullable: true),
                    ContentAz = table.Column<string>(nullable: false),
                    ContentRu = table.Column<string>(nullable: true),
                    ContentEn = table.Column<string>(nullable: true),
                    PortfolioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_Portfolios_PortfolioId",
                        column: x => x.PortfolioId,
                        principalTable: "Portfolios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 150, nullable: true),
                    Path = table.Column<string>(maxLength: 250, nullable: false),
                    ServiceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubServices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAz = table.Column<string>(maxLength: 75, nullable: false),
                    NameEn = table.Column<string>(maxLength: 75, nullable: true),
                    NameRu = table.Column<string>(maxLength: 75, nullable: true),
                    ContentAz = table.Column<string>(nullable: false),
                    ContentRu = table.Column<string>(nullable: true),
                    ContentEn = table.Column<string>(nullable: true),
                    ServiceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubServices_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AboutImages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AboutId = table.Column<int>(nullable: false),
                    ImageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboutImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AboutImages_Abouts_AboutId",
                        column: x => x.AboutId,
                        principalTable: "Abouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AboutImages_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HomePages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomePages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HomePages_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NewsImages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsMain = table.Column<bool>(nullable: false),
                    NewsId = table.Column<int>(nullable: false),
                    ImageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NewsImages_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NewsImages_News_NewsId",
                        column: x => x.NewsId,
                        principalTable: "News",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectImages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsMain = table.Column<bool>(nullable: false),
                    ImageId = table.Column<int>(nullable: false),
                    ProjectId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectImages_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectImages_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubServiceImages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsMain = table.Column<bool>(nullable: false),
                    ImageId = table.Column<int>(nullable: false),
                    SubServiceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubServiceImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubServiceImages_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubServiceImages_SubServices_SubServiceId",
                        column: x => x.SubServiceId,
                        principalTable: "SubServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AboutImages_AboutId",
                table: "AboutImages",
                column: "AboutId");

            migrationBuilder.CreateIndex(
                name: "IX_AboutImages_ImageId",
                table: "AboutImages",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_HomePages_ImageId",
                table: "HomePages",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_ServiceId",
                table: "Images",
                column: "ServiceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NewsImages_ImageId",
                table: "NewsImages",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsImages_NewsId",
                table: "NewsImages",
                column: "NewsId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectImages_ImageId",
                table: "ProjectImages",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectImages_ProjectId",
                table: "ProjectImages",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_PortfolioId",
                table: "Projects",
                column: "PortfolioId");

            migrationBuilder.CreateIndex(
                name: "IX_SubServiceImages_ImageId",
                table: "SubServiceImages",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_SubServiceImages_SubServiceId",
                table: "SubServiceImages",
                column: "SubServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_SubServices_ServiceId",
                table: "SubServices",
                column: "ServiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AboutImages");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "CustomerFeedbacks");

            migrationBuilder.DropTable(
                name: "HomePages");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "NewsImages");

            migrationBuilder.DropTable(
                name: "ProjectImages");

            migrationBuilder.DropTable(
                name: "StaticFields");

            migrationBuilder.DropTable(
                name: "SubServiceImages");

            migrationBuilder.DropTable(
                name: "Abouts");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "SubServices");

            migrationBuilder.DropTable(
                name: "Portfolios");

            migrationBuilder.DropTable(
                name: "Services");
        }
    }
}
