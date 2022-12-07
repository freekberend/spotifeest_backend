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
            migrationBuilder.DropTable(
                name: "treinen");

            migrationBuilder.DropPrimaryKey(
                name: "PK_party",
                table: "party");

            migrationBuilder.RenameTable(
                name: "party",
                newName: "parties");

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_parties",
                table: "parties",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_parties",
                table: "parties");

            migrationBuilder.DropColumn(
                name: "Token",
                table: "users");

            migrationBuilder.RenameTable(
                name: "parties",
                newName: "party");

            migrationBuilder.AddPrimaryKey(
                name: "PK_party",
                table: "party",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "treinen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Conducteur = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Snelheid = table.Column<int>(type: "int", nullable: false),
                    ThuisStation = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_treinen", x => x.Id);
                });
        }
    }
}
