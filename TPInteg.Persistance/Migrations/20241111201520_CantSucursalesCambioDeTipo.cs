using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TPInteg.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class CantSucursalesCambioDeTipo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CantSucursales",
                table: "Proveedor",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CantSucursales",
                table: "Proveedor",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
