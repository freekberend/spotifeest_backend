using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class VierdeMigratie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PartyUser_parties_PartyId",
                table: "PartyUser");

            migrationBuilder.RenameColumn(
                name: "PartyId",
                table: "PartyUser",
                newName: "PartiesId");

            migrationBuilder.AddForeignKey(
                name: "FK_PartyUser_parties_PartiesId",
                table: "PartyUser",
                column: "PartiesId",
                principalTable: "parties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PartyUser_parties_PartiesId",
                table: "PartyUser");

            migrationBuilder.RenameColumn(
                name: "PartiesId",
                table: "PartyUser",
                newName: "PartyId");

            migrationBuilder.AddForeignKey(
                name: "FK_PartyUser_parties_PartyId",
                table: "PartyUser",
                column: "PartyId",
                principalTable: "parties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
