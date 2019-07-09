using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FeedbackWebsite.Migrations
{
    public partial class AddingQuestionsText : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionsInfoModel");

            migrationBuilder.CreateTable(
                name: "AnswersInfoModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PersonId = table.Column<int>(nullable: false),
                    QuestionId = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    QuestionEnumAnswer = table.Column<int>(nullable: true),
                    AnswerTextAnswer = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswersInfoModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuestionTextModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsEnum = table.Column<bool>(nullable: false),
                    AnswerText = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionTextModel", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnswersInfoModel");

            migrationBuilder.DropTable(
                name: "QuestionTextModel");

            migrationBuilder.CreateTable(
                name: "QuestionsInfoModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Comment = table.Column<string>(nullable: true),
                    Question1 = table.Column<int>(nullable: false),
                    Question2 = table.Column<int>(nullable: false),
                    Question3 = table.Column<int>(nullable: false),
                    Question4 = table.Column<int>(nullable: false),
                    Question5 = table.Column<int>(nullable: false),
                    Question6 = table.Column<int>(nullable: false),
                    Question7 = table.Column<int>(nullable: false),
                    Question8 = table.Column<int>(nullable: false),
                    Question9 = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionsInfoModel", x => x.Id);
                });
        }
    }
}
