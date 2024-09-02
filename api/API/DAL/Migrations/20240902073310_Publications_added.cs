using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.dal.migrations
{
    /// <inheritdoc />
    public partial class Publications_added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "Publication",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    PublicationType = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsConfirmed = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    ConfirmatorId = table.Column<Guid>(type: "uuid", nullable: false),
                    ConfirmationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publication", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Publication_User_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Publication_User_ConfirmatorId",
                        column: x => x.ConfirmatorId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0ad34212-b0f2-4de8-8e9d-16716a366163"), "Student" },
                    { new Guid("91576ec7-121b-4ecb-890b-d07fc9bd2b75"), "Admin" },
                    { new Guid("e745e137-9ba6-438b-b23c-971cb266edfb"), "Teacher" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Publication_AuthorId",
                table: "Publication",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Publication_ConfirmatorId",
                table: "Publication",
                column: "ConfirmatorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Publication");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("0ad34212-b0f2-4de8-8e9d-16716a366163"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("91576ec7-121b-4ecb-890b-d07fc9bd2b75"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("e745e137-9ba6-438b-b23c-971cb266edfb"));

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("4ed27d5e-0058-4294-84b5-97975bec9cac"), "Student" },
                    { new Guid("8d043ed0-6413-45e7-9535-a77a4f41c394"), "Teacher" },
                    { new Guid("d02ca366-35f4-44eb-b32a-2fb250320364"), "Admin" }
                });
        }
    }
}
