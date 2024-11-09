using System.Text.Json.Serialization;

namespace TPInteg.Shared;
public class Localidad : BaseEntity
{
    public string Nombre { get; set; }

    public int CodigoPostal { get; set; }

    // Ignora la serialización de la lista de proveedores al serializar una Localidad
    [JsonIgnore]
    public List<Proveedor> Proveedores { get; set; } = new List<Proveedor>();
}
