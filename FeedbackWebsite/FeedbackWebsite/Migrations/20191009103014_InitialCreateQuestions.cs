using Microsoft.EntityFrameworkCore.Migrations;

namespace FeedbackWebsite.Migrations
{
    public partial class InitialCreateQuestions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "QuestionTextModel",
                columns: new[] { "Id", "IsEnum", "QuestionText" },
                values: new object[,]
                {
                    { 1, true, "1. The presenter(s) possessed appropriate competences, knowledge and expertise in the field demonstrated on the event." },
                    { 2, true, "2. The presenter(s) guided the event effectively, taking interactive approach in keeping the audience attention focused." },
                    { 3, true, "3. The event framework was easy to follow with duration optimized to cover the planned themes and discussions." },
                    { 4, true, "4. The event was conducted in affirmative manner and pleasant atmosphere." },
                    { 5, true, "5. The organizers offered consultancy and assistance outside planned timeframe and after the event ending." },
                    { 6, true, "6. The materials and content were beneficial, offering new and knowledgeable information appropriate to my skills and experience level." },
                    { 7, true, "7. I could successfully implement the event information and knowledge in my daily job activities." },
                    { 8, true, "8. The event met my expectations and I found the overall program valuable and useful." },
                    { 9, true, "9. I would recommend this event to my colleagues." },
                    { 10, false, "Comments: Please give a brief comment on the things you liked on this event, and suggest ideas for improvement of the future events of this kind." }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "QuestionTextModel",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "QuestionTextModel",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "QuestionTextModel",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "QuestionTextModel",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "QuestionTextModel",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "QuestionTextModel",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "QuestionTextModel",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "QuestionTextModel",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "QuestionTextModel",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "QuestionTextModel",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
