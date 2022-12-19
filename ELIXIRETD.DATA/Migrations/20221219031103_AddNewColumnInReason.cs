using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ELIXIRETD.DATA.Migrations
{
    public partial class AddNewColumnInReason : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MainMenuId",
                table: "Reasons",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MenuId",
                table: "Reasons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reasons_MainMenuId",
                table: "Reasons",
                column: "MainMenuId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reasons_MainMenus_MainMenuId",
                table: "Reasons",
                column: "MainMenuId",
                principalTable: "MainMenus",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reasons_MainMenus_MainMenuId",
                table: "Reasons");

            migrationBuilder.DropIndex(
                name: "IX_Reasons_MainMenuId",
                table: "Reasons");

            migrationBuilder.DropColumn(
                name: "MainMenuId",
                table: "Reasons");

            migrationBuilder.DropColumn(
                name: "MenuId",
                table: "Reasons");
        }
    }
}
