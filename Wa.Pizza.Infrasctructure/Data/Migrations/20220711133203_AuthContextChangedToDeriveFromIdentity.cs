using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wa.Pizza.Core.Migrations
{
    public partial class AuthContextChangedToDeriveFromIdentity : Migration
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

            migrationBuilder.DropForeignKey(
                name: "FK_RefreshToken_ApplicationUser_ApplicationUserId",
                table: "RefreshToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUser",
                table: "ApplicationUser");

            migrationBuilder.RenameTable(
                name: "ApplicationUser",
                newName: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NormalizedUserName",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NormalizedEmail",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "8c1427dc-c42f-4a6c-b524-ee22522c5e7d", 0, "d25ffae8-eaba-4ac5-b76e-7bf2525b6531", null, false, false, null, null, null, null, null, false, "00fad370-6511-41af-9abf-6fb4a3e1fc6f", false, "Test1" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "d45f7c4a-fb87-40e9-9421-68b0a78d2771", 0, "c28f4b35-08e0-44f1-a1ef-65f83aad553b", null, false, false, null, null, null, null, null, false, "87bc4e8b-dfb9-40a3-b32e-ec46b25a304a", false, "Test" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "dbb10eb9-b665-45d7-8f43-fbbdbf1c16a2", 0, "d935796c-9849-47b5-b6bb-449ed14e1f81", null, false, false, null, null, null, null, null, false, "eab048c0-8b27-4b87-aa5a-11e011191e73", false, "Test2" });

            migrationBuilder.UpdateData(
                table: "Adress",
                keyColumn: "Id",
                keyValue: 1,
                column: "ApplicationUserId",
                value: "d45f7c4a-fb87-40e9-9421-68b0a78d2771");

            migrationBuilder.UpdateData(
                table: "Adress",
                keyColumn: "Id",
                keyValue: 2,
                column: "ApplicationUserId",
                value: "8c1427dc-c42f-4a6c-b524-ee22522c5e7d");

            migrationBuilder.UpdateData(
                table: "Adress",
                keyColumn: "Id",
                keyValue: 3,
                column: "ApplicationUserId",
                value: "dbb10eb9-b665-45d7-8f43-fbbdbf1c16a2");

            migrationBuilder.UpdateData(
                table: "Basket",
                keyColumn: "Id",
                keyValue: 1,
                column: "ApplicationUserId",
                value: "d45f7c4a-fb87-40e9-9421-68b0a78d2771");

            migrationBuilder.UpdateData(
                table: "Basket",
                keyColumn: "Id",
                keyValue: 2,
                column: "ApplicationUserId",
                value: "8c1427dc-c42f-4a6c-b524-ee22522c5e7d");

            migrationBuilder.UpdateData(
                table: "Basket",
                keyColumn: "Id",
                keyValue: 3,
                column: "ApplicationUserId",
                value: "dbb10eb9-b665-45d7-8f43-fbbdbf1c16a2");

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 1,
                column: "ApplicationUserId",
                value: "8c1427dc-c42f-4a6c-b524-ee22522c5e7d");

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 2,
                column: "ApplicationUserId",
                value: "dbb10eb9-b665-45d7-8f43-fbbdbf1c16a2");

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 66,
                column: "ApplicationUserId",
                value: "d45f7c4a-fb87-40e9-9421-68b0a78d2771");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Adress_AspNetUsers_ApplicationUserId",
                table: "Adress",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Basket_AspNetUsers_ApplicationUserId",
                table: "Basket",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_AspNetUsers_ApplicationUserId",
                table: "Order",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshToken_AspNetUsers_ApplicationUserId",
                table: "RefreshToken",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adress_AspNetUsers_ApplicationUserId",
                table: "Adress");

            migrationBuilder.DropForeignKey(
                name: "FK_Basket_AspNetUsers_ApplicationUserId",
                table: "Basket");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_AspNetUsers_ApplicationUserId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_RefreshToken_AspNetUsers_ApplicationUserId",
                table: "RefreshToken");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "EmailIndex",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8c1427dc-c42f-4a6c-b524-ee22522c5e7d");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d45f7c4a-fb87-40e9-9421-68b0a78d2771");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dbb10eb9-b665-45d7-8f43-fbbdbf1c16a2");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                newName: "ApplicationUser");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "ApplicationUser",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NormalizedUserName",
                table: "ApplicationUser",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NormalizedEmail",
                table: "ApplicationUser",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "ApplicationUser",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUser",
                table: "ApplicationUser",
                column: "Id");

            migrationBuilder.InsertData(
                table: "ApplicationUser",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "0cfd6406-58b5-47c9-9727-60c8e8c4a946", 0, "946a3ccf-4c9a-45ec-a12d-5a891c2e939b", null, false, false, null, null, null, null, null, false, "152cfbc2-8a38-49e7-890d-90121478f7e5", false, "Test2" });

            migrationBuilder.InsertData(
                table: "ApplicationUser",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "aa1df5e9-59d4-402b-8694-1a022a9e4df7", 0, "4a2f4449-c3d1-4d68-b09b-de5f1f08aa6c", null, false, false, null, null, null, null, null, false, "69571162-2f70-41b0-b80f-b20a6e19e858", false, "Test" });

            migrationBuilder.InsertData(
                table: "ApplicationUser",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "c5f7f70d-d516-493b-947c-eb4e81935b8c", 0, "1733ecef-d214-409d-9461-84df7002bccb", null, false, false, null, null, null, null, null, false, "be399f7a-be62-4fc6-9dc4-2ed85f7d1d90", false, "Test1" });

            migrationBuilder.UpdateData(
                table: "Adress",
                keyColumn: "Id",
                keyValue: 1,
                column: "ApplicationUserId",
                value: "aa1df5e9-59d4-402b-8694-1a022a9e4df7");

            migrationBuilder.UpdateData(
                table: "Adress",
                keyColumn: "Id",
                keyValue: 2,
                column: "ApplicationUserId",
                value: "c5f7f70d-d516-493b-947c-eb4e81935b8c");

            migrationBuilder.UpdateData(
                table: "Adress",
                keyColumn: "Id",
                keyValue: 3,
                column: "ApplicationUserId",
                value: "0cfd6406-58b5-47c9-9727-60c8e8c4a946");

            migrationBuilder.UpdateData(
                table: "Basket",
                keyColumn: "Id",
                keyValue: 1,
                column: "ApplicationUserId",
                value: "aa1df5e9-59d4-402b-8694-1a022a9e4df7");

            migrationBuilder.UpdateData(
                table: "Basket",
                keyColumn: "Id",
                keyValue: 2,
                column: "ApplicationUserId",
                value: "c5f7f70d-d516-493b-947c-eb4e81935b8c");

            migrationBuilder.UpdateData(
                table: "Basket",
                keyColumn: "Id",
                keyValue: 3,
                column: "ApplicationUserId",
                value: "0cfd6406-58b5-47c9-9727-60c8e8c4a946");

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 1,
                column: "ApplicationUserId",
                value: "c5f7f70d-d516-493b-947c-eb4e81935b8c");

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 2,
                column: "ApplicationUserId",
                value: "0cfd6406-58b5-47c9-9727-60c8e8c4a946");

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 66,
                column: "ApplicationUserId",
                value: "aa1df5e9-59d4-402b-8694-1a022a9e4df7");

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

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshToken_ApplicationUser_ApplicationUserId",
                table: "RefreshToken",
                column: "ApplicationUserId",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
