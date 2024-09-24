using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ValhallaManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vikingos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    BatallasGanadas = table.Column<int>(type: "INTEGER", nullable: false),
                    ArmaFavorita = table.Column<int>(type: "INTEGER", nullable: false),
                    NivelHonor = table.Column<int>(type: "INTEGER", nullable: false),
                    CausaMuerteGloriosa = table.Column<int>(type: "INTEGER", nullable: false),
                    ValhallaPoints = table.Column<int>(type: "INTEGER", nullable: false),
                    Clasificacion = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vikingos", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vikingos");
        }
    }
}
