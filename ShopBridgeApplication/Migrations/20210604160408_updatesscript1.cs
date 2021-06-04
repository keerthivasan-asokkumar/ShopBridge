using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopBridgeApplication.Migrations
{
    public partial class updatesscript1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductCategory",
                table: "Product");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductCategory",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
