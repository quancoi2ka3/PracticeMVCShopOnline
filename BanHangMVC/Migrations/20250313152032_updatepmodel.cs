using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BanHangMVC.Migrations
{
    /// <inheritdoc />
    public partial class updatepmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductsID",
                table: "tb_ProductImage",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_tb_ProductImage_ProductsID",
                table: "tb_ProductImage",
                column: "ProductsID");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_ProductImage_tb_Products_ProductsID",
                table: "tb_ProductImage",
                column: "ProductsID",
                principalTable: "tb_Products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_ProductImage_tb_Products_ProductsID",
                table: "tb_ProductImage");

            migrationBuilder.DropIndex(
                name: "IX_tb_ProductImage_ProductsID",
                table: "tb_ProductImage");

            migrationBuilder.DropColumn(
                name: "ProductsID",
                table: "tb_ProductImage");
        }
    }
}
