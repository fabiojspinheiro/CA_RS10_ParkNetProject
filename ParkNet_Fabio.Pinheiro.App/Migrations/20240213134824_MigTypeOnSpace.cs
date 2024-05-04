using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkNet_Fabio.Pinheiro.App.Migrations
{
    /// <inheritdoc />
    public partial class MigTypeOnSpace : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "ParkingSpace",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ParkingSpace_TypeId",
                table: "ParkingSpace",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingSpace_VehicleTypes_TypeId",
                table: "ParkingSpace",
                column: "TypeId",
                principalTable: "VehicleTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParkingSpace_VehicleTypes_TypeId",
                table: "ParkingSpace");

            migrationBuilder.DropIndex(
                name: "IX_ParkingSpace_TypeId",
                table: "ParkingSpace");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "ParkingSpace");
        }
    }
}
