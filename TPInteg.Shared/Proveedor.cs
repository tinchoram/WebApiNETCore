﻿using System.Text.Json.Serialization;

namespace TPInteg.Shared
{
    public class Proveedor : BaseEntity
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NombreComercial { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public int LocalidadId { get; set; }
        public string Cuit { get; set; }
        public string SitioWebUrl { get; set; }
        public bool Activo { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public DateTime? FechaAlta { get; set; }
        public DateTime? FechaBaja { get; set; }

        [JsonIgnore] // Ignorar esta propiedad en la deserialización
        public virtual Localidad? Localidad { get; set; }

        // Ignora la serialización de la lista de productos al serializar un Proveedor
        [JsonIgnore]
        public List<Producto> Productos { get; set; } = new List<Producto>();
    }
}

