using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CallaApp.Migrations
{
    public partial class AddDefaultValueToProductImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsTest",
                table: "ProductImages",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsTest",
                table: "ProductImages");
        }
    }
}
