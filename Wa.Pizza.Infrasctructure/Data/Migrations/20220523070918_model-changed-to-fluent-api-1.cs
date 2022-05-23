using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wa.Pizza.Core.Migrations
{
    public partial class modelchangedtofluentapi1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 67);
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
                                2
                });
            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "Id", "ApplicationUserId", "CreationDate", "Description", "Status" },
                values: new object[,]
                {
                                { 67, 2, new DateTime(2186, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Post request test", 2 }
                });
        }
    }
}
