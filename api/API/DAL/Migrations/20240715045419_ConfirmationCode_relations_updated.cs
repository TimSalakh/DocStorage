using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.dal.migrations
{
    /// <inheritdoc />
    public partial class ConfirmationCode_relations_updated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("77b71a21-2dee-42ce-a145-9c0e2447f266"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("89aeae1d-86d2-4545-990c-0fbd022e76b3"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("fd73971f-9bc7-4743-822d-4dff645925ef"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "ConfirmationCode",
                type: "uuid",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("409ecef4-8e43-4e14-a0a0-4d4f28f76425"), "Admin" },
                    { new Guid("852390ee-9a07-4710-a1cd-7eda7e0aae96"), "Teacher" },
                    { new Guid("f7babaa4-c56b-4cf7-ae9e-5ebb01ad6cc8"), "Student" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConfirmationCode_UserId",
                table: "ConfirmationCode",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConfirmationCode_User_UserId",
                table: "ConfirmationCode",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConfirmationCode_User_UserId",
                table: "ConfirmationCode");

            migrationBuilder.DropIndex(
                name: "IX_ConfirmationCode_UserId",
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
                name: "UserId",
                table: "ConfirmationCode");

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("77b71a21-2dee-42ce-a145-9c0e2447f266"), "Student" },
                    { new Guid("89aeae1d-86d2-4545-990c-0fbd022e76b3"), "Teacher" },
                    { new Guid("fd73971f-9bc7-4743-822d-4dff645925ef"), "Admin" }
                });
        }
    }
}
