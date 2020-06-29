using Microsoft.EntityFrameworkCore.Migrations;

namespace QuizApplication.Web.Migrations
{
    public partial class Addedanswertable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Questions",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "test",
                table: "Questions");

            migrationBuilder.RenameTable(
                name: "Questions",
                newName: "question");

            migrationBuilder.AddPrimaryKey(
                name: "PK_question",
                table: "question",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionId = table.Column<string>(nullable: true),
                    AnswerText = table.Column<string>(nullable: true),
                    IsCorrect = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_question",
                table: "question");

            migrationBuilder.RenameTable(
                name: "question",
                newName: "Questions");

            migrationBuilder.AddColumn<bool>(
                name: "test",
                table: "Questions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Questions",
                table: "Questions",
                column: "Id");
        }
    }
}
