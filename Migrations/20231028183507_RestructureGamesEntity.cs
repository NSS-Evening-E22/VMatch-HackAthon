using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Volunteer_Match_BE.Migrations
{
    public partial class RestructureGamesEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamGames_Teams_TeamOneId",
                table: "TeamGames");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamGames_Teams_TeamTwoId",
                table: "TeamGames");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamGames_Teams_WinningTeamId",
                table: "TeamGames");

            migrationBuilder.DropIndex(
                name: "IX_TeamGames_TeamOneId",
                table: "TeamGames");

            migrationBuilder.DropIndex(
                name: "IX_TeamGames_TeamTwoId",
                table: "TeamGames");

            migrationBuilder.DropColumn(
                name: "TeamOneId",
                table: "TeamGames");

            migrationBuilder.DropColumn(
                name: "TeamTwoId",
                table: "TeamGames");

            migrationBuilder.RenameColumn(
                name: "WinningTeamId",
                table: "TeamGames",
                newName: "TeamId");

            migrationBuilder.RenameIndex(
                name: "IX_TeamGames_WinningTeamId",
                table: "TeamGames",
                newName: "IX_TeamGames_TeamId");

            migrationBuilder.AddColumn<int>(
                name: "WinningTeamId",
                table: "Games",
                type: "integer",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamGames_Teams_TeamId",
                table: "TeamGames",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamGames_Teams_TeamId",
                table: "TeamGames");

            migrationBuilder.DropColumn(
                name: "WinningTeamId",
                table: "Games");

            migrationBuilder.RenameColumn(
                name: "TeamId",
                table: "TeamGames",
                newName: "WinningTeamId");

            migrationBuilder.RenameIndex(
                name: "IX_TeamGames_TeamId",
                table: "TeamGames",
                newName: "IX_TeamGames_WinningTeamId");

            migrationBuilder.AddColumn<int>(
                name: "TeamOneId",
                table: "TeamGames",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TeamTwoId",
                table: "TeamGames",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TeamGames_TeamOneId",
                table: "TeamGames",
                column: "TeamOneId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamGames_TeamTwoId",
                table: "TeamGames",
                column: "TeamTwoId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamGames_Teams_TeamOneId",
                table: "TeamGames",
                column: "TeamOneId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamGames_Teams_TeamTwoId",
                table: "TeamGames",
                column: "TeamTwoId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamGames_Teams_WinningTeamId",
                table: "TeamGames",
                column: "WinningTeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
