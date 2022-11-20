using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.Migrations.MuseumData
{
    public partial class initMuseumDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExhibitionRoom",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameRoom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    File3D = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExhibitionRoom", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Museum",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MuseumName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Museum", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeOfArticle",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfArticle", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeOfArtifact",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameTypeArtifact = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfArtifact", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Aritifact",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MuseumId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TypeOfArtifactId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameArtifact = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    File3D = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiscoveryDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aritifact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Aritifact_Museum_MuseumId",
                        column: x => x.MuseumId,
                        principalTable: "Museum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Aritifact_TypeOfArtifact_TypeOfArtifactId",
                        column: x => x.TypeOfArtifactId,
                        principalTable: "TypeOfArtifact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Article",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ArtifactId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExhibitionRoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TypeOfArticleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TypeOfArtifactId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    State = table.Column<bool>(type: "bit", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Article", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Article_Aritifact_ArtifactId",
                        column: x => x.ArtifactId,
                        principalTable: "Aritifact",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Article_ExhibitionRoom_TypeOfArtifactId",
                        column: x => x.TypeOfArtifactId,
                        principalTable: "ExhibitionRoom",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Article_TypeOfArticle_TypeOfArtifactId",
                        column: x => x.TypeOfArtifactId,
                        principalTable: "TypeOfArticle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aritifact_MuseumId",
                table: "Aritifact",
                column: "MuseumId");

            migrationBuilder.CreateIndex(
                name: "IX_Aritifact_TypeOfArtifactId",
                table: "Aritifact",
                column: "TypeOfArtifactId");

            migrationBuilder.CreateIndex(
                name: "IX_Article_ArtifactId",
                table: "Article",
                column: "ArtifactId");

            migrationBuilder.CreateIndex(
                name: "IX_Article_TypeOfArtifactId",
                table: "Article",
                column: "TypeOfArtifactId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Article");

            migrationBuilder.DropTable(
                name: "Aritifact");

            migrationBuilder.DropTable(
                name: "ExhibitionRoom");

            migrationBuilder.DropTable(
                name: "TypeOfArticle");

            migrationBuilder.DropTable(
                name: "Museum");

            migrationBuilder.DropTable(
                name: "TypeOfArtifact");
        }
    }
}
