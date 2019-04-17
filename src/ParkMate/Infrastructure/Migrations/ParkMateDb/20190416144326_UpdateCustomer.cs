using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Infrastructure.Migrations.ParkMateDb
{
    public partial class UpdateCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Schedule_ScheduleId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_BookingHistory_BookingHistoryId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Schedule_BookingsId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_ParkingSpaces_BookingHistory_BookingHistoryId",
                table: "ParkingSpaces");

            migrationBuilder.DropForeignKey(
                name: "FK_ParkingSpaces_Schedule_ScheduleId",
                table: "ParkingSpaces");

            migrationBuilder.DropTable(
                name: "Schedule");

            migrationBuilder.DropIndex(
                name: "IX_ParkingSpaces_BookingHistoryId",
                table: "ParkingSpaces");

            migrationBuilder.DropIndex(
                name: "IX_ParkingSpaces_ScheduleId",
                table: "ParkingSpaces");

            migrationBuilder.DropIndex(
                name: "IX_Customers_BookingHistoryId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_BookingsId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "BookingHistoryId",
                table: "ParkingSpaces");

            migrationBuilder.DropColumn(
                name: "ScheduleId",
                table: "ParkingSpaces");

            migrationBuilder.DropColumn(
                name: "BookingHistoryId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "BookingsId",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "ScheduleId",
                table: "Bookings",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_ScheduleId",
                table: "Bookings",
                newName: "IX_Bookings_CustomerId");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Customers_CustomerId",
                table: "Bookings",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Customers_CustomerId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Bookings",
                newName: "ScheduleId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_CustomerId",
                table: "Bookings",
                newName: "IX_Bookings_ScheduleId");

            migrationBuilder.AddColumn<int>(
                name: "BookingHistoryId",
                table: "ParkingSpaces",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ScheduleId",
                table: "ParkingSpaces",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BookingHistoryId",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BookingsId",
                table: "Customers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Schedule",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedule", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParkingSpaces_BookingHistoryId",
                table: "ParkingSpaces",
                column: "BookingHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingSpaces_ScheduleId",
                table: "ParkingSpaces",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_BookingHistoryId",
                table: "Customers",
                column: "BookingHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_BookingsId",
                table: "Customers",
                column: "BookingsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Schedule_ScheduleId",
                table: "Bookings",
                column: "ScheduleId",
                principalTable: "Schedule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_BookingHistory_BookingHistoryId",
                table: "Customers",
                column: "BookingHistoryId",
                principalTable: "BookingHistory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Schedule_BookingsId",
                table: "Customers",
                column: "BookingsId",
                principalTable: "Schedule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingSpaces_BookingHistory_BookingHistoryId",
                table: "ParkingSpaces",
                column: "BookingHistoryId",
                principalTable: "BookingHistory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingSpaces_Schedule_ScheduleId",
                table: "ParkingSpaces",
                column: "ScheduleId",
                principalTable: "Schedule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
