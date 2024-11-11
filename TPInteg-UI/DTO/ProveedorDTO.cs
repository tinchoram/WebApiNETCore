using System;
using System.ComponentModel.DataAnnotations;

namespace TPInteg_UI.DTO
{
    public class ProveedorDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NombreComercial { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public int LocalidadId { get; set; }
        public string Cuit { get; set; }
        public string SitioWebUrl { get; set; }
        public bool Activo { get; set; } = true;
        public DateTime? FechaNacimiento { get; set; }
        public DateTime? FechaAlta { get; set; }
        public DateTime? FechaBaja { get; set; }
    }
}
