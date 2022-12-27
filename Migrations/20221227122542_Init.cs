using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParcingYamaha.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ModelDB",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    prodPictureFileURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nickname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    modelName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    modelYear = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    modelTypeCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    productNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    colorType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    colorName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModelDB", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChapterDB",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModelsDBID = table.Column<int>(type: "int", nullable: false),
                    partFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    chapter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    chapterID = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChapterDB", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChapterDB_ModelDB_ModelsDBID",
                        column: x => x.ModelsDBID,
                        principalTable: "ModelDB",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PartDB",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    chapterID = table.Column<int>(type: "int", nullable: false),
                    chapterDBId = table.Column<int>(type: "int", nullable: true),
                    partNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    partName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    refNo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartDB", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PartDB_ChapterDB_chapterDBId",
                        column: x => x.chapterDBId,
                        principalTable: "ChapterDB",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChapterDB_ModelsDBID",
                table: "ChapterDB",
                column: "ModelsDBID");

            migrationBuilder.CreateIndex(
                name: "IX_PartDB_chapterDBId",
                table: "PartDB",
                column: "chapterDBId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PartDB");

            migrationBuilder.DropTable(
                name: "ChapterDB");

            migrationBuilder.DropTable(
                name: "ModelDB");
        }
    }
}
