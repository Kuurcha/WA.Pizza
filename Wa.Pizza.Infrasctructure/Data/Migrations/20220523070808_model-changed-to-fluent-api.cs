using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wa.Pizza.Core.Migrations
{
    public partial class modelchangedtofluentapi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Adress",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Adress",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Adress",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "BasketItem",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BasketItem",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BasketItem",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "BasketItem",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "BasketItem",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "OrderItem",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OrderItem",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "OrderItem",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "OrderItem",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "OrderItem",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Basket",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Basket",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Basket",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CatalogItem",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CatalogItem",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CatalogItem",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CatalogItem",
                keyColumn: "Id",
                keyValue: 2224);

            migrationBuilder.DeleteData(
                table: "CatalogItem",
                keyColumn: "Id",
                keyValue: 7567);

            migrationBuilder.DeleteData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: 3);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ApplicationUser",
                column: "Id",
                values: new object[]
                {
                    1,
                    3
                });

            migrationBuilder.InsertData(
                table: "CatalogItem",
                columns: new[] { "Id", "CatalogType", "Description", "Name", "Price", "Quantity" },
                values: new object[,]
                {
                    { 1, 3, "Regular clone trooper", "Clone trooper", 4000m, 3000000 },
                    { 2, 0, "With extra Tomato Sauce", "Tomato pizza", 100m, 500 },
                    { 3, 0, "Classic", "Pepperoni", 150m, 150 },
                    { 2224, 3, "Clone trooper commander", "Cody", 10000m, 1 },
                    { 7567, 3, "Clone trooper commander", "Rex", 10000m, 1 }
                });

            migrationBuilder.InsertData(
                table: "Adress",
                columns: new[] { "Id", "AdressString", "ApplicationUserId" },
                values: new object[,]
                {
                    { 1, "Corusant 19", 1 },
                    { 2, "Omega-4", 2 },
                    { 3, "Terra-4", 3 }
                });

            migrationBuilder.InsertData(
                table: "Basket",
                columns: new[] { "Id", "ApplicationUserId", "LastModified" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2050, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, new DateTime(2186, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 3, new DateTime(4000, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "Id", "ApplicationUserId", "CreationDate", "Description", "Status" },
                values: new object[,]
                {
                    { 1, 2, new DateTime(2186, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bring extra tomato sauce, don't be late, don't make Aria mad", 2 },
                    { 2, 3, new DateTime(4000, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Someone order pepperoni pizza into the Emperor's palace", 0 },
                    { 66, 1, new DateTime(2019, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "The republic will be reogranised into a first galactic empire", 3 }
                });

            migrationBuilder.InsertData(
                table: "BasketItem",
                columns: new[] { "Id", "BasketId", "CatalogItemId", "CatalogItemName", "CatalogType", "Quantity", "UnitPrice" },
                values: new object[,]
                {
                    { 1, 1, 7567, "Rex", 3, 1, 10000m },
                    { 2, 1, 2224, "Cody", 3, 1, 10000m },
                    { 3, 1, 1, "Clone trooper", 3, 3000000, 4000m },
                    { 4, 2, 2, "Tomato pizza", 0, 500, 100m },
                    { 5, 3, 3, "Classic", 0, 150, 150m }
                });

            migrationBuilder.InsertData(
                table: "OrderItem",
                columns: new[] { "Id", "CatalogItemId", "CatalogItemName", "Discount", "OrderId", "Quantity", "UnitPrice" },
                values: new object[,]
                {
                    { 1, 7567, "Rex", 1m, 66, 1, 10000m },
                    { 2, 2224, "Cody", 1m, 66, 1, 10000m },
                    { 3, 1, "Clone trooper", 0.8m, 66, 3000000, 4000m },
                    { 4, 2, "Tomato pizza", 0.01m, 1, 50, 100m },
                    { 5, 3, "Pepperoni", 0.99m, 2, 5, 150m }
                });
        }
    }
}
