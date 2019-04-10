using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Infrastructure.Migrations.ParkMateDb
{
    public partial class UpdateCustomers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookingHistoryId",
                table: "ParkingSpaces",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "ParkingSpaces",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ScheduleId",
                table: "ParkingSpaces",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BookingHistory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingHistory", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    IdentityId = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    BookingsId = table.Column<int>(nullable: true),
                    BookingHistoryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_BookingHistory_BookingHistoryId",
                        column: x => x.BookingHistoryId,
                        principalTable: "BookingHistory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customers_Schedule_BookingsId",
                        column: x => x.BookingsId,
                        principalTable: "Schedule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Make = table.Column<string>(nullable: true),
                    Model = table.Column<string>(nullable: true),
                    Colour = table.Column<string>(nullable: true),
                    Registration = table.Column<string>(nullable: true),
                    CustomerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicles_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ParkingSpaceId = table.Column<int>(nullable: true),
                    VehicleId = table.Column<int>(nullable: true),
                    BookingPeriod_Start = table.Column<DateTime>(nullable: false),
                    BookingPeriod_End = table.Column<DateTime>(nullable: false),
                    BookingPeriod_Charge_Value = table.Column<decimal>(nullable: false),
                    BookingTime = table.Column<DateTime>(nullable: false),
                    BookingHistoryId = table.Column<int>(nullable: true),
                    ScheduleId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_BookingHistory_BookingHistoryId",
                        column: x => x.BookingHistoryId,
                        principalTable: "BookingHistory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bookings_ParkingSpaces_ParkingSpaceId",
                        column: x => x.ParkingSpaceId,
                        principalTable: "ParkingSpaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bookings_Schedule_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bookings_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParkingSpaces_BookingHistoryId",
                table: "ParkingSpaces",
                column: "BookingHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingSpaces_CustomerId",
                table: "ParkingSpaces",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingSpaces_ScheduleId",
                table: "ParkingSpaces",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_BookingHistoryId",
                table: "Bookings",
                column: "BookingHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ParkingSpaceId",
                table: "Bookings",
                column: "ParkingSpaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ScheduleId",
                table: "Bookings",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_VehicleId",
                table: "Bookings",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_BookingHistoryId",
                table: "Customers",
                column: "BookingHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_BookingsId",
                table: "Customers",
                column: "BookingsId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_CustomerId",
                table: "Vehicles",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingSpaces_BookingHistory_BookingHistoryId",
                table: "ParkingSpaces",
                column: "BookingHistoryId",
                principalTable: "BookingHistory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingSpaces_Customers_CustomerId",
                table: "ParkingSpaces",
                column: "CustomerId",
                principalTable: "Customers",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParkingSpaces_BookingHistory_BookingHistoryId",
                table: "ParkingSpaces");

            migrationBuilder.DropForeignKey(
                name: "FK_ParkingSpaces_Customers_CustomerId",
                table: "ParkingSpaces");

            migrationBuilder.DropForeignKey(
                name: "FK_ParkingSpaces_Schedule_ScheduleId",
                table: "ParkingSpaces");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "BookingHistory");

            migrationBuilder.DropTable(
                name: "Schedule");

            migrationBuilder.DropIndex(
                name: "IX_ParkingSpaces_BookingHistoryId",
                table: "ParkingSpaces");

            migrationBuilder.DropIndex(
                name: "IX_ParkingSpaces_CustomerId",
                table: "ParkingSpaces");

            migrationBuilder.DropIndex(
                name: "IX_ParkingSpaces_ScheduleId",
                table: "ParkingSpaces");

            migrationBuilder.DropColumn(
                name: "BookingHistoryId",
                table: "ParkingSpaces");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "ParkingSpaces");

            migrationBuilder.DropColumn(
                name: "ScheduleId",
                table: "ParkingSpaces");
        }
    }
}
