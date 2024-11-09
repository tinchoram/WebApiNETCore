using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TPInteg.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class @base : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Proveedor",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "Proveedor",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Telefono",
                table: "Proveedor",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Producto",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "Producto",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CodigoPostal",
                table: "Localidad",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Proveedor");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "Proveedor");

            migrationBuilder.DropColumn(
                name: "Telefono",
                table: "Proveedor");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Producto");

            migrationBuilder.DropColumn(
                name: "Stock",
                table: "Producto");

            migrationBuilder.DropColumn(
                name: "CodigoPostal",
                table: "Localidad");
        }
    }
}
