using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dotnet9.Models.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTagLenth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("f5a726be-12f3-47b0-9245-d4b77d41ea4f"));

            migrationBuilder.AlterColumn<string>(
                name: "TagName",
                table: "PostTags",
                type: "character varying(32)",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(10)",
                oldMaxLength: 10);

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Email", "LockedTime", "LoginFailCount", "Pwd", "UpdateTime", "UserName" },
                values: new object[] { new Guid("f41d92e9-9752-4b07-836d-6377406e1069"), "qq1012434131@gmail.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "JAvlGPq9JyTdtvBO6x2llnRI1+gxwIyPqCKAn3THIKk=", null, "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("f41d92e9-9752-4b07-836d-6377406e1069"));

            migrationBuilder.AlterColumn<string>(
                name: "TagName",
                table: "PostTags",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(32)",
                oldMaxLength: 32);

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Email", "LockedTime", "LoginFailCount", "Pwd", "UpdateTime", "UserName" },
                values: new object[] { new Guid("f5a726be-12f3-47b0-9245-d4b77d41ea4f"), "qq1012434131@gmail.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "JAvlGPq9JyTdtvBO6x2llnRI1+gxwIyPqCKAn3THIKk=", null, "admin" });
        }
    }
}
