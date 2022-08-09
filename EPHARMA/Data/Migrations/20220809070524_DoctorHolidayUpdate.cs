using Microsoft.EntityFrameworkCore.Migrations;

namespace EPHARMA.Data.Migrations
{
    public partial class DoctorHolidayUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorHolidays_Customers_CustomerId",
                table: "DoctorHolidays");

            migrationBuilder.DropIndex(
                name: "IX_DoctorHolidays_CustomerId",
                table: "DoctorHolidays");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "DoctorHolidays");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "DoctorHolidays",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DoctorHolidays_CustomerId",
                table: "DoctorHolidays",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorHolidays_Customers_CustomerId",
                table: "DoctorHolidays",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
