using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class WerktHetNu2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "userparties",
                columns: table => new
                {
                    userId = table.Column<int>(type: "int", nullable: false),
                    partyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_userparties_parties_partyId",
                        column: x => x.partyId,
                        principalTable: "parties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_userparties_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "userpreferences",
                columns: table => new
                {
                    userId = table.Column<int>(type: "int", nullable: false),
                    preferenceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_userpreferences_preferences_preferenceId",
                        column: x => x.preferenceId,
                        principalTable: "preferences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_userpreferences_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_userparties_partyId",
                table: "userparties",
                column: "partyId");

            migrationBuilder.CreateIndex(
                name: "IX_userparties_userId",
                table: "userparties",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_userpreferences_preferenceId",
                table: "userpreferences",
                column: "preferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_userpreferences_userId",
                table: "userpreferences",
                column: "userId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "userparties");

            migrationBuilder.DropTable(
                name: "userpreferences");
        }
    }
}
