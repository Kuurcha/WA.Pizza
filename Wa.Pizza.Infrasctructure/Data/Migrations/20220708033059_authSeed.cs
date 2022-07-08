using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wa.Pizza.Core.Migrations
{
    public partial class authSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ApplicationUser",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "0cfd6406-58b5-47c9-9727-60c8e8c4a946", 0, "946a3ccf-4c9a-45ec-a12d-5a891c2e939b", null, false, false, null, null, null, null, null, false, "152cfbc2-8a38-49e7-890d-90121478f7e5", false, "Test2" },
                    { "aa1df5e9-59d4-402b-8694-1a022a9e4df7", 0, "4a2f4449-c3d1-4d68-b09b-de5f1f08aa6c", null, false, false, null, null, null, null, null, false, "69571162-2f70-41b0-b80f-b20a6e19e858", false, "Test" },
                    { "c5f7f70d-d516-493b-947c-eb4e81935b8c", 0, "1733ecef-d214-409d-9461-84df7002bccb", null, false, false, null, null, null, null, null, false, "be399f7a-be62-4fc6-9dc4-2ed85f7d1d90", false, "Test1" }
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
                    { 1, "Corusan 19", "aa1df5e9-59d4-402b-8694-1a022a9e4df7" },
                    { 2, "Omega-4", "c5f7f70d-d516-493b-947c-eb4e81935b8c" },
                    { 3, "Terra-4", "0cfd6406-58b5-47c9-9727-60c8e8c4a946" }
                });

            migrationBuilder.InsertData(
                table: "Basket",
                columns: new[] { "Id", "ApplicationUserId", "LastModified" },
                values: new object[,]
                {
                    { 1, "aa1df5e9-59d4-402b-8694-1a022a9e4df7", new DateTime(2050, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "c5f7f70d-d516-493b-947c-eb4e81935b8c", new DateTime(2186, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "0cfd6406-58b5-47c9-9727-60c8e8c4a946", new DateTime(4000, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "Id", "ApplicationUserId", "CreationDate", "Description", "Status" },
                values: new object[,]
                {
                    { 1, "c5f7f70d-d516-493b-947c-eb4e81935b8c", new DateTime(2186, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bring extra tomato sauce, don't be late, don't make Aria mad", 2 },
                    { 2, "0cfd6406-58b5-47c9-9727-60c8e8c4a946", new DateTime(4000, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Someone order pepperoni pizza into the Emperor's palace", 4 },
                    { 66, "aa1df5e9-59d4-402b-8694-1a022a9e4df7", new DateTime(2019, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "The republic will be reogranised into a first galactic empire", 3 }
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                keyValue: "0cfd6406-58b5-47c9-9727-60c8e8c4a946");

            migrationBuilder.DeleteData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: "aa1df5e9-59d4-402b-8694-1a022a9e4df7");

            migrationBuilder.DeleteData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: "c5f7f70d-d516-493b-947c-eb4e81935b8c");
        }
    }
}
