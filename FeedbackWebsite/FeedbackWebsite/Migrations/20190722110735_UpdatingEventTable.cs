using Microsoft.EntityFrameworkCore.Migrations;

namespace FeedbackWebsite.Migrations
{
    public partial class UpdatingEventTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Department",
                table: "EventInfoModel");

            migrationBuilder.DropColumn(
                name: "EmployeeName",
                table: "EventInfoModel");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "EventInfoModel");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "EventInfoModel",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeName",
                table: "EventInfoModel",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "EventInfoModel",
                nullable: false,
                defaultValue: "");
        }
    }
}
