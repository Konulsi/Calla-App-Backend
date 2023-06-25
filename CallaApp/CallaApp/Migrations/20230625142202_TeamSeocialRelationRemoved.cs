using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CallaApp.Migrations
{
    public partial class TeamSeocialRelationRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamSocials_Teams_TeamId",
                table: "TeamSocials");

            migrationBuilder.DropIndex(
                name: "IX_TeamSocials_TeamId",
                table: "TeamSocials");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "TeamSocials");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "TeamSocials",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TeamSocials_TeamId",
                table: "TeamSocials",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamSocials_Teams_TeamId",
                table: "TeamSocials",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
