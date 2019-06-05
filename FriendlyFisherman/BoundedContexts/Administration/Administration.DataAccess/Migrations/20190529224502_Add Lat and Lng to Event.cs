using Microsoft.EntityFrameworkCore.Migrations;

namespace Administration.DataAccess.Migrations
{
    public partial class AddLatandLngtoEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Lat",
                table: "Events",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Lng",
                table: "Events",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lat",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Lng",
                table: "Events");
        }
    }
}
