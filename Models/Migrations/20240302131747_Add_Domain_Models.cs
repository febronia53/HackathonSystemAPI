using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Models.Migrations
{
    /// <inheritdoc />
    public partial class Add_Domain_Models : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hackathons",
                columns: table => new
                {
                    HackathonID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Theme = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistrationStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegistrationEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EventDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaxTeamSize = table.Column<int>(type: "int", nullable: false),
                    MaxTeams = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hackathons", x => x.HackathonID);
                });

            migrationBuilder.CreateTable(
                name: "TeamMembers",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TeamMemberID = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamMembers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ChallengeTitles",
                columns: table => new
                {
                    ChallengeTitleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HackathonID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChallengeTitles", x => x.ChallengeTitleId);
                    table.ForeignKey(
                        name: "FK_ChallengeTitles_Hackathons_HackathonID",
                        column: x => x.HackathonID,
                        principalTable: "Hackathons",
                        principalColumn: "HackathonID");
                });

            migrationBuilder.CreateTable(
                name: "TeamRegisterations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChallengeTitleId = table.Column<int>(type: "int", nullable: false),
                    HackathonID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamRegisterations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamRegisterations_ChallengeTitles_ChallengeTitleId",
                        column: x => x.ChallengeTitleId,
                        principalTable: "ChallengeTitles",
                        principalColumn: "ChallengeTitleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamRegisterations_Hackathons_HackathonID",
                        column: x => x.HackathonID,
                        principalTable: "Hackathons",
                        principalColumn: "HackathonID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChallengeTitles_HackathonID",
                table: "ChallengeTitles",
                column: "HackathonID");

            migrationBuilder.CreateIndex(
                name: "IX_TeamRegisterations_ChallengeTitleId",
                table: "TeamRegisterations",
                column: "ChallengeTitleId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamRegisterations_HackathonID",
                table: "TeamRegisterations",
                column: "HackathonID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeamMembers");

            migrationBuilder.DropTable(
                name: "TeamRegisterations");

            migrationBuilder.DropTable(
                name: "ChallengeTitles");

            migrationBuilder.DropTable(
                name: "Hackathons");
        }
    }
}
