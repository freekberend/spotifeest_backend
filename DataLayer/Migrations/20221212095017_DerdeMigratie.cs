using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class DerdeMigratie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Preference3",
                table: "partypreferences",
                newName: "Voorkeur3");

            migrationBuilder.RenameColumn(
                name: "Preference2",
                table: "partypreferences",
                newName: "Voorkeur2");

            migrationBuilder.RenameColumn(
                name: "Preference1",
                table: "partypreferences",
                newName: "Voorkeur1");

            migrationBuilder.RenameColumn(
                name: "RoomCode",
                table: "parties",
                newName: "FeestOwner");

            migrationBuilder.RenameColumn(
                name: "PartyOwner",
                table: "parties",
                newName: "FeestNaam");

            migrationBuilder.RenameColumn(
                name: "PartyName",
                table: "parties",
                newName: "FeestCode");

            migrationBuilder.AddColumn<int>(
                name: "FeestId",
                table: "preferences",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FeestVoorkeurId",
                table: "parties",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_preferences_FeestId",
                table: "preferences",
                column: "FeestId");

            migrationBuilder.CreateIndex(
                name: "IX_parties_FeestVoorkeurId",
                table: "parties",
                column: "FeestVoorkeurId");

            migrationBuilder.AddForeignKey(
                name: "FK_parties_partypreferences_FeestVoorkeurId",
                table: "parties",
                column: "FeestVoorkeurId",
                principalTable: "partypreferences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_preferences_parties_FeestId",
                table: "preferences",
                column: "FeestId",
                principalTable: "parties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_parties_partypreferences_FeestVoorkeurId",
                table: "parties");

            migrationBuilder.DropForeignKey(
                name: "FK_preferences_parties_FeestId",
                table: "preferences");

            migrationBuilder.DropIndex(
                name: "IX_preferences_FeestId",
                table: "preferences");

            migrationBuilder.DropIndex(
                name: "IX_parties_FeestVoorkeurId",
                table: "parties");

            migrationBuilder.DropColumn(
                name: "FeestId",
                table: "preferences");

            migrationBuilder.DropColumn(
                name: "FeestVoorkeurId",
                table: "parties");

            migrationBuilder.RenameColumn(
                name: "Voorkeur3",
                table: "partypreferences",
                newName: "Preference3");

            migrationBuilder.RenameColumn(
                name: "Voorkeur2",
                table: "partypreferences",
                newName: "Preference2");

            migrationBuilder.RenameColumn(
                name: "Voorkeur1",
                table: "partypreferences",
                newName: "Preference1");

            migrationBuilder.RenameColumn(
                name: "FeestOwner",
                table: "parties",
                newName: "RoomCode");

            migrationBuilder.RenameColumn(
                name: "FeestNaam",
                table: "parties",
                newName: "PartyOwner");

            migrationBuilder.RenameColumn(
                name: "FeestCode",
                table: "parties",
                newName: "PartyName");
        }
    }
}
