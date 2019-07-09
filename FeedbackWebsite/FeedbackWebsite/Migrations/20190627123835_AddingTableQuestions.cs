using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FeedbackWebsite.Migrations
{
    public partial class AddingTableQuestions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuestionsInfoModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                        
                    Question1 = table.Column<string>(nullable: true),
                    Question2 = table.Column<string>(nullable: true),
                    Question3 = table.Column<string>(nullable: true),
                    Question4 = table.Column<string>(nullable: true),
                    Question5 = table.Column<string>(nullable: true),
                    Question6 = table.Column<string>(nullable: true),
                    Question7 = table.Column<string>(nullable: true),
                    Question8 = table.Column<string>(nullable: true),
                    Question9 = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true)
                }
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionsInfoModel");
        }
    }
}
