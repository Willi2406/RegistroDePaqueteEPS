using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistroDePaqueteEPS.Migrations
{
    /// <inheritdoc />
    public partial class Sexta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Domicilio",
                table: "DireccionesDelivery",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "Alias",
                table: "DireccionesDelivery",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Alias",
                table: "DireccionesDelivery");

            migrationBuilder.AlterColumn<string>(
                name: "Domicilio",
                table: "DireccionesDelivery",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);
        }
    }
}
