using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CallaApp.Migrations
{
    public partial class AddColumnTeamTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Testimontial",
                table: "Teams",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Testimontial",
                table: "Teams");
        }
    }
}
