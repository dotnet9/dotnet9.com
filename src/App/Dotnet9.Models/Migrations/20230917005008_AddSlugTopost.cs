using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dotnet9.Models.Migrations
{
    /// <inheritdoc />
    public partial class AddSlugTopost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("f41d92e9-9752-4b07-836d-6377406e1069"));

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Posts",
                type: "character varying(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Email", "LockedTime", "LoginFailCount", "Pwd", "UpdateTime", "UserName" },
                values: new object[] { new Guid("eaa0c993-552b-4795-aee1-e5836fef6a98"), "qq1012434131@gmail.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "JAvlGPq9JyTdtvBO6x2llnRI1+gxwIyPqCKAn3THIKk=", null, "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("eaa0c993-552b-4795-aee1-e5836fef6a98"));

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Posts");

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Email", "LockedTime", "LoginFailCount", "Pwd", "UpdateTime", "UserName" },
                values: new object[] { new Guid("f41d92e9-9752-4b07-836d-6377406e1069"), "qq1012434131@gmail.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "JAvlGPq9JyTdtvBO6x2llnRI1+gxwIyPqCKAn3THIKk=", null, "admin" });
        }
    }
}
