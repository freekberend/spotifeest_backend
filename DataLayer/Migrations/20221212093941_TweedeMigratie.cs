using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class TweedeMigratie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PartyId",
                table: "users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PartyId",
                table: "songs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_PartyId",
                table: "users",
                column: "PartyId");

            migrationBuilder.CreateIndex(
                name: "IX_songs_PartyId",
                table: "songs",
                column: "PartyId");

            migrationBuilder.AddForeignKey(
                name: "FK_songs_parties_PartyId",
                table: "songs",
                column: "PartyId",
                principalTable: "parties",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_users_parties_PartyId",
                table: "users",
                column: "PartyId",
                principalTable: "parties",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_songs_parties_PartyId",
                table: "songs");

            migrationBuilder.DropForeignKey(
                name: "FK_users_parties_PartyId",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_PartyId",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_songs_PartyId",
                table: "songs");

            migrationBuilder.DropColumn(
                name: "PartyId",
                table: "users");

            migrationBuilder.DropColumn(
                name: "PartyId",
                table: "songs");
        }
    }
}
