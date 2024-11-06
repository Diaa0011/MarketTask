using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Market.Migrations
{
    /// <inheritdoc />
    public partial class fixRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_AspNetUsers_clientId",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_clientId",
                table: "Carts");

            migrationBuilder.RenameColumn(
                name: "clientId",
                table: "Carts",
                newName: "ClientId");

            migrationBuilder.AlterColumn<string>(
                name: "ClientId",
                table: "Carts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClientAddress",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClientCity",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClientState",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Carts_ClientId",
                table: "Carts",
                column: "ClientId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_AspNetUsers_ClientId",
                table: "Carts",
                column: "ClientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_AspNetUsers_ClientId",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_ClientId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "ClientAddress",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ClientCity",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ClientState",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "Carts",
                newName: "clientId");

            migrationBuilder.AlterColumn<string>(
                name: "clientId",
                table: "Carts",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_clientId",
                table: "Carts",
                column: "clientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_AspNetUsers_clientId",
                table: "Carts",
                column: "clientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
