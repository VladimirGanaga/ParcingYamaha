using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParcingYamaha.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
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
                    nickname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    modelName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    modelYear = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    modelTypeCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    productNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    colorType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    modelBaseCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    colorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    calledCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    vinNoSearch = table.Column<bool>(type: "bit", nullable: false),
                    prodPictureNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    releaseYymm = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    modelComment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    prodCategory = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modeldatacollection", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Modeldatacollection");
        }
    }
}
