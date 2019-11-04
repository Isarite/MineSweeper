using Microsoft.EntityFrameworkCore.Migrations;

namespace MineServer.Migrations
{
    public partial class Initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Strategies_AspNetUsers_PlayerId",
                table: "Strategies");

            migrationBuilder.RenameColumn(
                name: "PlayerId",
                table: "Strategies",
                newName: "playerId");

            migrationBuilder.RenameIndex(
                name: "IX_Strategies_PlayerId",
                table: "Strategies",
                newName: "IX_Strategies_playerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Strategies_AspNetUsers_playerId",
                table: "Strategies",
                column: "playerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Strategies_AspNetUsers_playerId",
                table: "Strategies");

            migrationBuilder.RenameColumn(
                name: "playerId",
                table: "Strategies",
                newName: "PlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_Strategies_playerId",
                table: "Strategies",
                newName: "IX_Strategies_PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Strategies_AspNetUsers_PlayerId",
                table: "Strategies",
                column: "PlayerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
