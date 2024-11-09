using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TPInteg.Shared;
public class Producto : BaseEntity
{
    public string Codigo { get; set; }

    public string Descripcion { get; set; }

    [Range(1, 1000)]
    public double PrecioUnitario { get; set; }

    public int Stock { get; set; }

    public string Estado { get; set; }

    public int ProveedorId { get; set; }

    // Ignora la serialización del Proveedor al serializar un Producto y hazlo opcional
    [JsonIgnore]
    public Proveedor? Proveedor { get; set; }
}