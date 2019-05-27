using Microsoft.EntityFrameworkCore.Migrations;

namespace Users.DataAccess.Migrations
{
    public partial class AddedDeleteBySenderandDeletedByReceiverfieldstoPersonalMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DeletedByReceiver",
                table: "UserPersonalMessages",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "DeletedBySender",
                table: "UserPersonalMessages",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedByReceiver",
                table: "UserPersonalMessages");

            migrationBuilder.DropColumn(
                name: "DeletedBySender",
                table: "UserPersonalMessages");
        }
    }
}
