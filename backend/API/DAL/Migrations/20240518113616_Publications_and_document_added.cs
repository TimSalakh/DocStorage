using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.dal.migrations
{
    /// <inheritdoc />
    public partial class Publications_and_document_added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "6c2dc596-bebd-417d-8226-a5bff2688cdc");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "b5725046-5923-4d16-abb3-7855d0a0247d");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "f48b0f70-a886-44b3-a32a-8c850d3a7f77");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "f954f761-0775-4718-a5fb-5c418737acf8");

            migrationBuilder.RenameColumn(
                name: "Bio",
                table: "AspNetUsers",
                newName: "GroupTag");

            migrationBuilder.AlterColumn<string>(
                name: "Patronymic",
                table: "AspNetUsers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateTable(
                name: "Publication",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
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
                        name: "FK_Publication_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Publication_AspNetUsers_ConfirmatorId",
                        column: x => x.ConfirmatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Document",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    PublicationId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Document", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Document_Publication_PublicationId",
                        column: x => x.PublicationId,
                        principalTable: "Publication",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5ec2a705-ed95-41ad-8cc2-abdf25dea9df", null, "Administrator", "ADMINISTRATOR" },
                    { "74d7131e-48f0-4775-baac-4a366bb64132", null, "Teacher", "TEACHER" },
                    { "bbcc8aba-1904-4e74-842e-df65e732d212", null, "Student", "STUDENT" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Document_PublicationId",
                table: "Document",
                column: "PublicationId");

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
                name: "Document");

            migrationBuilder.DropTable(
                name: "Publication");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "5ec2a705-ed95-41ad-8cc2-abdf25dea9df");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "74d7131e-48f0-4775-baac-4a366bb64132");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "bbcc8aba-1904-4e74-842e-df65e732d212");

            migrationBuilder.RenameColumn(
                name: "GroupTag",
                table: "AspNetUsers",
                newName: "Bio");

            migrationBuilder.AlterColumn<string>(
                name: "Patronymic",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6c2dc596-bebd-417d-8226-a5bff2688cdc", null, "Student", "STUDENT" },
                    { "b5725046-5923-4d16-abb3-7855d0a0247d", null, "Administrator", "ADMINISTRATOR" },
                    { "f48b0f70-a886-44b3-a32a-8c850d3a7f77", null, "Guest", "GUEST" },
                    { "f954f761-0775-4718-a5fb-5c418737acf8", null, "Teacher", "TEACHER" }
                });
        }
    }
}
