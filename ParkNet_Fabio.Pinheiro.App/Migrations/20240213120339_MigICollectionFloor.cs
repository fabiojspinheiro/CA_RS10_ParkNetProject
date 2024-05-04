using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkNet_Fabio.Pinheiro.App.Migrations
{
    /// <inheritdoc />
    public partial class MigICollectionFloor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RowOfSpaces",
                table: "Floor",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowOfSpaces",
                table: "Floor");
        }
    }
}
