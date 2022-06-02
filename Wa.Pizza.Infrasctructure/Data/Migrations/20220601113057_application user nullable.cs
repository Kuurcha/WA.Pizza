using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wa.Pizza.Core.Migrations
{
    public partial class applicationusernullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Basket_ApplicationUser_ApplicationUserId",
                table: "Basket");

            migrationBuilder.DropIndex(
                name: "IX_Basket_ApplicationUserId",
                table: "Basket");

            migrationBuilder.AlterColumn<int>(
                name: "ApplicationUserId",
                table: "Basket",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Basket_ApplicationUserId",
                table: "Basket",
                column: "ApplicationUserId",
                unique: true,
                filter: "[ApplicationUserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Basket_ApplicationUser_ApplicationUserId",
                table: "Basket",
                column: "ApplicationUserId",
                principalTable: "ApplicationUser",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Basket_ApplicationUser_ApplicationUserId",
                table: "Basket");

            migrationBuilder.DropIndex(
                name: "IX_Basket_ApplicationUserId",
                table: "Basket");

            migrationBuilder.AlterColumn<int>(
                name: "ApplicationUserId",
                table: "Basket",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Basket_ApplicationUserId",
                table: "Basket",
                column: "ApplicationUserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Basket_ApplicationUser_ApplicationUserId",
                table: "Basket",
                column: "ApplicationUserId",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
