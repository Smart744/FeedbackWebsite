using Microsoft.EntityFrameworkCore.Migrations;

namespace FeedbackWebsite.Migrations
{
    public partial class ChangingQuestionsTextName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AnswerText",
                table: "QuestionTextModel",
                newName: "QuestionText");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QuestionText",
                table: "QuestionTextModel",
                newName: "AnswerText");
        }
    }
}
