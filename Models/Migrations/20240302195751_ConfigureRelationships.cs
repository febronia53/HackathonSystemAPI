using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Models.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChallengeTitles_Hackathons_HackathonID",
                table: "ChallengeTitles");

            migrationBuilder.DropIndex(
                name: "IX_ChallengeTitles_HackathonID",
                table: "ChallengeTitles");

            migrationBuilder.DropColumn(
                name: "HackathonID",
                table: "ChallengeTitles");

            migrationBuilder.AddColumn<int>(
                name: "TeamRegisterationId",
                table: "TeamMembers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "HackathonChallengeTitles",
                columns: table => new
                {
                    ChallengeTitlesChallengeTitleId = table.Column<int>(type: "int", nullable: false),
                    HackathonID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HackathonChallengeTitles", x => new { x.ChallengeTitlesChallengeTitleId, x.HackathonID });
                    table.ForeignKey(
                        name: "FK_HackathonChallengeTitles_ChallengeTitles_ChallengeTitlesChallengeTitleId",
                        column: x => x.ChallengeTitlesChallengeTitleId,
                        principalTable: "ChallengeTitles",
                        principalColumn: "ChallengeTitleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HackathonChallengeTitles_Hackathons_HackathonID",
                        column: x => x.HackathonID,
                        principalTable: "Hackathons",
                        principalColumn: "HackathonID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeamMembers_TeamRegisterationId",
                table: "TeamMembers",
                column: "TeamRegisterationId");

            migrationBuilder.CreateIndex(
                name: "IX_HackathonChallengeTitles_HackathonID",
                table: "HackathonChallengeTitles",
                column: "HackathonID");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamMembers_TeamRegisterations_TeamRegisterationId",
                table: "TeamMembers",
                column: "TeamRegisterationId",
                principalTable: "TeamRegisterations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamMembers_TeamRegisterations_TeamRegisterationId",
                table: "TeamMembers");

            migrationBuilder.DropTable(
                name: "HackathonChallengeTitles");

            migrationBuilder.DropIndex(
                name: "IX_TeamMembers_TeamRegisterationId",
                table: "TeamMembers");

            migrationBuilder.DropColumn(
                name: "TeamRegisterationId",
                table: "TeamMembers");

            migrationBuilder.AddColumn<int>(
                name: "HackathonID",
                table: "ChallengeTitles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChallengeTitles_HackathonID",
                table: "ChallengeTitles",
                column: "HackathonID");

            migrationBuilder.AddForeignKey(
                name: "FK_ChallengeTitles_Hackathons_HackathonID",
                table: "ChallengeTitles",
                column: "HackathonID",
                principalTable: "Hackathons",
                principalColumn: "HackathonID");
        }
    }
}
