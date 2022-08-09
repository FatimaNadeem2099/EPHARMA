using Microsoft.EntityFrameworkCore.Migrations;

namespace EPHARMA.Data.Migrations
{
    public partial class DoctorTableUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Contact",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "CoverImage",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "DoctorAddress",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "DoctorLatitude",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "DoctorLogo",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "DoctorLongitude",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "IsBestRated",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "IsFeatured",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "Overview",
                table: "Doctors");

            migrationBuilder.AddColumn<string>(
                name: "CNIC",
                table: "Doctors",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ConsultancyCharges",
                table: "Doctors",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Doctors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Education",
                table: "Doctors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Doctors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfilePhoto",
                table: "Doctors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Services",
                table: "Doctors",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "YearsOfExperience",
                table: "Doctors",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CNIC",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "ConsultancyCharges",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "Education",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "ProfilePhoto",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "Services",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "YearsOfExperience",
                table: "Doctors");

            migrationBuilder.AddColumn<string>(
                name: "Contact",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CoverImage",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DoctorAddress",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "DoctorLatitude",
                table: "Doctors",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "DoctorLogo",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "DoctorLongitude",
                table: "Doctors",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "IsBestRated",
                table: "Doctors",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFeatured",
                table: "Doctors",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Overview",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
