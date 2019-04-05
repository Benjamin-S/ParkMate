using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations.ParkMateDb
{
    public partial class UpdateSchedule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Friday_DayOfWeek",
                table: "SpaceAvailability",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Monday_DayOfWeek",
                table: "SpaceAvailability",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Saturday_DayOfWeek",
                table: "SpaceAvailability",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Sunday_DayOfWeek",
                table: "SpaceAvailability",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Thursday_DayOfWeek",
                table: "SpaceAvailability",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Tuesday_DayOfWeek",
                table: "SpaceAvailability",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Wednesday_DayOfWeek",
                table: "SpaceAvailability",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Friday_DayOfWeek",
                table: "SpaceAvailability");

            migrationBuilder.DropColumn(
                name: "Monday_DayOfWeek",
                table: "SpaceAvailability");

            migrationBuilder.DropColumn(
                name: "Saturday_DayOfWeek",
                table: "SpaceAvailability");

            migrationBuilder.DropColumn(
                name: "Sunday_DayOfWeek",
                table: "SpaceAvailability");

            migrationBuilder.DropColumn(
                name: "Thursday_DayOfWeek",
                table: "SpaceAvailability");

            migrationBuilder.DropColumn(
                name: "Tuesday_DayOfWeek",
                table: "SpaceAvailability");

            migrationBuilder.DropColumn(
                name: "Wednesday_DayOfWeek",
                table: "SpaceAvailability");
        }
    }
}
