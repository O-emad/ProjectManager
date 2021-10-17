using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectManager.Data.Migrations
{
    public partial class addingBoardSections1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_BoardSections_BoardSectionId",
                table: "Tasks");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("c6daa885-8ed2-4b4f-a7b7-8de06e5da300"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("fe0a08fe-9588-460e-ab33-a0c84dcc3cbd"));

            migrationBuilder.AlterColumn<Guid>(
                name: "BoardSectionId",
                table: "Tasks",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("e8518445-2e7f-4b14-89a1-ca545ac8a7e3"), "7d3ce6e0-b258-4c74-8662-725163426514", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("dc72b019-0d14-4194-bb24-98b4b840346c"), "74479b39-00f3-419a-b262-34d1aca62c89", "User", "USER" });

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_BoardSections_BoardSectionId",
                table: "Tasks",
                column: "BoardSectionId",
                principalTable: "BoardSections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_BoardSections_BoardSectionId",
                table: "Tasks");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("dc72b019-0d14-4194-bb24-98b4b840346c"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e8518445-2e7f-4b14-89a1-ca545ac8a7e3"));

            migrationBuilder.AlterColumn<Guid>(
                name: "BoardSectionId",
                table: "Tasks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("fe0a08fe-9588-460e-ab33-a0c84dcc3cbd"), "3b90c984-b212-44b6-a2d4-a327ad43adac", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("c6daa885-8ed2-4b4f-a7b7-8de06e5da300"), "ce8167f7-9667-439f-babb-7eee78ab0171", "User", "USER" });

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_BoardSections_BoardSectionId",
                table: "Tasks",
                column: "BoardSectionId",
                principalTable: "BoardSections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
