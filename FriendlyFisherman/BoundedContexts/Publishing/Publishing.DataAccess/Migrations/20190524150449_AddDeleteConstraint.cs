using Microsoft.EntityFrameworkCore.Migrations;

namespace Publishing.DataAccess.Migrations
{
    public partial class AddDeleteConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThreadReplies_Threads_ThreadId",
                table: "ThreadReplies");

            migrationBuilder.AddForeignKey(
                name: "FK_ThreadReplies_Threads_ThreadId",
                table: "ThreadReplies",
                column: "ThreadId",
                principalTable: "Threads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThreadReplies_Threads_ThreadId",
                table: "ThreadReplies");

            migrationBuilder.AddForeignKey(
                name: "FK_ThreadReplies_Threads_ThreadId",
                table: "ThreadReplies",
                column: "ThreadId",
                principalTable: "Threads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
