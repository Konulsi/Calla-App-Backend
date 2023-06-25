using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CallaApp.Migrations
{
    public partial class CreateTeamSocialPivotTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Link",
                table: "TeamSocials");

            migrationBuilder.CreateTable(
                name: "TeamSocialsPivot",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamId = table.Column<int>(type: "int", nullable: false),
                    TeamSocialId = table.Column<int>(type: "int", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoftDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamSocialsPivot", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamSocialsPivot_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamSocialsPivot_TeamSocials_TeamSocialId",
                        column: x => x.TeamSocialId,
                        principalTable: "TeamSocials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeamSocialsPivot_TeamId",
                table: "TeamSocialsPivot",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamSocialsPivot_TeamSocialId",
                table: "TeamSocialsPivot",
                column: "TeamSocialId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeamSocialsPivot");

            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "TeamSocials",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
