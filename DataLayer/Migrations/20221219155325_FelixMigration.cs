using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class FelixMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "parties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FeestCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FeestNaam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Closed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_parties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "trains",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TreinNaam = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trains", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "traveler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_traveler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "preferences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FeestId = table.Column<int>(type: "int", nullable: false),
                    Nummer1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nummer2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nummer3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Oorsprong1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Oorsprong2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Oorsprong3 = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_preferences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_preferences_parties_FeestId",
                        column: x => x.FeestId,
                        principalTable: "parties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReizigerTrein",
                columns: table => new
                {
                    ReizigersId = table.Column<int>(type: "int", nullable: false),
                    TreinenId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReizigerTrein", x => new { x.ReizigersId, x.TreinenId });
                    table.ForeignKey(
                        name: "FK_ReizigerTrein_trains_TreinenId",
                        column: x => x.TreinenId,
                        principalTable: "trains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReizigerTrein_traveler_ReizigersId",
                        column: x => x.ReizigersId,
                        principalTable: "traveler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PartyUser",
                columns: table => new
                {
                    PartiesId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartyUser", x => new { x.PartiesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_PartyUser_parties_PartiesId",
                        column: x => x.PartiesId,
                        principalTable: "parties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PartyUser_users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "recommendationhistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Artist = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Track = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Spotifytrackid = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Keuze = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Jsonstring = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EigenaarId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recommendationhistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_recommendationhistories_users_EigenaarId",
                        column: x => x.EigenaarId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PreferenceUser",
                columns: table => new
                {
                    PreferencesId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreferenceUser", x => new { x.PreferencesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_PreferenceUser_preferences_PreferencesId",
                        column: x => x.PreferencesId,
                        principalTable: "preferences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PreferenceUser_users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PartyUser_UsersId",
                table: "PartyUser",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_preferences_FeestId",
                table: "preferences",
                column: "FeestId");

            migrationBuilder.CreateIndex(
                name: "IX_PreferenceUser_UsersId",
                table: "PreferenceUser",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_recommendationhistories_EigenaarId",
                table: "recommendationhistories",
                column: "EigenaarId");

            migrationBuilder.CreateIndex(
                name: "IX_ReizigerTrein_TreinenId",
                table: "ReizigerTrein",
                column: "TreinenId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PartyUser");

            migrationBuilder.DropTable(
                name: "PreferenceUser");

            migrationBuilder.DropTable(
                name: "recommendationhistories");

            migrationBuilder.DropTable(
                name: "ReizigerTrein");

            migrationBuilder.DropTable(
                name: "preferences");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "trains");

            migrationBuilder.DropTable(
                name: "traveler");

            migrationBuilder.DropTable(
                name: "parties");
        }
    }
}
