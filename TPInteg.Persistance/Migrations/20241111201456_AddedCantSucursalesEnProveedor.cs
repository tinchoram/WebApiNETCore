using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TPInteg.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddedCantSucursalesEnProveedor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CantSucursales",
                table: "Proveedor",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CantSucursales",
                table: "Proveedor");
        }
    }
}
