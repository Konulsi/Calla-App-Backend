using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CallaApp.Migrations
{
    public partial class RemoveAndAddColumnForDecorTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsHover",
                table: "Decors");

            migrationBuilder.RenameColumn(
                name: "IsMain",
                table: "Decors",
                newName: "HoverImage");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HoverImage",
                table: "Decors",
                newName: "IsMain");

            migrationBuilder.AddColumn<bool>(
                name: "IsHover",
                table: "Decors",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
