using Microsoft.EntityFrameworkCore.Migrations;

namespace Users.DataAccess.Migrations
{
    public partial class InitialSeedOfRoleAndUserRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "b3974a22-e113-4fb1-a7f1-492f151ab5f1");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e7db626f-0edb-4bec-a4b3-354ac26a51eb", "1aca8f36-54b9-45b2-b1cd-f47ab0396c44", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "ImagePath", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "a25c996b-4318-4bcb-8bef-6c777a8b5825", 0, null, "admin@ff.com", true, "Admin", null, "Adminov", false, null, "ADMIN@FF.COM", "ADMIN", "AOI191Ls8igwemUWnyFcFIMOXrtzIp7JIXmpRb3fc+X6sOg99YkoGEUl8/EUd8HcMw==", "99999999", true, null, false, "admin" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "e7db626f-0edb-4bec-a4b3-354ac26a51eb", "a25c996b-4318-4bcb-8bef-6c777a8b5825" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "e7db626f-0edb-4bec-a4b3-354ac26a51eb", "a25c996b-4318-4bcb-8bef-6c777a8b5825" });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "e7db626f-0edb-4bec-a4b3-354ac26a51eb");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a25c996b-4318-4bcb-8bef-6c777a8b5825");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "ImagePath", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "b3974a22-e113-4fb1-a7f1-492f151ab5f1", 0, null, "admin@ff.com", true, "Admin", null, "Adminov", false, null, null, null, null, null, false, null, false, "admin" });
        }
    }
}
