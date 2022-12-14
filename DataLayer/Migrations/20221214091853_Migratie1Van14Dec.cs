using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class Migratie1Van14Dec : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_parties_partypreferences_FeestVoorkeurId",
                table: "parties");

            migrationBuilder.DropTable(
                name: "partypreferences");

            migrationBuilder.DropTable(
                name: "songs");

            migrationBuilder.DropColumn(
                name: "FeestOwner",
                table: "parties");

            migrationBuilder.RenameColumn(
                name: "Genre3",
                table: "preferences",
                newName: "Oorsprong3");

            migrationBuilder.RenameColumn(
                name: "Genre2",
                table: "preferences",
                newName: "Oorsprong2");

            migrationBuilder.RenameColumn(
                name: "Genre1",
                table: "preferences",
                newName: "Oorsprong1");

            migrationBuilder.RenameColumn(
                name: "FeestVoorkeurId",
                table: "parties",
                newName: "FeestOwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_parties_FeestVoorkeurId",
                table: "parties",
                newName: "IX_parties_FeestOwnerId");

            migrationBuilder.AddColumn<string>(
                name: "Nummer1",
                table: "preferences",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nummer2",
                table: "preferences",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nummer3",
                table: "preferences",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_parties_users_FeestOwnerId",
                table: "parties",
                column: "FeestOwnerId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_parties_users_FeestOwnerId",
                table: "parties");

            migrationBuilder.DropColumn(
                name: "Nummer1",
                table: "preferences");

            migrationBuilder.DropColumn(
                name: "Nummer2",
                table: "preferences");

            migrationBuilder.DropColumn(
                name: "Nummer3",
                table: "preferences");

            migrationBuilder.RenameColumn(
                name: "Oorsprong3",
                table: "preferences",
                newName: "Genre3");

            migrationBuilder.RenameColumn(
                name: "Oorsprong2",
                table: "preferences",
                newName: "Genre2");

            migrationBuilder.RenameColumn(
                name: "Oorsprong1",
                table: "preferences",
                newName: "Genre1");

            migrationBuilder.RenameColumn(
                name: "FeestOwnerId",
                table: "parties",
                newName: "FeestVoorkeurId");

            migrationBuilder.RenameIndex(
                name: "IX_parties_FeestOwnerId",
                table: "parties",
                newName: "IX_parties_FeestVoorkeurId");

            migrationBuilder.AddColumn<string>(
                name: "FeestOwner",
                table: "parties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "partypreferences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Voorkeur1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Voorkeur2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Voorkeur3 = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_partypreferences", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "songs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Artist = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PartyId = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_songs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_songs_parties_PartyId",
                        column: x => x.PartyId,
                        principalTable: "parties",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_songs_PartyId",
                table: "songs",
                column: "PartyId");

            migrationBuilder.AddForeignKey(
                name: "FK_parties_partypreferences_FeestVoorkeurId",
                table: "parties",
                column: "FeestVoorkeurId",
                principalTable: "partypreferences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
