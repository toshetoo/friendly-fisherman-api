using Microsoft.EntityFrameworkCore.Migrations;

namespace Publishing.DataAccess.Migrations
{
    public partial class RenameContentFieldThreads : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Subtitle",
                table: "Threads",
                newName: "Content");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Threads",
                newName: "Subtitle");
        }
    }
}
