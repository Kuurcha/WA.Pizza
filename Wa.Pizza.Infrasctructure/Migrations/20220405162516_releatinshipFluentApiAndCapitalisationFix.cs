using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wa.Pizza.Core.Migrations
{
    public partial class releatinshipFluentApiAndCapitalisationFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUser_Adress_adressID",
                table: "ApplicationUser");

            migrationBuilder.DropForeignKey(
                name: "FK_BasketItem_Basket_BasketId",
                table: "BasketItem");

            migrationBuilder.DropIndex(
                name: "IX_OrderItem_CatalogItemId",
                table: "OrderItem");

            migrationBuilder.DropIndex(
                name: "IX_BasketItem_CatalogItemId",
                table: "BasketItem");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUser_adressID",
                table: "ApplicationUser");

            migrationBuilder.RenameColumn(
                name: "adressID",
                table: "ApplicationUser",
                newName: "AdressID");

            migrationBuilder.AlterColumn<int>(
                name: "BasketId",
                table: "BasketItem",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_CatalogItemId",
                table: "OrderItem",
                column: "CatalogItemId");

            migrationBuilder.CreateIndex(
                name: "IX_BasketItem_CatalogItemId",
                table: "BasketItem",
                column: "CatalogItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUser_AdressID",
                table: "ApplicationUser",
                column: "AdressID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUser_Adress_AdressID",
                table: "ApplicationUser",
                column: "AdressID",
                principalTable: "Adress",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItem_Basket_BasketId",
                table: "BasketItem",
                column: "BasketId",
                principalTable: "Basket",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUser_Adress_AdressID",
                table: "ApplicationUser");

            migrationBuilder.DropForeignKey(
                name: "FK_BasketItem_Basket_BasketId",
                table: "BasketItem");

            migrationBuilder.DropIndex(
                name: "IX_OrderItem_CatalogItemId",
                table: "OrderItem");

            migrationBuilder.DropIndex(
                name: "IX_BasketItem_CatalogItemId",
                table: "BasketItem");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUser_AdressID",
                table: "ApplicationUser");

            migrationBuilder.RenameColumn(
                name: "AdressID",
                table: "ApplicationUser",
                newName: "adressID");

            migrationBuilder.AlterColumn<int>(
                name: "BasketId",
                table: "BasketItem",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_CatalogItemId",
                table: "OrderItem",
                column: "CatalogItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BasketItem_CatalogItemId",
                table: "BasketItem",
                column: "CatalogItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUser_adressID",
                table: "ApplicationUser",
                column: "adressID");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUser_Adress_adressID",
                table: "ApplicationUser",
                column: "adressID",
                principalTable: "Adress",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItem_Basket_BasketId",
                table: "BasketItem",
                column: "BasketId",
                principalTable: "Basket",
                principalColumn: "Id");
        }
    }
}
