using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BanHangMVC.Migrations
{
    /// <inheritdoc />
    public partial class updatetbNews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Detail",
                table: "tb_New",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Detail",
                table: "tb_New");
        }
    }
}
