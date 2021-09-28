using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectManager.Data.Migrations
{
    public partial class editingteams : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonTeam_Team_TeamsId",
                table: "PersonTeam");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTeam_Team_TeamsId",
                table: "ProjectTeam");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Team",
                table: "Team");

            migrationBuilder.RenameTable(
                name: "Team",
                newName: "Teams");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Teams",
                table: "Teams",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonTeam_Teams_TeamsId",
                table: "PersonTeam",
                column: "TeamsId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTeam_Teams_TeamsId",
                table: "ProjectTeam",
                column: "TeamsId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonTeam_Teams_TeamsId",
                table: "PersonTeam");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTeam_Teams_TeamsId",
                table: "ProjectTeam");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Teams",
                table: "Teams");

            migrationBuilder.RenameTable(
                name: "Teams",
                newName: "Team");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Team",
                table: "Team",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonTeam_Team_TeamsId",
                table: "PersonTeam",
                column: "TeamsId",
                principalTable: "Team",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTeam_Team_TeamsId",
                table: "ProjectTeam",
                column: "TeamsId",
                principalTable: "Team",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
