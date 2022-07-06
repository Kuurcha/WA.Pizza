using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wa.Pizza.Core.Migrations
{
    public partial class Authentication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adress_ApplicationUser_ApplicationUserId",
                table: "Adress");

            migrationBuilder.DropForeignKey(
                name: "FK_Basket_ApplicationUser_ApplicationUserId",
                table: "Basket");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_ApplicationUser_ApplicationUserId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Basket_ApplicationUserId",
                table: "Basket");

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
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Order"
            );
            migrationBuilder.AddColumn<string>(
                name: "CatalogItemName",
                table: "OrderItem",
                type: "nvarchar(254)",
                maxLength: 254,
                nullable: false
                );


            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Order"
            );

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Order",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Basket"
            );

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Basket",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: ""
             );

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ApplicationUser"
            );

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "ApplicationUser",
                type: "nvarchar(450)",
                nullable: false
            );

            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                table: "ApplicationUser",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "ApplicationUser",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "ApplicationUser",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "ApplicationUser",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                table: "ApplicationUser",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "ApplicationUser",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "ApplicationUser",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "ApplicationUser",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "ApplicationUser",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "ApplicationUser",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "ApplicationUser",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                table: "ApplicationUser",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                table: "ApplicationUser",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "ApplicationUser",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Adress"
            );

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Adress",
                type: "nvarchar(450)",
                nullable: true
            );

            migrationBuilder.AddColumn<string>(
                name: "Test",
                table: "Adress",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.ApplicationUserId);
                    table.ForeignKey(
                        name: "FK_RefreshToken_ApplicationUser_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Basket_ApplicationUserId",
                table: "Basket",
                column: "ApplicationUserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Adress_ApplicationUser_ApplicationUserId",
                table: "Adress",
                column: "ApplicationUserId",
                principalTable: "ApplicationUser",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Basket_ApplicationUser_ApplicationUserId",
                table: "Basket",
                column: "ApplicationUserId",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_ApplicationUser_ApplicationUserId",
                table: "Order",
                column: "ApplicationUserId",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adress_ApplicationUser_ApplicationUserId",
                table: "Adress");

            migrationBuilder.DropForeignKey(
                name: "FK_Basket_ApplicationUser_ApplicationUserId",
                table: "Basket");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_ApplicationUser_ApplicationUserId",
                table: "Order");

            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.DropIndex(
                name: "IX_Basket_ApplicationUserId",
                table: "Basket");

            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "Test",
                table: "Adress");

            migrationBuilder.DropColumn(
               name: "ApplicationUserId",
               table: "Order"
           );

            migrationBuilder.AddColumn<string>(
                name: "CatalogItemName",
                table: "OrderItem",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false);

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Order"
            );

            migrationBuilder.AddColumn<int>(
                name: "ApplicationUserId",
                table: "Order",
                type: "int",
                nullable: true
             );

            migrationBuilder.DropColumn(
               name: "ApplicationUserId",
               table: "Basket"
           );

            migrationBuilder.AddColumn<int>(
                name: "ApplicationUserId",
                table: "Basket",
                type: "int",
                nullable: true
            );

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ApplicationUser"
            );

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ApplicationUser",
                type: "int",
                nullable: false);

            migrationBuilder.DropColumn(
               name: "ApplicationUserId",
               table: "Basket"
            );

            migrationBuilder.AddColumn<int>(
                name: "ApplicationUserId",
                table: "Adress",
                type: "int",
                nullable: false,
                defaultValue: 0
                );

            migrationBuilder.InsertData(
                table: "ApplicationUser",
                column: "Id",
                values: new object[]
                {
                    1,
                    2,
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
                    { 1, "Corusan 19", 1 },
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
                    { 2, 3, new DateTime(4000, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Someone order pepperoni pizza into the Emperor's palace", 4 },
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

            migrationBuilder.CreateIndex(
                name: "IX_Basket_ApplicationUserId",
                table: "Basket",
                column: "ApplicationUserId",
                unique: true,
                filter: "[ApplicationUserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Adress_ApplicationUser_ApplicationUserId",
                table: "Adress",
                column: "ApplicationUserId",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Basket_ApplicationUser_ApplicationUserId",
                table: "Basket",
                column: "ApplicationUserId",
                principalTable: "ApplicationUser",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_ApplicationUser_ApplicationUserId",
                table: "Order",
                column: "ApplicationUserId",
                principalTable: "ApplicationUser",
                principalColumn: "Id");
        }
    }
}
