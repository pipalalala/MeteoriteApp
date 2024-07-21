using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeteoriteAPI.Domain.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class InitMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "meteorites",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    meteorite_id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    nametype = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    recclass = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    mass = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: true),
                    fall = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    year = table.Column<int>(type: "int", nullable: true),
                    reclat = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: true),
                    reclong = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: true),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    coordinates_long = table.Column<decimal>(type: "decimal(18,5)", precision: 18, scale: 5, nullable: true),
                    coordinates_lat = table.Column<decimal>(type: "decimal(18,5)", precision: 18, scale: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_meteorites", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_meteorites_name",
                table: "meteorites",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "IX_meteorites_recclass",
                table: "meteorites",
                column: "recclass");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "meteorites");
        }
    }
}
