using Microsoft.EntityFrameworkCore.Migrations;

namespace Publishing.DataAccess.Migrations
{
    public partial class CreateConnectionBetweenThreadANdSeen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ThreadId",
                table: "ThreadSeenCounts",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ThreadId",
                table: "ThreadReplies",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ThreadSeenCounts_ThreadId",
                table: "ThreadSeenCounts",
                column: "ThreadId");

            migrationBuilder.CreateIndex(
                name: "IX_ThreadReplies_ThreadId",
                table: "ThreadReplies",
                column: "ThreadId");

            migrationBuilder.AddForeignKey(
                name: "FK_ThreadReplies_Threads_ThreadId",
                table: "ThreadReplies",
                column: "ThreadId",
                principalTable: "Threads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ThreadSeenCounts_Threads_ThreadId",
                table: "ThreadSeenCounts",
                column: "ThreadId",
                principalTable: "Threads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThreadReplies_Threads_ThreadId",
                table: "ThreadReplies");

            migrationBuilder.DropForeignKey(
                name: "FK_ThreadSeenCounts_Threads_ThreadId",
                table: "ThreadSeenCounts");

            migrationBuilder.DropIndex(
                name: "IX_ThreadSeenCounts_ThreadId",
                table: "ThreadSeenCounts");

            migrationBuilder.DropIndex(
                name: "IX_ThreadReplies_ThreadId",
                table: "ThreadReplies");

            migrationBuilder.AlterColumn<string>(
                name: "ThreadId",
                table: "ThreadSeenCounts",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ThreadId",
                table: "ThreadReplies",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
