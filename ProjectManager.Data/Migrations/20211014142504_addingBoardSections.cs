using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectManager.Data.Migrations
{
    public partial class addingBoardSections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("3f1764a5-0735-45c1-876e-9d158aec419b"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("97ffdcb4-d3e8-46b2-b7eb-9d7a9e6d053f"));

            migrationBuilder.AddColumn<Guid>(
                name: "BoardSectionId",
                table: "Tasks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "BoardSections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardSections", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("fe0a08fe-9588-460e-ab33-a0c84dcc3cbd"), "3b90c984-b212-44b6-a2d4-a327ad43adac", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("c6daa885-8ed2-4b4f-a7b7-8de06e5da300"), "ce8167f7-9667-439f-babb-7eee78ab0171", "User", "USER" });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_BoardSectionId",
                table: "Tasks",
                column: "BoardSectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_BoardSections_BoardSectionId",
                table: "Tasks",
                column: "BoardSectionId",
                principalTable: "BoardSections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_BoardSections_BoardSectionId",
                table: "Tasks");

            migrationBuilder.DropTable(
                name: "BoardSections");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_BoardSectionId",
                table: "Tasks");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("c6daa885-8ed2-4b4f-a7b7-8de06e5da300"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("fe0a08fe-9588-460e-ab33-a0c84dcc3cbd"));

            migrationBuilder.DropColumn(
                name: "BoardSectionId",
                table: "Tasks");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("3f1764a5-0735-45c1-876e-9d158aec419b"), "abdbfea1-96e2-4e9d-9393-0e4900af1661", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("97ffdcb4-d3e8-46b2-b7eb-9d7a9e6d053f"), "eb404f2f-75a9-4010-9e30-86e63a81a2c5", "User", "USER" });
        }
    }
}
