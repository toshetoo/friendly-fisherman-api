using Microsoft.EntityFrameworkCore.Migrations;

namespace Administration.DataAccess.Migrations
{
    public partial class AddCascadeDeleteForPolls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PollAnswers_Polls_PollId",
                table: "PollAnswers");

            migrationBuilder.AddForeignKey(
                name: "FK_PollAnswers_Polls_PollId",
                table: "PollAnswers",
                column: "PollId",
                principalTable: "Polls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PollAnswers_Polls_PollId",
                table: "PollAnswers");

            migrationBuilder.AddForeignKey(
                name: "FK_PollAnswers_Polls_PollId",
                table: "PollAnswers",
                column: "PollId",
                principalTable: "Polls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
