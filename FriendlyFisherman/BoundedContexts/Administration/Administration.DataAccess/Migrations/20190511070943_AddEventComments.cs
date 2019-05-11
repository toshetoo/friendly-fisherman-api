using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Administration.DataAccess.Migrations
{
    public partial class AddEventComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventComments",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    EventId = table.Column<string>(nullable: true),
                    CreatorId = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventComments", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventComments");
        }
    }
}
