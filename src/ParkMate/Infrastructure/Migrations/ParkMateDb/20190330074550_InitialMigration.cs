using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Infrastructure.Migrations.ParkMateDb
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SpaceAvailability",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    IsVisible = table.Column<bool>(nullable: false),
                    Monday_AvailableFrom = table.Column<TimeSpan>(nullable: false),
                    Monday_AvailableTo = table.Column<TimeSpan>(nullable: false),
                    Monday_IsAvailable = table.Column<bool>(nullable: false),
                    Tuesday_AvailableFrom = table.Column<TimeSpan>(nullable: false),
                    Tuesday_AvailableTo = table.Column<TimeSpan>(nullable: false),
                    Tuesday_IsAvailable = table.Column<bool>(nullable: false),
                    Wednesday_AvailableFrom = table.Column<TimeSpan>(nullable: false),
                    Wednesday_AvailableTo = table.Column<TimeSpan>(nullable: false),
                    Wednesday_IsAvailable = table.Column<bool>(nullable: false),
                    Thursday_AvailableFrom = table.Column<TimeSpan>(nullable: false),
                    Thursday_AvailableTo = table.Column<TimeSpan>(nullable: false),
                    Thursday_IsAvailable = table.Column<bool>(nullable: false),
                    Friday_AvailableFrom = table.Column<TimeSpan>(nullable: false),
                    Friday_AvailableTo = table.Column<TimeSpan>(nullable: false),
                    Friday_IsAvailable = table.Column<bool>(nullable: false),
                    Saturday_AvailableFrom = table.Column<TimeSpan>(nullable: false),
                    Saturday_AvailableTo = table.Column<TimeSpan>(nullable: false),
                    Saturday_IsAvailable = table.Column<bool>(nullable: false),
                    Sunday_AvailableFrom = table.Column<TimeSpan>(nullable: false),
                    Sunday_AvailableTo = table.Column<TimeSpan>(nullable: false),
                    Sunday_IsAvailable = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpaceAvailability", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ParkingSpaces",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    OwnerId = table.Column<string>(nullable: false),
                    Description_Title = table.Column<string>(nullable: false),
                    Description_Description = table.Column<string>(nullable: false),
                    Description_ImageURL = table.Column<string>(nullable: false),
                    Address_Street = table.Column<string>(nullable: false),
                    Address_City = table.Column<string>(nullable: false),
                    Address_State = table.Column<string>(nullable: false),
                    Address_Zip = table.Column<string>(nullable: false),
                    Address_Latitude = table.Column<double>(nullable: false),
                    Address_Longitude = table.Column<double>(nullable: false),
                    AvailabilityId = table.Column<int>(nullable: true),
                    BookingRate_HourlyRate_Value = table.Column<decimal>(nullable: false),
                    BookingRate_DailyRate_Value = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingSpaces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParkingSpaces_SpaceAvailability_AvailabilityId",
                        column: x => x.AvailabilityId,
                        principalTable: "SpaceAvailability",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParkingSpaces_AvailabilityId",
                table: "ParkingSpaces",
                column: "AvailabilityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParkingSpaces");

            migrationBuilder.DropTable(
                name: "SpaceAvailability");
        }
    }
}
