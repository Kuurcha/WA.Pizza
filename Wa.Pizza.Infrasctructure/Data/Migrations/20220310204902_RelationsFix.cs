using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wa.Pizza.Core.Migrations
{
    public partial class RelationsFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUser_Basket_BasketId",
                table: "ApplicationUser");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUser_Order_OrderID",
                table: "ApplicationUser");

            migrationBuilder.DropForeignKey(
                name: "FK_BasketItem_Basket_basketId",
                table: "BasketItem");

            migrationBuilder.DropForeignKey(
                name: "FK_CatalogItem_BasketItem_BasketItemId",
                table: "CatalogItem");

            migrationBuilder.DropForeignKey(
                name: "FK_CatalogItem_OrderItem_orderItemid",
                table: "CatalogItem");

            migrationBuilder.DropIndex(
                name: "IX_CatalogItem_BasketItemId",
                table: "CatalogItem");

            migrationBuilder.DropIndex(
                name: "IX_CatalogItem_orderItemid",
                table: "CatalogItem");

            migrationBuilder.DropIndex(
                name: "IX_BasketItem_basketId",
                table: "BasketItem");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUser_BasketId",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "BasketItemId",
                table: "CatalogItem");

            migrationBuilder.DropColumn(
                name: "ShopOrderItemId",
                table: "CatalogItem");

            migrationBuilder.DropColumn(
                name: "orderItemid",
                table: "CatalogItem");

            migrationBuilder.DropColumn(
                name: "BasketId",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Adress");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "OrderItem",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "OrderItem",
                newName: "CatalogItemId");

            migrationBuilder.RenameColumn(
                name: "basketId",
                table: "BasketItem",
                newName: "CatalogItemId");

            migrationBuilder.RenameColumn(
                name: "OrderID",
                table: "ApplicationUser",
                newName: "adressID");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUser_OrderID",
                table: "ApplicationUser",
                newName: "IX_ApplicationUser_adressID");

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserId",
                table: "Order",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OrderItemId",
                table: "Order",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserId",
                table: "Basket",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BasketItem",
                table: "Basket",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "basketItemId",
                table: "Basket",
                type: "uniqueidentifier",
                nullable: true);

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
                name: "IX_Basket_ApplicationUserId",
                table: "Basket",
                column: "ApplicationUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Basket_basketItemId",
                table: "Basket",
                column: "basketItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUser_Adress_adressID",
                table: "ApplicationUser",
                column: "adressID",
                principalTable: "Adress",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Basket_ApplicationUser_ApplicationUserId",
                table: "Basket",
                column: "ApplicationUserId",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Basket_BasketItem_basketItemId",
                table: "Basket",
                column: "basketItemId",
                principalTable: "BasketItem",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItem_CatalogItem_CatalogItemId",
                table: "BasketItem",
                column: "CatalogItemId",
                principalTable: "CatalogItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_CatalogItem_CatalogItemId",
                table: "OrderItem",
                column: "CatalogItemId",
                principalTable: "CatalogItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUser_Adress_adressID",
                table: "ApplicationUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Basket_ApplicationUser_ApplicationUserId",
                table: "Basket");

            migrationBuilder.DropForeignKey(
                name: "FK_Basket_BasketItem_basketItemId",
                table: "Basket");

            migrationBuilder.DropForeignKey(
                name: "FK_BasketItem_CatalogItem_CatalogItemId",
                table: "BasketItem");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_CatalogItem_CatalogItemId",
                table: "OrderItem");

            migrationBuilder.DropIndex(
                name: "IX_OrderItem_CatalogItemId",
                table: "OrderItem");

            migrationBuilder.DropIndex(
                name: "IX_BasketItem_CatalogItemId",
                table: "BasketItem");

            migrationBuilder.DropIndex(
                name: "IX_Basket_ApplicationUserId",
                table: "Basket");

            migrationBuilder.DropIndex(
                name: "IX_Basket_basketItemId",
                table: "Basket");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "OrderItemId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Basket");

            migrationBuilder.DropColumn(
                name: "BasketItem",
                table: "Basket");

            migrationBuilder.DropColumn(
                name: "basketItemId",
                table: "Basket");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "OrderItem",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "CatalogItemId",
                table: "OrderItem",
                newName: "OrderId");

            migrationBuilder.RenameColumn(
                name: "CatalogItemId",
                table: "BasketItem",
                newName: "basketId");

            migrationBuilder.RenameColumn(
                name: "adressID",
                table: "ApplicationUser",
                newName: "OrderID");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUser_adressID",
                table: "ApplicationUser",
                newName: "IX_ApplicationUser_OrderID");

            migrationBuilder.AddColumn<Guid>(
                name: "BasketItemId",
                table: "CatalogItem",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ShopOrderItemId",
                table: "CatalogItem",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "orderItemid",
                table: "CatalogItem",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BasketId",
                table: "ApplicationUser",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserId",
                table: "Adress",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_CatalogItem_BasketItemId",
                table: "CatalogItem",
                column: "BasketItemId");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogItem_orderItemid",
                table: "CatalogItem",
                column: "orderItemid");

            migrationBuilder.CreateIndex(
                name: "IX_BasketItem_basketId",
                table: "BasketItem",
                column: "basketId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUser_BasketId",
                table: "ApplicationUser",
                column: "BasketId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUser_Basket_BasketId",
                table: "ApplicationUser",
                column: "BasketId",
                principalTable: "Basket",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUser_Order_OrderID",
                table: "ApplicationUser",
                column: "OrderID",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItem_Basket_basketId",
                table: "BasketItem",
                column: "basketId",
                principalTable: "Basket",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CatalogItem_BasketItem_BasketItemId",
                table: "CatalogItem",
                column: "BasketItemId",
                principalTable: "BasketItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CatalogItem_OrderItem_orderItemid",
                table: "CatalogItem",
                column: "orderItemid",
                principalTable: "OrderItem",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
