using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampusHub.Context.Migrations.PgSql.Migrations
{
    /// <inheritdoc />
    public partial class ClubAndClubMeeting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_clubs_participants_clubs_ParticipatingClubsId",
                table: "clubs_participants");

            migrationBuilder.CreateTable(
                name: "clubs_subscribers",
                columns: table => new
                {
                    SubscribedClubsId = table.Column<int>(type: "integer", nullable: false),
                    SubscribersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clubs_subscribers", x => new { x.SubscribedClubsId, x.SubscribersId });
                    table.ForeignKey(
                        name: "FK_clubs_subscribers_clubs_SubscribedClubsId",
                        column: x => x.SubscribedClubsId,
                        principalTable: "clubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_clubs_subscribers_users_SubscribersId",
                        column: x => x.SubscribersId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_clubs_subscribers_SubscribersId",
                table: "clubs_subscribers",
                column: "SubscribersId");

            migrationBuilder.AddForeignKey(
                name: "FK_clubs_participants_club_meetings_ParticipatingClubsId",
                table: "clubs_participants",
                column: "ParticipatingClubsId",
                principalTable: "club_meetings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_clubs_participants_club_meetings_ParticipatingClubsId",
                table: "clubs_participants");

            migrationBuilder.DropTable(
                name: "clubs_subscribers");

            migrationBuilder.AddForeignKey(
                name: "FK_clubs_participants_clubs_ParticipatingClubsId",
                table: "clubs_participants",
                column: "ParticipatingClubsId",
                principalTable: "clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
