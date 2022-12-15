using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class RecommendationHistoryAdd2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_recommendationhistories_EigenaarId",
                table: "recommendationhistories",
                column: "EigenaarId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "recommendationhistories");
        }
    }
}
