using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TPInteg.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class NuevosCamposProveedor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Proveedor_Localidad_LocalidadId",
                table: "Proveedor");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaBaja",
                table: "Proveedor",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaAlta",
                table: "Proveedor",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "Proveedor",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Apellido",
                table: "Proveedor",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Cuit",
                table: "Proveedor",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaNacimiento",
                table: "Proveedor",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NombreComercial",
                table: "Proveedor",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SitioWebUrl",
                table: "Proveedor",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Proveedor_Localidad_LocalidadId",
                table: "Proveedor",
                column: "LocalidadId",
                principalTable: "Localidad",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Proveedor_Localidad_LocalidadId",
                table: "Proveedor");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Proveedor");

            migrationBuilder.DropColumn(
                name: "Apellido",
                table: "Proveedor");

            migrationBuilder.DropColumn(
                name: "Cuit",
                table: "Proveedor");

            migrationBuilder.DropColumn(
                name: "FechaNacimiento",
                table: "Proveedor");

            migrationBuilder.DropColumn(
                name: "NombreComercial",
                table: "Proveedor");

            migrationBuilder.DropColumn(
                name: "SitioWebUrl",
                table: "Proveedor");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "FechaBaja",
                table: "Proveedor",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "FechaAlta",
                table: "Proveedor",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Proveedor_Localidad_LocalidadId",
                table: "Proveedor",
                column: "LocalidadId",
                principalTable: "Localidad",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
