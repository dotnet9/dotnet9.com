using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dotnet9.Models.Migrations
{
    /// <inheritdoc />
    public partial class AddShortIdForPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("eaa0c993-552b-4795-aee1-e5836fef6a98"));

            migrationBuilder.AddColumn<string>(
                name: "ShortId",
                table: "Posts",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Email", "LockedTime", "LoginFailCount", "Pwd", "UpdateTime", "UserName" },
                values: new object[] { new Guid("4b3a94e9-2c0f-400c-894b-e85e70bf0565"), "qq1012434131@gmail.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "JAvlGPq9JyTdtvBO6x2llnRI1+gxwIyPqCKAn3THIKk=", null, "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("4b3a94e9-2c0f-400c-894b-e85e70bf0565"));

            migrationBuilder.DropColumn(
                name: "ShortId",
                table: "Posts");

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Email", "LockedTime", "LoginFailCount", "Pwd", "UpdateTime", "UserName" },
                values: new object[] { new Guid("eaa0c993-552b-4795-aee1-e5836fef6a98"), "qq1012434131@gmail.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "JAvlGPq9JyTdtvBO6x2llnRI1+gxwIyPqCKAn3THIKk=", null, "admin" });
        }
    }
}
