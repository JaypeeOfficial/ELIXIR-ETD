using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ELIXIRETD.DATA.Migrations
{
    public partial class ChangeColumnId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reasons_MainMenus_MainMenuId",
                table: "Reasons");

            migrationBuilder.DropColumn(
                name: "MenuId",
                table: "Reasons");

            migrationBuilder.AlterColumn<int>(
                name: "MainMenuId",
                table: "Reasons",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reasons_MainMenus_MainMenuId",
                table: "Reasons",
                column: "MainMenuId",
                principalTable: "MainMenus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reasons_MainMenus_MainMenuId",
                table: "Reasons");

            migrationBuilder.AlterColumn<int>(
                name: "MainMenuId",
                table: "Reasons",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "MenuId",
                table: "Reasons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Reasons_MainMenus_MainMenuId",
                table: "Reasons",
                column: "MainMenuId",
                principalTable: "MainMenus",
                principalColumn: "Id");
        }
    }
}
