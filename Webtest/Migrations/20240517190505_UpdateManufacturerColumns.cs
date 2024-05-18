using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Webtest.Migrations
{
    /// <inheritdoc />
    public partial class UpdateManufacturerColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "manufacturer_name",
                table: "manufacturer",
                newName: "ManufacturerName");

            migrationBuilder.RenameColumn(
                name: "manufacturer_country",
                table: "manufacturer",
                newName: "ManufacturerCountry");

            migrationBuilder.RenameColumn(
                name: "manufacturer_id",
                table: "manufacturer",
                newName: "ManufacturerId");

            migrationBuilder.RenameColumn(
                name: "manufacturer_id",
                table: "car",
                newName: "ManufacturerId");

            migrationBuilder.RenameColumn(
                name: "car_year",
                table: "car",
                newName: "CarYear");

            migrationBuilder.RenameColumn(
                name: "car_name",
                table: "car",
                newName: "CarName");

            migrationBuilder.RenameColumn(
                name: "car_id",
                table: "car",
                newName: "CarId");

            migrationBuilder.RenameIndex(
                name: "IX_car_manufacturer_id",
                table: "car",
                newName: "IX_car_ManufacturerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ManufacturerName",
                table: "manufacturer",
                newName: "manufacturer_name");

            migrationBuilder.RenameColumn(
                name: "ManufacturerCountry",
                table: "manufacturer",
                newName: "manufacturer_country");

            migrationBuilder.RenameColumn(
                name: "ManufacturerId",
                table: "manufacturer",
                newName: "manufacturer_id");

            migrationBuilder.RenameColumn(
                name: "ManufacturerId",
                table: "car",
                newName: "manufacturer_id");

            migrationBuilder.RenameColumn(
                name: "CarYear",
                table: "car",
                newName: "car_year");

            migrationBuilder.RenameColumn(
                name: "CarName",
                table: "car",
                newName: "car_name");

            migrationBuilder.RenameColumn(
                name: "CarId",
                table: "car",
                newName: "car_id");

            migrationBuilder.RenameIndex(
                name: "IX_car_ManufacturerId",
                table: "car",
                newName: "IX_car_manufacturer_id");
        }
    }
}
