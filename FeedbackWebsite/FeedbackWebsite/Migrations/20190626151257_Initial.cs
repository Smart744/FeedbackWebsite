using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FeedbackWebsite.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventInfoModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmployeeName = table.Column<string>(),
                    Department = table.Column<string>(),
                    Position = table.Column<string>(),
                    EventName = table.Column<string>(),
                    EventOrg = table.Column<string>(nullable: true),
                    PresentersName = table.Column<string>(nullable: true),
                    EventLocation = table.Column<string>(nullable: true),
                    EventStartDate = table.Column<DateTime>(nullable: false),
                    EventEndDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventInfoModel", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventInfoModel");
        }
    }
}
