using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BanHangMVC.Migrations
{
    /// <inheritdoc />
    public partial class updateIsActive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "tb_Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "tb_Post",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "tb_New",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "tb_Category",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "tb_Products");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "tb_Post");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "tb_New");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "tb_Category");
        }
    }
}
