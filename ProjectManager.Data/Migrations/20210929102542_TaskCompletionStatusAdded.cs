using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectManager.Data.Migrations
{
    public partial class TaskCompletionStatusAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CompletionStatus",
                table: "Tasks",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompletionStatus",
                table: "Tasks");
        }
    }
}
