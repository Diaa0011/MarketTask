using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Market.Migrations
{
    /// <inheritdoc />
    public partial class client_merchant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_AspNetUsers_clientId",
                table: "Carts");

            migrationBuilder.AddColumn<int>(
                name: "MerchantId",
                table: "Stores",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "clientId",
                table: "Carts",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clients_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Merchants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Merchants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Merchants_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: 1,
                column: "MerchantId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: 2,
                column: "MerchantId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Stores_MerchantId",
                table: "Stores",
                column: "MerchantId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_userId",
                table: "Clients",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Merchants_userId",
                table: "Merchants",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Clients_clientId",
                table: "Carts",
                column: "clientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_Merchants_MerchantId",
                table: "Stores",
                column: "MerchantId",
                principalTable: "Merchants",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Clients_clientId",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Stores_Merchants_MerchantId",
                table: "Stores");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Merchants");

            migrationBuilder.DropIndex(
                name: "IX_Stores_MerchantId",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "MerchantId",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "clientId",
                table: "Carts",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_AspNetUsers_clientId",
                table: "Carts",
                column: "clientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
