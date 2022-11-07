using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RiodeBackEndFinal.Migrations
{
    public partial class UpdateBasket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserBasket_AspNetUsers_AppUserId1",
                table: "UserBasket");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBasket_Products_ProductId",
                table: "UserBasket");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserBasket",
                table: "UserBasket");

            migrationBuilder.DropIndex(
                name: "IX_UserBasket_AppUserId1",
                table: "UserBasket");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "UserBasket");

            migrationBuilder.RenameTable(
                name: "UserBasket",
                newName: "UserBaskets");

            migrationBuilder.RenameIndex(
                name: "IX_UserBasket_ProductId",
                table: "UserBaskets",
                newName: "IX_UserBaskets_ProductId");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "UserBaskets",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserBaskets",
                table: "UserBaskets",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserBaskets_AppUserId",
                table: "UserBaskets",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserBaskets_AspNetUsers_AppUserId",
                table: "UserBaskets",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserBaskets_Products_ProductId",
                table: "UserBaskets",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserBaskets_AspNetUsers_AppUserId",
                table: "UserBaskets");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBaskets_Products_ProductId",
                table: "UserBaskets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserBaskets",
                table: "UserBaskets");

            migrationBuilder.DropIndex(
                name: "IX_UserBaskets_AppUserId",
                table: "UserBaskets");

            migrationBuilder.RenameTable(
                name: "UserBaskets",
                newName: "UserBasket");

            migrationBuilder.RenameIndex(
                name: "IX_UserBaskets_ProductId",
                table: "UserBasket",
                newName: "IX_UserBasket_ProductId");

            migrationBuilder.AlterColumn<int>(
                name: "AppUserId",
                table: "UserBasket",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId1",
                table: "UserBasket",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserBasket",
                table: "UserBasket",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserBasket_AppUserId1",
                table: "UserBasket",
                column: "AppUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserBasket_AspNetUsers_AppUserId1",
                table: "UserBasket",
                column: "AppUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserBasket_Products_ProductId",
                table: "UserBasket",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
