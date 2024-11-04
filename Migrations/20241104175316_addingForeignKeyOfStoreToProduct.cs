using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Market.Migrations
{
    /// <inheritdoc />
    public partial class addingForeignKeyOfStoreToProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Stores_storeId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "storeId",
                table: "Products",
                newName: "StoreId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_storeId",
                table: "Products",
                newName: "IX_Products_StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Stores_StoreId",
                table: "Products",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Stores_StoreId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "StoreId",
                table: "Products",
                newName: "storeId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_StoreId",
                table: "Products",
                newName: "IX_Products_storeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Stores_storeId",
                table: "Products",
                column: "storeId",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
