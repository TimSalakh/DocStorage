using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.dal.migrations
{
    /// <inheritdoc />
    public partial class ConfirmationCode_Updated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConfirmationCode_User_UserId",
                table: "ConfirmationCode");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("409ecef4-8e43-4e14-a0a0-4d4f28f76425"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("852390ee-9a07-4710-a1cd-7eda7e0aae96"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("f7babaa4-c56b-4cf7-ae9e-5ebb01ad6cc8"));

            migrationBuilder.DropColumn(
                name: "DestinationEmail",
                table: "ConfirmationCode");

            migrationBuilder.AddColumn<DateTime>(
                name: "NextResendTime",
                table: "ConfirmationCode",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("4ed27d5e-0058-4294-84b5-97975bec9cac"), "Student" },
                    { new Guid("8d043ed0-6413-45e7-9535-a77a4f41c394"), "Teacher" },
                    { new Guid("d02ca366-35f4-44eb-b32a-2fb250320364"), "Admin" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ConfirmationCode_User_UserId",
                table: "ConfirmationCode",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConfirmationCode_User_UserId",
                table: "ConfirmationCode");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("4ed27d5e-0058-4294-84b5-97975bec9cac"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("8d043ed0-6413-45e7-9535-a77a4f41c394"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("d02ca366-35f4-44eb-b32a-2fb250320364"));

            migrationBuilder.DropColumn(
                name: "NextResendTime",
                table: "ConfirmationCode");

            migrationBuilder.AddColumn<string>(
                name: "DestinationEmail",
                table: "ConfirmationCode",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("409ecef4-8e43-4e14-a0a0-4d4f28f76425"), "Admin" },
                    { new Guid("852390ee-9a07-4710-a1cd-7eda7e0aae96"), "Teacher" },
                    { new Guid("f7babaa4-c56b-4cf7-ae9e-5ebb01ad6cc8"), "Student" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ConfirmationCode_User_UserId",
                table: "ConfirmationCode",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
