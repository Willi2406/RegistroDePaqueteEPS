using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistroDePaqueteEPS.Migrations
{
    /// <inheritdoc />
    public partial class Quinta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DireccionesDelivery",
                columns: table => new
                {
                    DireccionDeliveryId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClienteId = table.Column<int>(type: "INTEGER", nullable: false),
                    AutorizadoEntregaId = table.Column<int>(type: "INTEGER", nullable: false),
                    Provincia = table.Column<string>(type: "TEXT", nullable: false),
                    Municipio = table.Column<string>(type: "TEXT", nullable: false),
                    Sector = table.Column<string>(type: "TEXT", nullable: false),
                    Calle = table.Column<string>(type: "TEXT", nullable: false),
                    Domicilio = table.Column<string>(type: "TEXT", nullable: false),
                    Referencias = table.Column<string>(type: "TEXT", nullable: false),
                    Principal = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DireccionesDelivery", x => x.DireccionDeliveryId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DireccionesDelivery");
        }
    }
}
