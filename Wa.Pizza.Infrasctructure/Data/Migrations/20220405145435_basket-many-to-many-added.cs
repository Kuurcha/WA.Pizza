using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wa.Pizza.Core.Migrations
{
    public partial class basketmanytomanyadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Basket_BasketItem_BasketItemId",
                table: "Basket");

            migrationBuilder.DropIndex(
                name: "IX_Basket_BasketItemId",
                table: "Basket");

            migrationBuilder.DropColumn(
                name: "BasketItemId",
                table: "Basket");

            migrationBuilder.AddColumn<int>(
                name: "BasketId",
                table: "BasketItem",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BasketItem_BasketId",
                table: "BasketItem",
                column: "BasketId");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItem_Basket_BasketId",
                table: "BasketItem",
                column: "BasketId",
                principalTable: "Basket",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketItem_Basket_BasketId",
                table: "BasketItem");

            migrationBuilder.DropIndex(
                name: "IX_BasketItem_BasketId",
                table: "BasketItem");

            migrationBuilder.DropColumn(
                name: "BasketId",
                table: "BasketItem");

            migrationBuilder.AddColumn<int>(
                name: "BasketItemId",
                table: "Basket",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Basket_BasketItemId",
                table: "Basket",
                column: "BasketItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Basket_BasketItem_BasketItemId",
                table: "Basket",
                column: "BasketItemId",
                principalTable: "BasketItem",
                principalColumn: "Id");
        }
    }
}
