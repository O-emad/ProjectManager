using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectManager.Data.Migrations
{
    public partial class linkSectionWithTask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sections_Projects_ProjectId",
                table: "Sections");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Projects_ProjectId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Sections_ProjectSectionId",
                table: "Tasks");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0054025c-42dc-4515-89ac-b6f93832e416"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d2af3b4a-0af0-4d2e-b3e7-2e4b0bc511d2"));

            migrationBuilder.RenameColumn(
                name: "ProjectSectionId",
                table: "Tasks",
                newName: "SectionId");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_ProjectSectionId",
                table: "Tasks",
                newName: "IX_Tasks_SectionId");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjectId",
                table: "Tasks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjectId",
                table: "Sections",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("3f1764a5-0735-45c1-876e-9d158aec419b"), "abdbfea1-96e2-4e9d-9393-0e4900af1661", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("97ffdcb4-d3e8-46b2-b7eb-9d7a9e6d053f"), "eb404f2f-75a9-4010-9e30-86e63a81a2c5", "User", "USER" });

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_Projects_ProjectId",
                table: "Sections",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Projects_ProjectId",
                table: "Tasks",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Sections_SectionId",
                table: "Tasks",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sections_Projects_ProjectId",
                table: "Sections");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Projects_ProjectId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Sections_SectionId",
                table: "Tasks");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("3f1764a5-0735-45c1-876e-9d158aec419b"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("97ffdcb4-d3e8-46b2-b7eb-9d7a9e6d053f"));

            migrationBuilder.RenameColumn(
                name: "SectionId",
                table: "Tasks",
                newName: "ProjectSectionId");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_SectionId",
                table: "Tasks",
                newName: "IX_Tasks_ProjectSectionId");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjectId",
                table: "Tasks",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjectId",
                table: "Sections",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("0054025c-42dc-4515-89ac-b6f93832e416"), "cff44619-5827-44f5-a7ef-d49dd9476678", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("d2af3b4a-0af0-4d2e-b3e7-2e4b0bc511d2"), "69728654-dfa9-4ea7-92ba-124d039fc347", "User", "USER" });

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_Projects_ProjectId",
                table: "Sections",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Projects_ProjectId",
                table: "Tasks",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Sections_ProjectSectionId",
                table: "Tasks",
                column: "ProjectSectionId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
