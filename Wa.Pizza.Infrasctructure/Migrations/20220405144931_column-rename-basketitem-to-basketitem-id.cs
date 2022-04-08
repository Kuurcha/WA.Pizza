using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wa.Pizza.Core.Migrations
{
    public partial class columnrenamebasketitemtobasketitemid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Basket_BasketItem_basketItemId",
                table: "Basket");

            migrationBuilder.DropColumn(
                name: "BasketItem",
                table: "Basket");

            migrationBuilder.RenameColumn(
                name: "basketItemId",
                table: "Basket",
                newName: "BasketItemId");

            migrationBuilder.RenameIndex(
                name: "IX_Basket_basketItemId",
                table: "Basket",
                newName: "IX_Basket_BasketItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Basket_BasketItem_BasketItemId",
                table: "Basket",
                column: "BasketItemId",
                principalTable: "BasketItem",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Basket_BasketItem_BasketItemId",
                table: "Basket");

            migrationBuilder.RenameColumn(
                name: "BasketItemId",
                table: "Basket",
                newName: "basketItemId");

            migrationBuilder.RenameIndex(
                name: "IX_Basket_BasketItemId",
                table: "Basket",
                newName: "IX_Basket_basketItemId");

            migrationBuilder.AddColumn<int>(
                name: "BasketItem",
                table: "Basket",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Basket_BasketItem_basketItemId",
                table: "Basket",
                column: "basketItemId",
                principalTable: "BasketItem",
                principalColumn: "Id");
        }
    }
}
