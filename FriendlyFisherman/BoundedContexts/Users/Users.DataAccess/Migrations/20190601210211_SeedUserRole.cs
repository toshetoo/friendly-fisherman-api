using Microsoft.EntityFrameworkCore.Migrations;

namespace Users.DataAccess.Migrations
{
    public partial class SeedUserRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "12da2c97-0655-4b4f-8f2d-1138aed72aa6", "974bb73f-9834-406e-93cd-ebe7eac6ba43", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2dba9841-c2d7-46bd-8006-ad4d109e2c37", "96578a79-a0b8-4079-8595-e293f0f0447f", "User", "USER" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "ImagePath", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1820df53-5b29-4be9-a65b-56a5037c8ffc", 0, null, "admin@ff.com", true, "Admin", null, "Adminov", false, null, "ADMIN@FF.COM", "ADMIN", "ACtX2q+2zrEpSYxFBqHKv0C63lZus1YL3gqXmR6iobntI9ltprz5VhWFT9izZFYKUQ==", "99999999", true, null, false, "admin" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "12da2c97-0655-4b4f-8f2d-1138aed72aa6", "1820df53-5b29-4be9-a65b-56a5037c8ffc" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "2dba9841-c2d7-46bd-8006-ad4d109e2c37");

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "12da2c97-0655-4b4f-8f2d-1138aed72aa6", "1820df53-5b29-4be9-a65b-56a5037c8ffc" });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "12da2c97-0655-4b4f-8f2d-1138aed72aa6");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "1820df53-5b29-4be9-a65b-56a5037c8ffc");

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
    }
}
