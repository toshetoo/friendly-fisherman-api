using Microsoft.EntityFrameworkCore.Migrations;

namespace Publishing.DataAccess.Migrations
{
    public partial class AddCascadeDeleteSeenCountConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThreadSeenCounts_Threads_ThreadId",
                table: "ThreadSeenCounts");

            migrationBuilder.AddForeignKey(
                name: "FK_ThreadSeenCounts_Threads_ThreadId",
                table: "ThreadSeenCounts",
                column: "ThreadId",
                principalTable: "Threads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThreadSeenCounts_Threads_ThreadId",
                table: "ThreadSeenCounts");

            migrationBuilder.AddForeignKey(
                name: "FK_ThreadSeenCounts_Threads_ThreadId",
                table: "ThreadSeenCounts",
                column: "ThreadId",
                principalTable: "Threads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
