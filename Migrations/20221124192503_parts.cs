using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParcingYamaha.Migrations
{
    /// <inheritdoc />
    public partial class parts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Modeldatacollection",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    prodPictureFileURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nickname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    modelName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    modelYear = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    modelTypeCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    productNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    colorType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    modelBaseCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    colorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    calledCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    vinNoSearch = table.Column<bool>(type: "bit", nullable: true),
                    prodPictureNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    releaseYymm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    modelComment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    prodCategory = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modeldatacollection", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Partsdatacollection",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    modeldatacollectionID = table.Column<int>(type: "int", nullable: false),
                    partNewsFileURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    selectableId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    partNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    partName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    appSerial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    refNo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partsdatacollection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Partsdatacollection_Modeldatacollection_modeldatacollectionID",
                        column: x => x.modeldatacollectionID,
                        principalTable: "Modeldatacollection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Partsdatacollection_modeldatacollectionID",
                table: "Partsdatacollection",
                column: "modeldatacollectionID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Partsdatacollection");

            migrationBuilder.DropTable(
                name: "Modeldatacollection");
        }
    }
}
