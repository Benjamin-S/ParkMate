using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations.ParkMateDb
{
    public partial class UpdateBooking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Customers_CustomerId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_CustomerId",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "BookingPeriod_Charge_Value",
                table: "Bookings",
                newName: "BookingInfo_Total_Value");

            migrationBuilder.RenameColumn(
                name: "BookingPeriod_Start",
                table: "Bookings",
                newName: "BookingInfo_Start");

            migrationBuilder.RenameColumn(
                name: "BookingPeriod_End",
                table: "Bookings",
                newName: "BookingInfo_End");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "Bookings",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId1",
                table: "Bookings",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BookingInfo_BillingUnit",
                table: "Bookings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BookingInfo_BookingUnits",
                table: "Bookings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "BookingInfo_Rate_Value",
                table: "Bookings",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_CustomerId1",
                table: "Bookings",
                column: "CustomerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Customers_CustomerId1",
                table: "Bookings",
                column: "CustomerId1",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Customers_CustomerId1",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_CustomerId1",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "CustomerId1",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "BookingInfo_BillingUnit",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "BookingInfo_BookingUnits",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "BookingInfo_Rate_Value",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "BookingInfo_Total_Value",
                table: "Bookings",
                newName: "BookingPeriod_Charge_Value");

            migrationBuilder.RenameColumn(
                name: "BookingInfo_Start",
                table: "Bookings",
                newName: "BookingPeriod_Start");

            migrationBuilder.RenameColumn(
                name: "BookingInfo_End",
                table: "Bookings",
                newName: "BookingPeriod_End");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "Bookings",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_CustomerId",
                table: "Bookings",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Customers_CustomerId",
                table: "Bookings",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
