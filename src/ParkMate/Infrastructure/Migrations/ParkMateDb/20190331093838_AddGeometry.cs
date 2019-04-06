using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

namespace Infrastructure.Migrations.ParkMateDb
{
    public partial class AddGeometry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address_Latitude",
                table: "ParkingSpaces");

            migrationBuilder.DropColumn(
                name: "Address_Longitude",
                table: "ParkingSpaces");

            migrationBuilder.AddColumn<Point>(
                name: "Address_Location",
                table: "ParkingSpaces",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address_Location",
                table: "ParkingSpaces");

            migrationBuilder.AddColumn<double>(
                name: "Address_Latitude",
                table: "ParkingSpaces",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Address_Longitude",
                table: "ParkingSpaces",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
