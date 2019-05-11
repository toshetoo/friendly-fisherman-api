using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Administration.DataAccess.Migrations
{
    public partial class AddPolls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Polls",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    EndOn = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    Question = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Polls", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PollAnswers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    PollId = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PollAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PollAnswers_Polls_PollId",
                        column: x => x.PollId,
                        principalTable: "Polls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PollAnswers_PollId",
                table: "PollAnswers",
                column: "PollId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PollAnswers");

            migrationBuilder.DropTable(
                name: "Polls");
        }
    }
}
