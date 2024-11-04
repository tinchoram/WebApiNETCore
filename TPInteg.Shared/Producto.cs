namespace TPInteg.Shared;
public class Producto : BaseEntity
{
    public string Codigo { get; set; }
    public string Descripcion { get; set; }
    public decimal PrecioUnitario { get; set; }   
    public Proveedor Proveedor { get; set; }
}
