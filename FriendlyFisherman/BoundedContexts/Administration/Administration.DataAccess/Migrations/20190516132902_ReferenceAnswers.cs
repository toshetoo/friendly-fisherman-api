using Microsoft.EntityFrameworkCore.Migrations;

namespace Administration.DataAccess.Migrations
{
    public partial class ReferenceAnswers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnswerIndex",
                table: "UserPollAnswers");

            migrationBuilder.AddColumn<string>(
                name: "AnswerId",
                table: "UserPollAnswers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserPollAnswers_AnswerId",
                table: "UserPollAnswers",
                column: "AnswerId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPollAnswers_PollAnswers_AnswerId",
                table: "UserPollAnswers",
                column: "AnswerId",
                principalTable: "PollAnswers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPollAnswers_PollAnswers_AnswerId",
                table: "UserPollAnswers");

            migrationBuilder.DropIndex(
                name: "IX_UserPollAnswers_AnswerId",
                table: "UserPollAnswers");

            migrationBuilder.DropColumn(
                name: "AnswerId",
                table: "UserPollAnswers");

            migrationBuilder.AddColumn<int>(
                name: "AnswerIndex",
                table: "UserPollAnswers",
                nullable: false,
                defaultValue: 0);
        }
    }
}
