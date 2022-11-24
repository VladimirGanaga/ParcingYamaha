using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParcingYamaha.Migrations
{
    /// <inheritdoc />
    public partial class parts2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "chapter",
                table: "Partsdatacollection",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "chapter",
                table: "Partsdatacollection");
        }
    }
}
